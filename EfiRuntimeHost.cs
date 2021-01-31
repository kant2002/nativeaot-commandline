using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

public unsafe static class EfiRuntimeHost
{
    public static EFI_SYSTEM_TABLE* SystemTable { get; private set; }

    public static void Initialize(IntPtr imageHandle, EFI_SYSTEM_TABLE* systemTable)
    {
        SystemTable = systemTable;
    }

    [MethodImpl(MethodImplOptions.InternalCall)]
    [System.Runtime.RuntimeImport("*", "__managed__Main")]
    private static extern int ManagedMain(int argc, IntPtr argv);

    [System.Runtime.RuntimeExport("EfiMain")]
    static unsafe void EfiMain(IntPtr imageHandle, EFI_SYSTEM_TABLE* systemTable)
    {
        EfiRuntimeHost.Initialize(imageHandle, systemTable);

        ManagedMain(0, default);

        // Debug break to EFI runtime does not restart.
        while (true)
        {
        }
    }
}

[StructLayout(LayoutKind.Sequential)]
public unsafe readonly struct EFI_SYSTEM_TABLE
{
    public readonly EFI_TABLE_HEADER Hdr;
    public readonly char* FirmwareVendor;
    public readonly int FirmwareRevision;
    public readonly IntPtr ConsoleInHandle;
    public readonly void* ConIn;
    public readonly IntPtr ConsoleOutHandle;
    public readonly EFI_SIMPLE_TEXT_OUTPUT_PROTOCOL* ConOut;
    public readonly IntPtr StandardErrorHandle;
    public readonly void* StdErr;
    public readonly void* RuntimeServices;
    public readonly void* BootServices;
    public readonly ulong NumberOfTableEntries;
    public readonly void* ConfigurationTable;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe readonly struct EFI_SIMPLE_TEXT_OUTPUT_PROTOCOL
{
    private readonly IntPtr _reset;
    private readonly IntPtr _OutputString;
    private readonly IntPtr _testString;
    private readonly IntPtr _queryMode;
    private readonly IntPtr _setMode;
    private readonly IntPtr _setAttribute;
    private readonly IntPtr _clearScreen;
    private readonly IntPtr _setCursorPosition;
    private readonly IntPtr _enableCursor;

    public readonly IntPtr Mode;

    public ulong OutputString(void* handle, char* str)
    {
        return RawCalliHelper.StdCall(_OutputString, (byte*)handle, str);
    }
}

[StructLayout(LayoutKind.Sequential)]
public readonly struct EFI_TABLE_HEADER
{
    public readonly ulong Signature;
    public readonly int Revision;
    public readonly int HeaderSize;
    public readonly int Crc32;
    public readonly int Reserved;
}