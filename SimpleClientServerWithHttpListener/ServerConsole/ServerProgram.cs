using System;
using System.Diagnostics;
using System.Net;
using System.Threading;

namespace ServerConsole
{
    static class ServerProgram
    {
        static HttpListener listener;

        static void Main()
        {
            Console.WriteLine("Server");
            listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:7896/");
            listener.AuthenticationSchemes = AuthenticationSchemes.Anonymous;

            listener.Start();
            while (true)
            {
                ProcessRequest();
            }

        }

        static void ProcessRequest()
        {
            var startNew = Stopwatch.StartNew();
            var result = listener.BeginGetContext(ListenerCallback, listener);
            result.AsyncWaitHandle.WaitOne();
            startNew.Stop();
        }

        static void ListenerCallback(IAsyncResult result)
        {
            var context = listener.EndGetContext(result);
            Thread.Sleep(1000);
            context.Response.StatusCode = 200;
            context.Response.StatusDescription = "OK";
            var receivedText = context.Request.Headers["thread"] + " Received";
            Console.WriteLine("Server: " + receivedText);
            context.Response.Headers["thread"] = receivedText;
            context.Response.Close();
        }

    }
}