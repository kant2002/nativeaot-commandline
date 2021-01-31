namespace System
{

    public class Console
    {
        public static void WriteLine(string message)
        {
            Write(message);
            Write("\r\n");
        }

        public static unsafe void Write(string c)
        {
            fixed (char* address = c)
                EfiRuntimeHost.SystemTable->ConOut->OutputString(EfiRuntimeHost.SystemTable->ConOut, address);
        }
    }
}