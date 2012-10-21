using NSubstitute;
using NUnit.Framework;

[TestFixture]
public class TestForMockingSealedClass
{

    [Test]
    public void Simple()
    {
        var sealedClass = Substitute.For<SealedClass>();
        sealedClass.Method().Returns("HelloFromMock");
        Assert.AreEqual("HelloFromMock", sealedClass.Method());
    }
}