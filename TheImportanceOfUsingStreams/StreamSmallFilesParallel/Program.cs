using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {
        HashHelper.ComputeHashWithStream(@"..\..\..\SmallFile");
        Thread.Sleep(1000);
        var stopwatch = Stopwatch.StartNew();
        Parallel.For(0, 1000, i => HashHelper.ComputeHashWithStream(@"..\..\..\SmallFile"));
        stopwatch.Stop();
        Debug.WriteLine(stopwatch.ElapsedMilliseconds);
    }
}