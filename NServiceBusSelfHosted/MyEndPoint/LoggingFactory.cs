using NLog;
using NLog.Config;
using NLog.Targets;

public static class LoggingFactory
{

    public static void BuildLogger()
    {
        var consoleTarget = new ColoredConsoleTarget
            {
                Layout = "${message} ${exception:format=tostring}"
            };

        var debuggerTarget = new DebuggerTarget()
        {
            Layout = "${message} ${exception:format=tostring}"
        };

        var consoleRule = new LoggingRule("*", LogLevel.Debug, consoleTarget);
        var debuggerRule = new LoggingRule("*", LogLevel.Debug, debuggerTarget);

        var config = new LoggingConfiguration();

        config.LoggingRules.Add(consoleRule);
        config.LoggingRules.Add(debuggerRule);

        config.AddTarget("debugger", debuggerTarget);
        config.AddTarget("console", consoleTarget);

        LogManager.Configuration = config;
    }
}