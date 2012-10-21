using Nancy;

public class MainModule : NancyModule
{
    public MainModule()
    {
        Get["/"] = x => "Hello World";
    }
}