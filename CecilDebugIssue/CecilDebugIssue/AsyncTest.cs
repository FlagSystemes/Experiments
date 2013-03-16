using System.Threading.Tasks;
using NUnit.Framework;

[TestFixture]
public class AsyncTest
{
    [Test]
    public void TestAsynch()
    {
        AsyncDebuggerTest();
    }

    public async void AsyncDebuggerTest()
    {
        var test = await getTestString();
        //set breakpoint on next line and look at 'test' variable in debugger
        var test2 = test + test;
    }

    async Task<string> getTestString()
    {
        return "test";
    }
}