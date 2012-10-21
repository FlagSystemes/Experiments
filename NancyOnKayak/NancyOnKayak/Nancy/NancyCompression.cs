using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using Nancy;
using Nancy.Bootstrapper;

public class NancyCompression : IStartup
{

    public void Initialize(IPipelines pipelines)
    {
        pipelines.AfterRequest.AddItemToStartOfPipeline(CheckForCompression);
    }


    public IEnumerable<TypeRegistration> TypeRegistrations
    {
        get { return null; }
    }

    public IEnumerable<CollectionTypeRegistration> CollectionTypeRegistrations
    {
        get { return null; }
    }

    public IEnumerable<InstanceRegistration> InstanceRegistrations
    {
        get { return null; }
    }

    void CheckForCompression(NancyContext context)
    {
        if (!RequestIsGzipCompatible(context.Request))
        {
            return;
        }

        if (context.Response.StatusCode != HttpStatusCode.OK)
        {
            return;
        }

        if (!ResponseIsCompatibleMimeType(context.Response))
        {
            return;
        }

        if (ContentLengthIsTooSmall(context.Response))
        {
            return;
        }

        CompressResponse(context.Response);
    }

    void CompressResponse(Response response)
    {
        response.Headers["Content-Encoding"] = "gzip";

        var contents = response.Contents;
        response.Contents = responseStream =>
                                {
                                    using (var compression = new GZipStream(responseStream, CompressionMode.Compress))
                                    {
                                        contents(compression);
                                    }
                                };
    }

    bool ContentLengthIsTooSmall(Response response)
    {
        string contentLength;
        if (response.Headers.TryGetValue("Content-Length", out contentLength))
        {
            var length = long.Parse(contentLength);
            if (length < 4096)
            {
                return true;
            }
        }
        return false;
    }

    public static List<string> ValidMimes = new List<string>
                                                {
                                                    "text/css",
                                                    "text/html",
                                                    "text/plain",
                                                    "application/xml",
                                                    "application/json",
                                                    "application/xaml+xml",
                                                    "application/x-javascript"
                                                };

    bool ResponseIsCompatibleMimeType(Response response)
    {
        return ValidMimes.Any(x => x == response.ContentType);
    }

    bool RequestIsGzipCompatible(Request request)
    {
        return request.Headers.AcceptEncoding.Any(x => x.Contains("gzip"));
    }
}