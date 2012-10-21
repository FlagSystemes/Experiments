using Nancy;
using Nancy.Bootstrapper;
using Nancy.ViewEngines;
using TinyIoC;

namespace NancyEmbeddedViews
{
    public class MyBootstrapper : DefaultNancyBootstrapper 
    {
        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);
            //This should be the assembly your views are embedded in
            var assembly = GetType().Assembly;
            ResourceViewLocationProvider
                .RootNamespaces
                //TODO: replace NancyEmbeddedViews.MyViews with your resource prefix
                .Add(assembly, "NancyEmbeddedViews.MyViews");
        }

        protected override NancyInternalConfiguration InternalConfiguration
        {
            get
            {
                return NancyInternalConfiguration.WithOverrides(OnConfigurationBuilder);
            }
        }

        void OnConfigurationBuilder(NancyInternalConfiguration x)
        {
            x.ViewLocationProvider = typeof (ResourceViewLocationProvider);
        }
    }
}