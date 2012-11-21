using System.Diagnostics;
using NServiceBus;

public class MyMessageHandler:IHandleMessages<MyMessage>
{
    public void Handle(MyMessage message)
    {
        Debug.WriteLine(message.Id);
    }
}