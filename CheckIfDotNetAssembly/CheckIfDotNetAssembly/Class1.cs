using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using NUnit.Framework;

    [TestFixture]
    public class Class1
    {
        [Test]
        public void Foo()
        {
            foreach (var file in Directory.EnumerateFiles(@"C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\Bin\NETFX 4.0 Tools\x64", "*.dll", SearchOption.AllDirectories))
            {
                
        TestFile(file);
            }
            ;


    }

        static void TestFile(string file)
        {
            bool loadFileFailed = false;
            try
            {

            Assembly.LoadFile(file);
            }
            catch (Exception exception)
            {
                loadFileFailed = true;
            }
            bool imageReaderFailed = false;
            try
            {

            using (var fileStream = File.OpenRead(file))
            {
                using (var imageReader = new ImageReader(fileStream))
                {
                    imageReader.TryReadImage();
                }
            }
            }
            catch (BadImageFormatException)
            {
                imageReaderFailed = true;
            }
            Assert.AreEqual(loadFileFailed, imageReaderFailed,file);
        }
    }

public class ImageReader: IDisposable
{

    Stream stream;
    public ImageDefinition imageDefinition;
    BinaryReader binaryReader;
    DataDirectory cli;
    DataDirectory metadata;
    Section[] Sections;

    public ImageReader(Stream stream)
    {
        imageDefinition = new ImageDefinition();
        this.stream = stream;
        binaryReader = new BinaryReader(stream);
    }


    public void TryReadImage()
    {
        if (stream.Length < 128)
            throw new BadImageFormatException();

        // - DOSHeader

        // PE					2
        // Start				58
        // Lfanew				4
        // End					64

        if (binaryReader.ReadUInt16() != 0x5a4d)
            throw new BadImageFormatException();


        Advance(58);

        MoveTo(binaryReader.ReadUInt32());

        if (binaryReader.ReadUInt32() != 0x00004550)
            throw new BadImageFormatException();

        // - PEFileHeader

        // Machine				2
        imageDefinition.Architecture = ReadArchitecture();

        // NumberOfSections		2
        var sections = binaryReader.ReadUInt16();

        // TimeDateStamp		4
        // PointerToSymbolTable	4
        // NumberOfSymbols		4
        // OptionalHeaderSize	2
        Advance(14);

        // Characteristics		2
        var characteristics = binaryReader.ReadUInt16();

        ushort subsystem;
        ReadOptionalHeaders(out subsystem);
        ReadSections(sections);
        ReadCLIHeader();
        ReadMetadata();

        imageDefinition.Kind = GetModuleKind(characteristics, subsystem);
    }


    void ReadMetadata()
    {
        MoveTo(metadata);

        if (binaryReader.ReadUInt32() != 0x424a5342)
            throw new BadImageFormatException();

        // MajorVersion			2
        // MinorVersion			2
        // Reserved				4
        Advance(8);

        var version = ReadZeroTerminatedString(binaryReader.ReadInt32());
        imageDefinition.Runtime = ParseRuntime(version);

    }
    void ReadSections(ushort count)
    {
        var sections = new Section[count];

        for (var i = 0; i < count; i++)
        {
            var section = new Section();

            // Name
            section.Name = ReadZeroTerminatedString(8);

            // VirtualSize		4
            Advance(4);

            // VirtualAddress	4
            section.VirtualAddress = binaryReader.ReadUInt32();
            // SizeOfRawData	4
            section.SizeOfRawData = binaryReader.ReadUInt32();
            // PointerToRawData	4
            section.PointerToRawData = binaryReader.ReadUInt32();

            // PointerToRelocations		4
            // PointerToLineNumbers		4
            // NumberOfRelocations		2
            // NumberOfLineNumbers		2
            // Characteristics			4
            Advance(16);

            sections[i] = section;

            ReadSectionData(section);
        }

        Sections = sections;
    }


    void ReadSectionData(Section section)
    {
        var position = stream.Position;

        MoveTo(section.PointerToRawData);

        var length = (int)section.SizeOfRawData;
        var data = new byte[length];
        int offset = 0, read;

        while ((read = binaryReader.Read(data, offset, length - offset)) > 0)
            offset += read;

        section.Data = data;

        stream.Position = position;
    }
    void MoveTo(DataDirectory directory)
    {
        stream.Position = ResolveVirtualAddress(directory.VirtualAddress);
    }


    public uint ResolveVirtualAddress(UInt32 rva)
    {
        var section = GetSectionAtVirtualAddress(rva);
        if (section == null)
            throw new ArgumentOutOfRangeException();

        return ResolveVirtualAddressInSection(rva, section);
    }
    public uint ResolveVirtualAddressInSection(UInt32 rva, Section section)
    {
        return rva + section.PointerToRawData - section.VirtualAddress;
    }
    public Section GetSectionAtVirtualAddress(UInt32 rva)
    {
        var sections = this.Sections;
        for (var i = 0; i < sections.Length; i++)
        {
            var section = sections[i];
            if (rva >= section.VirtualAddress && rva < section.VirtualAddress + section.SizeOfRawData)
                return section;
        }

        return null;
    }
    public static TargetRuntime ParseRuntime(string self)
    {
        switch (self[1])
        {
            case '1':
                return self[3] == '0'
                    ? TargetRuntime.Net_1_0
                    : TargetRuntime.Net_1_1;
            case '2':
                return TargetRuntime.Net_2_0;
            case '4':
            default:
                return TargetRuntime.Net_4_0;
        }
    }
    string ReadZeroTerminatedString(int length)
    {
        var read = 0;
        var buffer = new char[length];
        var bytes = binaryReader.ReadBytes(length);
        while (read < length)
        {
            var current = bytes[read];
            if (current == 0)
                break;

            buffer[read++] = (char)current;
        }

        return new string(buffer, 0, read);
    }

    void ReadCLIHeader()
    {
        MoveTo(cli);

        // - CLIHeader

        // Cb						4
        // MajorRuntimeVersion		2
        // MinorRuntimeVersion		2
        Advance(8);

        // Metadata					8
        metadata = ReadDataDirectory();
        // Flags					4
        imageDefinition.Attributes = (ModuleAttributes)binaryReader.ReadUInt32();
        // EntryPointToken			4
        imageDefinition.EntryPointToken = binaryReader.ReadUInt32();
        // Resources				8
        imageDefinition.Resources = ReadDataDirectory();
        // StrongNameSignature		8
        // CodeManagerTable			8
        // VTableFixups				8
        // ExportAddressTableJumps	8
        // ManagedNativeHeader		8
    }
    protected DataDirectory ReadDataDirectory()
    {
        return new DataDirectory(binaryReader.ReadUInt32(), binaryReader.ReadUInt32());
    }
    void ReadOptionalHeaders(out ushort subsystem)
    {
        // - PEOptionalHeader
        //   - StandardFieldsHeader

        // Magic				2
        var pe64 = binaryReader.ReadUInt16() == 0x20b;

        //						pe32 || pe64

        // LMajor				1
        // LMinor				1
        // CodeSize				4
        // InitializedDataSize	4
        // UninitializedDataSize4
        // EntryPointRVA		4
        // BaseOfCode			4
        // BaseOfData			4 || 0

        //   - NTSpecificFieldsHeader

        // ImageBase			4 || 8
        // SectionAlignment		4
        // FileAlignement		4
        // OSMajor				2
        // OSMinor				2
        // UserMajor			2
        // UserMinor			2
        // SubSysMajor			2
        // SubSysMinor			2
        // Reserved				4
        // ImageSize			4
        // HeaderSize			4
        // FileChecksum			4
        Advance(66);

        // SubSystem			2
        subsystem =binaryReader.ReadUInt16();

        // DLLFlags				2
        // StackReserveSize		4 || 8
        // StackCommitSize		4 || 8
        // HeapReserveSize		4 || 8
        // HeapCommitSize		4 || 8
        // LoaderFlags			4
        // NumberOfDataDir		4

        //   - DataDirectoriesHeader

        // ExportTable			8
        // ImportTable			8
        // ResourceTable		8
        // ExceptionTable		8
        // CertificateTable		8
        // BaseRelocationTable	8

        Advance(pe64 ? 90 : 74);

        // Debug				8
        imageDefinition.Debug = ReadDataDirectory();

        // Copyright			8
        // GlobalPtr			8
        // TLSTable				8
        // LoadConfigTable		8
        // BoundImport			8
        // IAT					8
        // DelayImportDescriptor8
        Advance(56);

        // CLIHeader			8
        cli = ReadDataDirectory();

        if (cli.IsZero)
            throw new BadImageFormatException();

        // Reserved				8
        Advance(8);
    }

    static ModuleKind GetModuleKind(ushort characteristics, ushort subsystem)
    {
        if ((characteristics & 0x2000) != 0) // ImageCharacteristics.Dll
            return ModuleKind.Dll;

        if (subsystem == 0x2 || subsystem == 0x9) // SubSystem.WindowsGui || SubSystem.WindowsCeGui
            return ModuleKind.Windows;

        return ModuleKind.Console;
    }

    TargetArchitecture ReadArchitecture()
    {
        var machine =binaryReader.ReadUInt16();
        switch (machine)
        {
            case 0x014c:
                return TargetArchitecture.I386;
            case 0x8664:
                return TargetArchitecture.AMD64;
            case 0x0200:
                return TargetArchitecture.IA64;
        }

        throw new NotSupportedException();
    }
     void Advance(int bytes)
    {
        stream.Seek(bytes, SeekOrigin.Current);
    }
     void MoveTo(uint position)
     {
         stream.Position = position;
     }
    public void Dispose()
    {
        if (stream != null)
        {
            stream.Dispose();
        }
        if (binaryReader != null)
        {
            binaryReader.Dispose();
        }
    }

}