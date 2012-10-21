using System.Diagnostics;
using System.Threading;

class Program
{
    static void Main()
    {
        HashHelper.ComputeHashWithStream(@"..\..\..\SmallFile");
        Thread.Sleep(1000);
        var stopwatch = Stopwatch.StartNew();
        for (var i = 0; i < 1000; i++)
        {
            HashHelper.ComputeHashWithStream(@"..\..\..\SmallFile");
        }
        stopwatch.Stop();
        Debug.WriteLine(stopwatch.ElapsedMilliseconds);
    }
}