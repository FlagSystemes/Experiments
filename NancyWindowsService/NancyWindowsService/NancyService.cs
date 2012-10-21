using System;
using System.ServiceProcess;
using Nancy.Hosting.Self;

public class NancyService : ServiceBase
{
    NancyHost host;

    static void Main()
    {
        using (var service = new NancyService())
        {
            if (Environment.UserInteractive)
            {
                service.OnStart(null);
                Console.Write("Press any key to stop program");
                Console.Read();
                service.OnStop();
            }
            else
            {
                Run(service);
            }
        }
    }

    protected override void OnStart(string[] args)
    {
        host = new NancyHost(new Uri("http://localhost:12345"));
        host.Start(); 
    }

    protected override void OnStop()
    {
        host.Stop(); 
    }
}