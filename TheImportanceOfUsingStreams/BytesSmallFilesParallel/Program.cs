using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {
        HashHelper.ComputeHashWithBytes(@"..\..\..\SmallFile");
        Thread.Sleep(1000);
        var bytesWatch = Stopwatch.StartNew();
        Parallel.For(0, 1000, i => HashHelper.ComputeHashWithBytes(@"..\..\..\SmallFile"));
        bytesWatch.Stop();
        Console.WriteLine(bytesWatch.ElapsedMilliseconds);
    }

}