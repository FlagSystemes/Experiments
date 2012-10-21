using Nancy;

namespace NancyEmbeddedViews
{
    public class MainModule:NancyModule
    {
        public MainModule()
        {
            Get["/"] = o => { return View["Index.sshtml"]; };
        }
    }
}