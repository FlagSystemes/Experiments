using System.Diagnostics;
using NUnit.Framework;

[TestFixture]
public class SubsterTests
{
    [Test]
    public void Simple()
    {
        //place in a using so Subster knows when to un-map the drive
        using (var subster = new Subster(@"D:\LongDirectoryName"))
        {
            Debug.WriteLine(subster.DriveLetter);
            //peform operations against the shorter "DriveLetter"
        }
    }
}