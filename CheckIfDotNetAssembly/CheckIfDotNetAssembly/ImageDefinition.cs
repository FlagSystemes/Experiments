public class ImageDefinition
{
    public TargetArchitecture Architecture { get; set; }

    public ModuleKind Kind { get; set; }

    public TargetRuntime Runtime { get; set; }

    public ModuleAttributes Attributes { get; set; }

    public uint EntryPointToken { get; set; }

    public DataDirectory Resources { get; set; }

    public DataDirectory Debug { get; set; }
}