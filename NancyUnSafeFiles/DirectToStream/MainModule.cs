using System.IO;
using Nancy;

namespace DirectToStream
{
    public class MainModule : NancyModule
    {
        public MainModule()
        {
            Get["/"] = o =>
                           {
                               return Response.AsStream(() => File.OpenRead(@"C:\PathWithFilesToShare\FileName.txt"), "text/plain");
                           };
        }
    }
}