using NSubstitute;
using NUnit.Framework;

[TestFixture]
public class NSubTesttClass
{
    [Test]
    public void MockMultipleParams()
    {
        var mockInstance = Substitute.For<ClassToBeMocked>(null, null);
    }

    [Test]
    public void MockSingleParam()
    {
        var mockInstance = Substitute.For<ClassToBeMocked>(null);
    }

    [Test]
    public void MockSingleParamWithArray()
    {
        Substitute.For<ClassToBeMocked>(new object[] {null});
    }

    [Test]
    public void MockSingleParamWithCast()
    {
        Substitute.For<ClassToBeMocked>((object)null);
    }

}