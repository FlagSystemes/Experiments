using System;
using System.Collections.Generic;
using NLog;
using Nancy;
using Nancy.Bootstrapper;


public class ExceptionHandler : IStartup
{
    Logger logger;

    public ExceptionHandler(Logger logger)
    {
        this.logger = logger;
    }


    public void Initialize(IPipelines pipelines)
    {
        pipelines.OnError.AddItemToStartOfPipeline(LogError);
    }

    Response LogError(NancyContext context, Exception exception)
    {
        var message = string.Format("Error processing {0}", context.Request.Path);
        logger.ErrorException(message, exception);
        return context.Response;
    }

    public IEnumerable<TypeRegistration> TypeRegistrations
    {
        get { return null; }
    }

    public IEnumerable<CollectionTypeRegistration> CollectionTypeRegistrations
    {
        get { return null; }
    }

    public IEnumerable<InstanceRegistration> InstanceRegistrations
    {
        get { return null; }
    }
}