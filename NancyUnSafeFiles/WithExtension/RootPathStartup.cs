using System.Collections.Generic;
using Nancy;
using Nancy.Bootstrapper;

namespace WithExtension
{
    public class RootPathStartup : IStartup
    {
        public RootPathStartup(IRootPathProvider rootPathProvider)
        {
            GenericFileResponseEx.RootPaths.Add(rootPathProvider.GetRootPath());
            GenericFileResponseEx.RootPaths.Add(@"C:\PathWithFilesToShare");
        }

        public IEnumerable<TypeRegistration> TypeRegistrations { get { return null; } }

        public IEnumerable<CollectionTypeRegistration> CollectionTypeRegistrations { get { return null; } }

        public IEnumerable<InstanceRegistration> InstanceRegistrations { get { return null; } }

        public void Initialize(IApplicationPipelines pipelines)
        {
        }
    }
}