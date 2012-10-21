using System.Diagnostics;

class Program
{
    static void Main()
    {
        var stopwatch = Stopwatch.StartNew();
        HashHelper.ComputeHashWithBytes(@"..\..\..\LargeFile");
        stopwatch.Stop();
        Debug.WriteLine(stopwatch.ElapsedMilliseconds);
    }

}