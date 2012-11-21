using System;
using System.Threading;
using NServiceBus;
using NServiceBus.Installation.Environments;
using NUnit.Framework;

[TestFixture]
public class BusTests
{
    [Test]
    public void SendMessage()
    {
        var bus = Configure.With()
            .DefineEndpointName("fakeNameForTests")
              .DefaultBuilder()
              .JsonSerializer()
              .MsmqTransport()
              .UnicastBus()
              .SendOnly();

        bus.Send("MyEndPoint", new MyMessage(){Id = Guid.NewGuid()});

    }
}

