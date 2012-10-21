using System.ComponentModel.Composition;
using System.Reflection;
using Mono.Cecil;

[Export, PartCreationPolicy(CreationPolicy.Shared)]
public class AssemblyProcessor
{
    TypeProcessor typeProcessor;
    MakeMockable makeMockable;

    [ImportingConstructor]
    public AssemblyProcessor(TypeProcessor typeProcessor, MakeMockable makeMockable)
    {
        this.typeProcessor = typeProcessor;
        this.makeMockable = makeMockable;
    }

    public void Execute(string assemblyPath)
    {
        if (AssemblyName.GetAssemblyName(assemblyPath).GetPublicKey() != null)
        {
            //do no make mockable because it is signed
            return;
        }
        var moduleDefinition = ModuleDefinition.ReadModule(assemblyPath);

        Execute(moduleDefinition);
        moduleDefinition.Write(assemblyPath);
    }

    public void Execute(ModuleDefinition moduleDefinition)
    {
        foreach (var type in moduleDefinition.GetAllTypeDefinitions())
        {
            if (!type.IsClass)
            {
                continue;
            }
            typeProcessor.Execute(type);
        }
    }
}