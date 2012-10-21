using System.ComponentModel.Composition;
using Mono.Cecil;

[Export, PartCreationPolicy(CreationPolicy.Shared)]
public class TypeProcessor
{
    MakeMockable makeMockable;

    [ImportingConstructor]
    public TypeProcessor(MakeMockable makeMockable)
    {
        this.makeMockable = makeMockable;
    }

    public void Execute(TypeDefinition typeDefinition)
    {
        makeMockable.LogInfo("\t" + typeDefinition.FullName);

        typeDefinition.IsSealed = false;
        foreach (var method in typeDefinition.Methods)
        {
            if (method.IsConstructor)
            {
                continue;
            }
            ProcessMethod(method);
        }
    }

    void ProcessMethod(MethodDefinition method)
    {
        if (method == null)
        {
            return;
        }
        if (method.IsFinal)
        {
            return;
        }
        if (method.IsStatic)
        {
            return;
        }
        if (method.IsVirtual)
        {
            return;
        }
        if (method.IsPrivate)
        {
            return;
        }
        method.IsVirtual = true;
        method.IsNewSlot = true;
    }
}