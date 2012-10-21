using System;
using Nancy.Hosting.Self;

namespace NancyEmbeddedViews
{
    class Program
    {
        static void Main()
        {
            var host = new NancyHost(new Uri("http://localhost:12345"));
            host.Start();
            Console.Write("Press any key to stop program");
            Console.Read();
            host.Stop(); 
        }

    }
}
