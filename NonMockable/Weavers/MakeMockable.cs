using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Xml.Linq;
using Mono.Cecil;

public class MakeMockable
{
    public XElement Config { get; set; }
    public Action<string> LogInfo { get; set; }
    public Action<string> LogWarning { get; set; }
    public IAssemblyResolver AssemblyResolver { get; set; }
    public ModuleDefinition ModuleDefinition { get; set; }
    public string AssemblyPath { get; set; }

    public MakeMockable()
    {
        LogWarning = s => { };
        LogInfo = s => { };
    }

    public void Execute()
    {

        using (var catalog = new AssemblyCatalog(GetType().Assembly))
        using (var container = new CompositionContainer(catalog))
        {
            container.ComposeExportedValue(ModuleDefinition);
            container.ComposeExportedValue(this);

            var directoryName = Path.GetDirectoryName(AssemblyPath);
            
            foreach (var targetFile in Directory.EnumerateFiles(directoryName, "*.dll"))
            {
                if (targetFile != AssemblyPath)
                {
                    container.GetExportedValue<AssemblyProcessor>().Execute(targetFile);
                }
            }
            container.GetExportedValue<AssemblyProcessor>().Execute(ModuleDefinition);
        }
    }
}