using NLog;
using NLog.Config;
using NLog.Targets;

public static class LoggingFactory
{

    public static Logger BuildLogger()
    {

    

        var debuggerTarget = new DebuggerTarget
                                 {
                                     Layout = "${longdate} ${message} ${exception:format=tostring}",
                                 };

        var config = new LoggingConfiguration
                         {
                             LoggingRules =
                                 {
                                     new LoggingRule("*", LogLevel.Debug, debuggerTarget),
                                 }
                         };


        config.AddTarget("debug", debuggerTarget);

        LogManager.Configuration = config;
        return LogManager.GetLogger("");
    }

}