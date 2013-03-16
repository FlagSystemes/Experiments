using NUnit.Framework;

[TestFixture]
public class NonAsyncTest
{
    [Test]
    public void TestAsynch()
    {
        AsyncDebuggerTest();
    }

    public void AsyncDebuggerTest()
    {
        var test = getTestString();
        //set breakpoint on next line and look at 'test' variable in debugger
        var test2 = test + test;
    }

    private string getTestString()
    {
        return "test";
    }
}