using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Nancy;
using Nancy.Bootstrapper;

public class NancyNotModified : IStartup
{

    public void Initialize(IPipelines pipelines)
    {
        pipelines.AfterRequest.AddItemToStartOfPipeline(CheckForCached);
    }

    static void CheckForCached(NancyContext context)
    {
        var responseHeaders = context.Response.Headers;
        var requestHeaders = context.Request.Headers;

        string currentFileEtag;
        if (responseHeaders.TryGetValue("ETag", out currentFileEtag))
        {
            if (requestHeaders.IfNoneMatch.Contains(currentFileEtag))
            {
                context.Response = HttpStatusCode.NotModified;
                return;
            }
        }

        string responseLastModifiedString;
        if (responseHeaders.TryGetValue("Last-Modified", out responseLastModifiedString))
        {
            var responseLastModified = DateTime.ParseExact(responseLastModifiedString, "R", CultureInfo.InvariantCulture);
            if (responseLastModified <= requestHeaders.IfModifiedSince)
            {
                context.Response = HttpStatusCode.NotModified;
            }
        }
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