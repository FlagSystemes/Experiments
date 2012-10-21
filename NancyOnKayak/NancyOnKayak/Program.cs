using System;
using System.Net;
using System.ServiceProcess;
using System.Threading;
using Gate;
using Gate.Kayak;
using Kayak;
using NLog;
using Environment = System.Environment;

class Program : ServiceBase
{
    public static Logger Logger;
    IScheduler scheduler;
    Thread thread;

    static void Main()
    {
        Logger = LoggingFactory.BuildLogger();
        try
        {

            using (var service = new Program())
            {
                if (Environment.UserInteractive) 
                {
                    service.OnStart(null);
                    Console.WriteLine("Press any key to stop program");
                    Console.Read();
                    service.OnStop();
                }
                else
                {
                    Run(service);
                }
            }
        }
        catch (Exception exception)
        {
            Logger.ErrorException("Failed to start ", exception);
        }
        finally
        {
            //TODO: hack to make nlog work with mono. remove when nlog is updated
            LogManager.Configuration = null;
        }
    }


    protected override void OnStart(string[] args)
    {
        thread = new Thread(StartScheduler);

        thread.Start();
    }

    void StartScheduler()
    {
        var schedulerDelegate = new SchedulerDelegate
                                    {
                                        Logger = Logger
                                    };
        scheduler = KayakScheduler.Factory.Create(schedulerDelegate);

        var endPoint1 = new IPEndPoint(IPAddress.Any, 91);
        var appDelegate = AppBuilder.BuildConfiguration(Startup.Configuration);
        using (KayakServer.Factory.CreateGate(appDelegate, scheduler, null)
            .Listen(endPoint1))
        {
            scheduler.Start();
        }
    }

    protected override void OnStop()
    {
        if (scheduler != null)
        {
            scheduler.Stop();
        }
        if (thread != null)
        {
            thread.Join();
        }
    }
}
