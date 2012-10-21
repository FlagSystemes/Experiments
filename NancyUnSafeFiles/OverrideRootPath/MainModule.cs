using Nancy;

namespace OverrideRootPath
{
    public class MainModule : NancyModule
    {
        public MainModule()
        {
            Get["/"] = o =>
                           {
                               return Response.AsFile("FileName.txt");
                           };
        }
    }
}