using System.Diagnostics;
using System.IO;
using System.Net;
using NUnit.Framework;

namespace NancyCompression.Tests
{
    [TestFixture]
    public class IntegrationTest
    {
        
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
            Call(url, true);

            var notCompressed = Stopwatch.StartNew();
            Call(url, false);
            notCompressed.Stop();
            Debug.WriteLine(notCompressed.ElapsedMilliseconds);

            var compressed = Stopwatch.StartNew();
            Call(url, true);
            compressed.Stop();
            Debug.WriteLine(compressed.ElapsedMilliseconds);

        }

        void Call(string url, bool compress)
        {
            var webRequest = WebRequest.Create(url);
            if (compress)
            {
                webRequest.Headers.Add("Accept-Encoding", "gzip");
            }
            using (var webResponse = webRequest.GetResponse())
            using (var responseStream = webResponse.GetResponseStream())
            using (var destination = new MemoryStream())
            {
                responseStream.CopyTo(destination);
            }
        }

    }
}
