using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using NUnit.Framework;

namespace NancyConditionalResponses.Tests
{
    [TestFixture]
    public class IntegrationTest
    {
        [Test]
        public void ETagTest()
        {
            var webRequest1 = WebRequest.Create("http://localhost:12345/LargeFile");

            string etag;
            using (var webResponse = webRequest1.GetResponse())
            {
                etag = webResponse.Headers["ETag"];
                AssertThatFileIsInResponse(webResponse);
            }

            var webRequest2 = WebRequest.Create("http://localhost:12345/LargeFile");
            webRequest2.Headers.Add("If-None-Match", etag);
            AssertThatResponseIs304(webRequest2);
        }
        
        [Test]
        public void PerfLargeFileTest()
        {
            RunPerf("http://localhost:12345/LargeFile");
        }

        [Test]
        public void PerfMediumFileTest()
        {
            RunPerf("http://localhost:12345/MediumFile");
        }

        [Test]
        public void PerfSmallFileTest()
        {
            RunPerf("http://localhost:12345/SmallFile");
        }

        void RunPerf(string url)
        {
            CallOnceToWarmUp(url);
            var webRequest1 = WebRequest.Create(url);
            string etag;
            var notCached = Stopwatch.StartNew();
            using (var webResponse = webRequest1.GetResponse())
            {
                etag = webResponse.Headers["ETag"];
                using (var responseStream = webResponse.GetResponseStream())
                using (var destination = new MemoryStream())
                {
                    responseStream.CopyTo(destination);
                }
            }
            notCached.Stop();
            Debug.WriteLine(notCached.ElapsedMilliseconds);

            var webRequest2 = (HttpWebRequest)WebRequest.Create(url);
           webRequest2.Headers.Add("If-None-Match", etag);
            var cached = Stopwatch.StartNew();
            try
            {
                using (var webResponse = webRequest2.GetResponse())
                {
                    using (var responseStream = webResponse.GetResponseStream())
                    using (var destination = new MemoryStream())
                    {
                        responseStream.CopyTo(destination);
                    }
                }
            }
            catch (WebException)
            {
            }
            cached.Stop();

            Debug.WriteLine(cached.ElapsedMilliseconds);
        }

        void CallOnceToWarmUp(string url)
        {
            var webRequest1 = WebRequest.Create(url);
            using (var webResponse = webRequest1.GetResponse())
            using (var responseStream = webResponse.GetResponseStream())
            using (var destination = new MemoryStream())
            {
                responseStream.CopyTo(destination);
            }
        }


        [Test]
        public void LastModifiedTest()
        {
            var webRequest1 = WebRequest.Create("http://localhost:12345/LargeFile");

            string lastModifed;
            using (var webResponse = webRequest1.GetResponse())
            {
                lastModifed = webResponse.Headers["Last-Modified"];
                AssertThatFileIsInResponse(webResponse);
            }

            //Need to cast to HttpWebRequest so I have access to HttpWebRequest.IfModifiedSince
            var webRequest2 = (HttpWebRequest)WebRequest.Create("http://localhost:12345/LargeFile");
            //Have to use HttpWebRequest.IfModifiedSince because it is considered a "protected" header
            webRequest2.IfModifiedSince = DateTime.Parse(lastModifed);
            AssertThatResponseIs304(webRequest2);
        }

        void AssertThatFileIsInResponse(WebResponse webResponse)
        {
            using (var responseStream = webResponse.GetResponseStream())
            using (var destination = new MemoryStream())
            {
                responseStream.CopyTo(destination);
                Assert.Greater(destination.Length, 20000000);
            }
        }

        void AssertThatResponseIs304(WebRequest webRequest2)
        {
            WebException webException = null;
            try
            {
                webRequest2.GetResponse();
            }
            catch (WebException exception)
            {
                webException = exception;
            }
            Assert.IsNotNull(webException);
            Assert.AreEqual(HttpStatusCode.NotModified, ((HttpWebResponse)(webException.Response)).StatusCode);
        }

    }
}
