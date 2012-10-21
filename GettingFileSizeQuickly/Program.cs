using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using NUnit.Framework;

namespace GettingFileSizeQuickly
{
    [TestFixture]
    public class Tests
    {

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern int GetFileSize(IntPtr hFile, out int lpFileSize);


        [DllImport("kernel32.dll", SetLastError = true)]
        static extern SafeFileHandle CreateFile(string lpFileName, [MarshalAs(UnmanagedType.U4)] FileAccess dwDesiredAccess,
           [MarshalAs(UnmanagedType.U4)] FileShare dwShareMode, IntPtr lpSecurityAttributes, [MarshalAs(UnmanagedType.U4)] FileMode dwCreationDisposition,
           [MarshalAs(UnmanagedType.U4)] FileAttributes dwFlagsAndAttributes, IntPtr hTemplateFile);


        int iterations = 10000;
        string path = @"\\DESKTOP\Users\Simon\Downloads\010IntroToDevForce.zip";

        [Test]
        public void FileInfo()
        {
            var length = new FileInfo(path).Length;
            var stopwatch = Stopwatch.StartNew();
            for (var i = 0; i < iterations; i++)
            {
                length = new FileInfo(path).Length;
            }
            stopwatch.Stop();
            Debug.WriteLine((double)stopwatch.ElapsedMilliseconds/iterations);
        }
        [Test]
        public void FileStream()
        {
            long length;
            using (var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                length = fileStream.Length;
            }
            var stopwatch = Stopwatch.StartNew();
            for (var i = 0; i < iterations; i++)
            {
                using (var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    length = fileStream.Length;
                }
            }
            stopwatch.Stop();
            Debug.WriteLine((double)stopwatch.ElapsedMilliseconds/iterations);
        }
        [Test]
        public void PInvoke()
        {
            var length = GetFileSize(path);
            var stopwatch = Stopwatch.StartNew();
            for (var i = 0; i < iterations; i++)
            {
                    length = GetFileSize(path);
            }
            stopwatch.Stop();
            Debug.WriteLine((double)stopwatch.ElapsedMilliseconds/iterations);
        }


        public static long GetFileSize(string filePath)
        {
            int highSize;
            int fileSize;
            using (var handle = CreateFile(filePath, FileAccess.Read, FileShare.Read, IntPtr.Zero, FileMode.Open, FileAttributes.ReadOnly, IntPtr.Zero))
            {
                if (handle.IsInvalid)
                {
                    throw new Win32Exception();
                }
                fileSize = GetFileSize(handle.DangerousGetHandle(), out highSize);
            }
            if (fileSize == -1)
            {
                var errorCode = Marshal.GetLastWin32Error();
                if (errorCode != 0)
                {
                    throw new Win32Exception();
                }
            }
            return (highSize << 0x20) | ((long)(fileSize));
        }
    }
}
