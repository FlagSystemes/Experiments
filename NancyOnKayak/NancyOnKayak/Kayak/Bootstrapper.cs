using NLog;
using Nancy;
using TinyIoC;

public class Bootstrapper : DefaultNancyBootstrapper
{
    protected override void ConfigureApplicationContainer(TinyIoCContainer container)
    {
        base.ConfigureApplicationContainer(container);
        container.Register(typeof(Logger), Program.Logger);
#if DEBUG
        StaticConfiguration.DisableCaches = true;
#endif
    }


}