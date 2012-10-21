using System.Diagnostics;
using NUnit.Framework;

[TestFixture]
public class ParamsProof
{
    [Test]
    public void TestMethod()
    {
        MethodWithParams("one", "two");
        MethodWithParams("one");
        MethodWithParams();
        MethodWithParams(null, null);
        MethodWithParams((object) null);
        MethodWithParams(new object[] {null});
        MethodWithParams(null);
    }

    static void MethodWithParams(params object[] parameters)
    {
        if (parameters == null)
        {
            Debug.WriteLine("parameters is null");
            return;
        }

        Debug.WriteLine("parameters length is " + parameters.Length);
    }
}