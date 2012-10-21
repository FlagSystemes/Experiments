using Gate;
using Nancy.Hosting.Owin;

public static class NancyExtensions
{
    public static IAppBuilder RunNancy(this IAppBuilder builder)
    {
        return builder.Run(Delegates.ToDelegate(new NancyOwinHost().ProcessRequest));
    }
}