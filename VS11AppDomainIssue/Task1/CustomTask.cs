using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace TaskLibrary
{
    public class CustomTask:Task
    {
        public override bool Execute()
        {
            var message = "CustomTask version is " + GetType().Assembly.GetName().Version;
            var args = new BuildWarningEventArgs("", "", "", 0, 0, 0, 0, message, "", "CustomTask");
            BuildEngine.LogWarningEvent(args);
            return true;
        }
    }
}
