using System.ComponentModel.Composition.Hosting;
using Microsoft.Build.Utilities;
using Mono.Cecil;
using Newtonsoft.Json.Linq;

namespace MyTask
{
    public class MySimpleTask : AppDomainIsolatedTask
    {
        static AssemblyCatalog catalog;

        static MySimpleTask()
        {
            catalog = new AssemblyCatalog(typeof(MySimpleTask).Assembly);
        }
        public string AssemblyPath { set; get; }
        public override bool Execute()
        {
            using (var container = new CompositionContainer(catalog))
            {
                var moduleDefinition = ModuleDefinition.ReadModule(AssemblyPath);
                var buildLogger = LoggingFactory.BuildLogger();
                foreach (var typeDefinition in moduleDefinition.Types)
                {
                    buildLogger.Info(typeDefinition);
                }
                var jObject = new JObject();

            }
            return true;
        }
    }
}