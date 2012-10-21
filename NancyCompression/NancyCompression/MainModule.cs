using Nancy;

namespace NancyCompression
{
    public class MainModule:NancyModule
    {
        public MainModule()
        {
            Get["/{FileName}"] = o => Response.AsFile((string)o.FileName +  ".txt");
        }
    }
}