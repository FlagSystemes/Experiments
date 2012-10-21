using System.Diagnostics;

class Program
{
    static void Main()
    {
        var streamWatch = Stopwatch.StartNew();
        HashHelper.ComputeHashWithStream(@"..\..\..\LargeFile");
        streamWatch.Stop();
        Debug.WriteLine(streamWatch.ElapsedMilliseconds);
    }
}