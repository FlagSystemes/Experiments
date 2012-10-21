using System;   
using System.IO;
using NLog;
using NLog.Config;
using NLog.Targets;

static class LoggingFactory
{

    public static Logger BuildLogger()
    {
        var consoleTarget = new ColoredConsoleTarget
                                {
                                    Layout = "${message} ${exception:format=tostring}"
                                };

        var fileName = Path.Combine(Path.GetTempPath(), "NancyOnKayak", "WebsiteLog_${shortdate}.log");
        Console.WriteLine("Logging to:" + fileName);

        var fileTarget = new FileTarget
                             {
                                 FileName = fileName,
                                 Layout = "${longdate} ${message} ${exception:format=tostring}",
                                 AutoFlush = true,
                             };

        var config = new LoggingConfiguration
                         {
                             LoggingRules =
                                 {
                                     new LoggingRule("*", LogLevel.Debug, consoleTarget), 
                                     new LoggingRule("*", LogLevel.Debug, fileTarget),
                                 }
                         };


        config.AddTarget("file", fileTarget);
        config.AddTarget("console", consoleTarget);

        LogManager.Configuration = config;
        return LogManager.GetLogger("");
    }
}