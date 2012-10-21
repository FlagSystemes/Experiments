using System.IO;
using System.Reflection;
using NUnit.Framework;

[TestFixture]
public class RoboDeleteTests
{
    [Test]
    public void Simple()
    {
        var codebase = Assembly.GetExecutingAssembly().CodeBase.Replace("file:///","");
        var directoryToDelete = Path.Combine(Path.GetDirectoryName(codebase), "DirectoryToDelete");
        RoboDelete.Delete(directoryToDelete);
        Assert.IsFalse(Directory.Exists(directoryToDelete));
    }
}