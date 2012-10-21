using System;
using System.Diagnostics;
using System.Threading;

class Program
{
    static void Main()
    {
        HashHelper.ComputeHashWithBytes(@"..\..\..\SmallFile");
        Thread.Sleep(1000);
        var stopwatch = Stopwatch.StartNew();
        for (var i = 0; i < 1000; i++)
        {
            HashHelper.ComputeHashWithBytes(@"..\..\..\SmallFile");
        }
        stopwatch.Stop();
        Console.WriteLine(stopwatch.ElapsedMilliseconds);
    }

}