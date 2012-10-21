using Nancy;

namespace WithExtension
{
    public class MainModule : NancyModule
    {
        public MainModule()
        {
            Get["/"] = o =>
            {
                return Response.AsFileEx("FileName.txt");
            };
        }
    }
}
