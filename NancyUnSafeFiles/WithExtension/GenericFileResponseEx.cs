using System;
using System.Collections.Generic;
using System.IO;
using Nancy;

namespace WithExtension
{
    public class GenericFileResponseEx : Response
    {
        public static List<string> RootPaths { get; set; }

        static GenericFileResponseEx()
        {
            RootPaths = new List<string>();
        }
        public GenericFileResponseEx(string filePath) :
            this(filePath, MimeTypes.GetMimeType(filePath))
        {
        }


        public GenericFileResponseEx(string filePath, string contentType)
        {
            InitializeGenericFileResonse(filePath, contentType);
        }

        public string Filename { get; protected set; }

        static Action<Stream> GetFileContent(string filePath)
        {
            return stream =>
                       {
                           using (var file = File.OpenRead(filePath))
                           {
                               file.CopyTo(stream);
                           }
                       };
        }

        static bool IsSafeFilePath(string rootPath, string filePath)
        {
            if (!Path.HasExtension(filePath))
            {
                return false;
            }

            if (!File.Exists(filePath))
            {
                return false;
            }

            var fullPath = Path.GetFullPath(filePath);

            return fullPath.StartsWith(rootPath, StringComparison.Ordinal);
        }

        void InitializeGenericFileResonse(string filePath, string contentType)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                StatusCode = HttpStatusCode.NotFound;
                return;
            }
            if (RootPaths == null || RootPaths.Count == 0)
            {
                throw new InvalidOperationException("No RootPaths defined.");
            }
            foreach (var rootPath in RootPaths)
            {
                string fullPath;
                if (Path.IsPathRooted(filePath))
                {
                    fullPath = filePath;
                }
                else
                {
                    fullPath = Path.Combine(rootPath, filePath);
                }

                if (IsSafeFilePath(rootPath, fullPath))
                {
                    Filename = Path.GetFileName(fullPath);

                    var fi = new FileInfo(fullPath);
                    // TODO - set a standard caching time and/or public?
                    Headers["ETag"] = fi.LastWriteTimeUtc.Ticks.ToString("x");
                    Headers["Last-Modified"] = fi.LastWriteTimeUtc.ToString("R");
                    Contents = GetFileContent(fullPath);
                    ContentType = contentType;
                    StatusCode = HttpStatusCode.OK;
                    return;
                }
            }

            StatusCode = HttpStatusCode.NotFound;
        }
    }
}