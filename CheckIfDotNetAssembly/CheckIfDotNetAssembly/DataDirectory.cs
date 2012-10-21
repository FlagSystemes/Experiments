using System;

public struct DataDirectory
{

    public readonly UInt32 VirtualAddress;
    public readonly uint Size;

    public bool IsZero
    {
        get { return VirtualAddress == 0 && Size == 0; }
    }

    public DataDirectory(UInt32 rva, uint size)
    {
        this.VirtualAddress = rva;
        this.Size = size;
    }
}