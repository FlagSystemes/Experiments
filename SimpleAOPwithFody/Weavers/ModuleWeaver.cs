using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Mono.Cecil;
using Mono.Cecil.Cil;

public class ModuleWeaver
{
    public ModuleDefinition ModuleDefinition { get; set; }

    MethodReference nowMethod;
    MethodReference toLongTimeStringMethod;
    TypeReference dateTimeType;
    MethodReference concatMethod;
    MethodReference writLineMethod;

    public void Execute()
    {
        InitialiseReferences();
        foreach (var method in GetMethods())
        {
            Inject(method);
        }
    }

    void InitialiseReferences()
    {
        dateTimeType = ModuleDefinition.Import(typeof (DateTime));
        var dateTimeDefinition = dateTimeType.Resolve();
        nowMethod = ModuleDefinition.Import(dateTimeDefinition.Methods.First(x => x.Name == "get_Now"));
        toLongTimeStringMethod = ModuleDefinition.Import(dateTimeDefinition.Methods.First(x => x.Name == "ToLongTimeString"));
        var stringType = ModuleDefinition.Import(typeof(string)).Resolve();
        concatMethod = ModuleDefinition.Import(stringType.Methods.First(x => x.Name == "Concat" && x.Parameters.Count == 2));
        var debugType = ModuleDefinition.Import(typeof(Debug)).Resolve();
        writLineMethod = ModuleDefinition.Import(debugType.Methods.First(x => x.Name == "WriteLine" && x.Parameters.Count == 1 && x.Parameters[0].ParameterType.Name == "String"));
    }

    void Inject(MethodDefinition method)
    {
        var instructions = method.Body.Instructions;
        var variableDefinition = new VariableDefinition(dateTimeType);
        method.Body.Variables.Add(variableDefinition);
        instructions.Insert(0, Instruction.Create(OpCodes.Call, nowMethod));
        instructions.Insert(1, Instruction.Create(OpCodes.Stloc_0));
        instructions.Insert(2, Instruction.Create(OpCodes.Ldloca_S, variableDefinition));
        instructions.Insert(3, Instruction.Create(OpCodes.Call, toLongTimeStringMethod));
        instructions.Insert(4, Instruction.Create(OpCodes.Ldstr, " " + method.Name));
        instructions.Insert(5, Instruction.Create(OpCodes.Call, concatMethod));
        instructions.Insert(6, Instruction.Create(OpCodes.Call, writLineMethod));
    }

    IEnumerable<MethodDefinition> GetMethods()
    {
        return ModuleDefinition.Types.SelectMany(x => x.Methods.Where(ContainsAttribute));
    }

    bool ContainsAttribute(MethodDefinition method)
    {
        return method.CustomAttributes.Any(x => x.Constructor.DeclaringType.FullName == "LogMethodEntryAttribute");
    }
}