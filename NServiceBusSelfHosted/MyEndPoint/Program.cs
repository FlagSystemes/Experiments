using System;
using System.Collections.Generic;
using System.Reflection;
using System.ServiceProcess;
using NServiceBus;
using NServiceBus.Installation.Environments;
using NServiceBus.Unicast;

public class Program : ServiceBase
{
    static void Main()
    {
        using (var service = new Program())
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
        LoggingFactory.BuildLogger();
        BuildBus();
    }

    public static IBus BuildBus()
    {
        return Configure.With()
            .CustomConfigurationSource(new CustomConfigurationSource())
            .DefineEndpointName("MyEndPoint")
            .DefaultBuilder()
            .Sagas()
            .MsmqTransport()
                .IsTransactional(true)
                .PurgeOnStartup(false)
            .RunTimeoutManager()
            .UnicastBus()
                .LoadMessageHandlers()
                .ImpersonateSender(true)
            .JsonSerializer()
                     .Log4Net<NLogAppenderForLog4Net>(x => { })
            .CreateBus()
                .Start(() => Configure.Instance.ForInstallationOn<Windows>().Install());
    }


    protected override void OnStop()
    {
    }

    public static IEnumerable<Assembly> GetAssembliesToParse()
    {
        yield return typeof(MyMessage).Assembly;
        yield return typeof(UnicastBus).Assembly;
        yield return typeof(IBus).Assembly;
    }
}

