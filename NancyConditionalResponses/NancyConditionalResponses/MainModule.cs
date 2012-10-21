using Nancy;

namespace NancyConditionalResponses
{
    public class MainModule:NancyModule
    {
        public MainModule()
        {
            Get["/{FileName}"] = o => { return Response.AsFile((string)o.FileName +  ".txt"); };
            this.RegisterCacheCheck();
        }
    }
}