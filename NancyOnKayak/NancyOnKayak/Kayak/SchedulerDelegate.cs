using System;
using Kayak;
using NLog;

public class SchedulerDelegate : ISchedulerDelegate
{
    public Logger Logger;

    public void OnException(IScheduler scheduler, Exception e)
    {
        Logger.ErrorException("Kayak OnException", e);
    }

    public void OnStop(IScheduler scheduler)
    {
        Logger.Info("Kayak Scheduler is stopping.");
    }
}