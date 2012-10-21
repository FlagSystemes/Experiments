using Nancy;
using TinyIoC;

namespace NancyCompression
{
    public class BootstrapperEx : DefaultNancyBootstrapper
    {
        protected override void InitialiseInternal(TinyIoCContainer container)
        {
            base.InitialiseInternal(container);

            this.RegisterCompressionCheck();
        }
    }
}