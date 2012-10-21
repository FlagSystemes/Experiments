using Nancy;

namespace OverrideRootPath
{
    public class RootPathProvider : IRootPathProvider
    {
        public string GetRootPath()
        {
            return @"C:\PathWithFilesToShare";
        }
    }

}