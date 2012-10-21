using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

    public class Subster : IDisposable
    {
        char? driveLetter;

        public char DriveLetter
        {
            get { return driveLetter.Value; }
        }

        public Subster(string path)
        {
            driveLetter = GetDriveLetter();
            AddSubst(path);
        }

        char GetDriveLetter()
        {
            var existingDrives = DriveInfo.GetDrives().Select(x => x.Name[0]).ToList();
            for (var letter = 'A'; letter <= 'Z'; letter++)
            {
                var tempDriveLetter = letter;
                if (!existingDrives.Contains(tempDriveLetter))
                {
                    return tempDriveLetter;
                }
            }
            throw new Exception("All drive letters reserved.");
        }


        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool DefineDosDevice(uint dwFlags, string lpDeviceName, string lpTargetPath);

        void AddSubst(string path)
        {
            if (!DefineDosDevice(0, driveLetter + ":", path))
            {
                throw new Win32Exception();
            }
        }

        void RemoveSubst(char driveLetter)
        {
            if (!DefineDosDevice(0x00000002, driveLetter + ":", null))
            {
                throw new Win32Exception();
            }
        }

        public void Dispose()
        {
            if (driveLetter != null)
            {
                RemoveSubst(driveLetter.Value);
            }
        }
    }