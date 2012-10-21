using System.Net;
using NUnit.Framework;

[TestFixture]
public class IntegrationTest
{
    [Test]
    public void CallNancyService()
    {
        using (var webClient = new WebClient())
        {
            var downloadString = webClient.DownloadString("http://localhost:12345");
            Assert.AreEqual("Hello World", downloadString);
        }
    }
}
