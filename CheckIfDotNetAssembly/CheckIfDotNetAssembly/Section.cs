using System;

public sealed class Section
{
    public string Name;
    public UInt32 VirtualAddress;
    public uint VirtualSize;
    public uint SizeOfRawData;
    public uint PointerToRawData;
    public byte[] Data;
}