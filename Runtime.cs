namespace System
{
    public class Object
    {
        // The layout of object is a contract with the compiler.
        public IntPtr m_pEEType;
    }
    public struct Void { }
    public struct Byte { }
    public struct SByte { }
    public struct Int16 { }
    public struct Int32 { }
    public struct Boolean { }
    public struct Char { }
    public struct UInt64 { }
    public struct IntPtr{}
    public abstract class ValueType { }
    public abstract class Enum : ValueType { }
    public sealed class String { public readonly int Length; }
    public class Attribute { }
    public abstract class Array
    {
#pragma warning disable 649
        // This field should be the first field in Array as the runtime/compilers depend on it
        internal int _numComponents;
#pragma warning restore
    }

    class Array<T> : Array { }
}

namespace System.Runtime
{
    public sealed class RuntimeExportAttribute : Attribute
    {
        public RuntimeExportAttribute(string entry) { }
    }

    public sealed class RuntimeImportAttribute : Attribute
    {
        public string DllName { get; }
        public string EntryPoint { get; }

        public RuntimeImportAttribute(string entry)
        {
            EntryPoint = entry;
        }

        public RuntimeImportAttribute(string dllName, string entry)
        {
            EntryPoint = entry;
            DllName = dllName;
        }
    }
}

[System.Runtime.InteropServices.McgIntrinsicsAttribute]
internal class RawCalliHelper
{
    public static unsafe ulong StdCall<T, U>(System.IntPtr pfn, T* arg1, U* arg2) where T : unmanaged where U : unmanaged
    {
        // This will be filled in by an IL transform
        return 0;
    }
}

namespace System.Runtime.InteropServices
{
    sealed class McgIntrinsicsAttribute : Attribute { }

    // Required for unmanaged constraint
    public class UnmanagedType { }

    sealed class StructLayoutAttribute : Attribute
    {
        public StructLayoutAttribute(LayoutKind layoutKind)
        {
            Value = layoutKind;
        }

        public LayoutKind Value;
        /*public int Pack;
        public int Size;
        public CharSet CharSet;*/
    }

    internal enum LayoutKind
    {
        Sequential = 0, // 0x00000008,
        Explicit = 2, // 0x00000010,
        Auto = 3, // 0x00000000,
    }
}

namespace System.Runtime.CompilerServices
{
    public enum MethodImplOptions
    {
        Unmanaged = 0x0004,
        NoInlining = 0x0008,
        ForwardRef = 0x0010,
        Synchronized = 0x0020,
        NoOptimization = 0x0040,
        PreserveSig = 0x0080,
        AggressiveInlining = 0x0100,
        AggressiveOptimization = 0x0200,
        InternalCall = 0x1000
    }

    public sealed class MethodImplAttribute : Attribute
    {
        // public MethodCodeType MethodCodeType;

        public MethodImplAttribute(MethodImplOptions methodImplOptions)
        {
            Value = methodImplOptions;
        }

        public MethodImplAttribute(short value)
        {
            Value = (MethodImplOptions)value;
        }

        public MethodImplAttribute()
        {
        }

        public MethodImplOptions Value { get; }
    }

    public class RuntimeHelpers
    {
        public static unsafe int OffsetToStringData => sizeof(IntPtr) + sizeof(int);
    }
}

namespace Internal.Runtime.CompilerHelpers
{
    using System;
    using System.Runtime;

    internal partial class StartupCodeHelpers
    {
        [RuntimeExport("RhpReversePInvoke2")]
        static void RhpReversePInvoke2() { }
        [RuntimeExport("RhpReversePInvokeReturn2")]
        static void RhpReversePInvokeReturn2() { }
        [System.Runtime.RuntimeExport("__fail_fast")]
        static void FailFast() 
        {
            Console.WriteLine("Fail fast.");
            while (true) ;
        }
        [System.Runtime.RuntimeExport("RhpPInvoke")]
        static void RphPinvoke() { }
        [System.Runtime.RuntimeExport("RhpPInvokeReturn")]
        static void RphPinvokeReturn() { }
        [System.Runtime.RuntimeExport("RhpThrowEx")]
        static void RhpThrowEx(IntPtr ex)
        {
        }
    }

    internal unsafe partial class StartupCodeHelpers
    {
        internal static unsafe void InitializeCommandLineArgsW(int argc, char** argv)
        {
        }

        internal static unsafe void InitializeCommandLineArgs(int argc, sbyte** argv)
        {
        }

        private static string[] GetMainMethodArguments()
        {
            return null;
        }

        private static void SetLatchedExitCode(int exitCode)
        {
        }

        // Shuts down the class library and returns the process exit code.
        private static int Shutdown()
        {
            return 0;
        }
    }
}
