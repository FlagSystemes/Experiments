using System;

[Flags]
public enum ModuleAttributes
{
    ILOnly = 1,
    Required32Bit = 2,
    StrongNameSigned = 8,
}