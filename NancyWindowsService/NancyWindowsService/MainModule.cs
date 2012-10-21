using Nancy;

public class MainModule : NancyModule
{
    public MainModule()
    {
        Get["/"] = x => { return "Hello World"; };
    }
}