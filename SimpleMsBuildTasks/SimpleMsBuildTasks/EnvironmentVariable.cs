using System;
using System.IO;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace SimpleMsBuildTasks
{
    public class EnvironmentVariable : Task
    {
        public override bool Execute()
        {
            try
            {

            Environment.SetEnvironmentVariable(this.Variable, Value);
             }
            catch (Exception exception)
            {
                Log.LogErrorFromException(exception);
                return false;
            }
            return true;
        }

        public string Value { get; set; }

        public string Variable { get; set; }

    }
}