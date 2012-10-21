using Nancy;
using Nancy.Testing;
using NUnit.Framework;
using WithExtension;

namespace NancyUnSafeFiles.Tests
{
    [TestFixture]
    public class WithExtensionTest
    {
        [Test]
        public void Foo()
        {

            // Given
            var bootstrapper = new BootstrapperEx();
            var browser = new Browser(bootstrapper);

            // When
            var result = browser.Get("/", with => with.HttpRequest());

            // Then
            Assert.AreEqual(result.Body.AsString(), "File Content");
            Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);
        }

    }
}