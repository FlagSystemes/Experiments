using System;
using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length != 2)
        {
            Console.WriteLine("Invalid argument count");
            return;
        }
        var machineName = args[1];
        var option = args[0];
        if (string.Equals(option, "install", StringComparison.InvariantCultureIgnoreCase))
        {
            Install(machineName);
            return;
        }
        if (string.Equals(option, "uninstall", StringComparison.InvariantCultureIgnoreCase))
        {
            UnInstall(machineName);
            return;
        }
        throw new Exception("Invalid Option");
    }

    static void Install(string machineName)
    {
        Process.Start("sc")
    }
    static void UnInstall(string machineName)
    {
    }
}