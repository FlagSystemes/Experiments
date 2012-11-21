using NLog;
using log4net.Appender;
using log4net.Core;

public class NLogAppenderForLog4Net : AppenderSkeleton
{

    protected override void Append(LoggingEvent loggingEvent)
    {
        var logger = LogManager.GetLogger(loggingEvent.LoggerName);
        if (loggingEvent.Level == Level.Fatal)
        {
            if (loggingEvent.ExceptionObject != null)
            {
                logger.FatalException(loggingEvent.RenderedMessage, loggingEvent.ExceptionObject);
                return;
            }
            logger.Fatal(loggingEvent.RenderedMessage);
            return;
        }

        if (loggingEvent.Level == Level.Error ||
            loggingEvent.Level == Level.Critical ||
            loggingEvent.Level == Level.Emergency)
        {
            if (loggingEvent.ExceptionObject != null)
            {
                logger.ErrorException(loggingEvent.RenderedMessage, loggingEvent.ExceptionObject);
                return;
            }
            logger.Error(loggingEvent.RenderedMessage);
            return;
        }

        if (loggingEvent.Level == Level.Warn)
        {
            if (loggingEvent.ExceptionObject != null)
            {
                logger.WarnException(loggingEvent.RenderedMessage, loggingEvent.ExceptionObject);
                return;
            }
            logger.Warn(loggingEvent.RenderedMessage);
            return;

        }

        if (loggingEvent.Level == Level.Info || loggingEvent.Level == Level.Notice)
        {
            logger.Info(loggingEvent.RenderedMessage);
            return;
        }
        logger.Trace(loggingEvent.RenderedMessage);
    }
}