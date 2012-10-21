using System;
using System.Diagnostics;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace ClientConsole
{
    static class ClientProgram
    {

        static void Main()
        {
            Console.WriteLine("Client");
            //Wait for serer to start
            Thread.Sleep(1000);
            var startNew = Stopwatch.StartNew();
            var calls = 100;
            var result = Parallel.For(0, calls, CallServer);
            while (!result.IsCompleted)
            {
                Thread.Sleep(100);
            }
            startNew.Stop();
            Console.WriteLine("Client finished {0}x1sec calls in {1}sec", calls, startNew.Elapsed.Seconds);

            Console.ReadLine();

        }

        private static void CallServer(int i)
        {
            var webRequest = WebRequest.Create("http://localhost:7896/");
            webRequest.Headers["thread"] = i.ToString();
            using (var webResponse = webRequest.GetResponse())
            {
                Console.WriteLine("Client: " + webResponse.Headers["thread"]);
            }
        }
    }
}