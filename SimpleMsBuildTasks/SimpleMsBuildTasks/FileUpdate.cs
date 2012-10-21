using System;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Build.Utilities;

namespace SimpleMsBuildTasks
{
    public class FileUpdate : Task
    {

        public string File{get; set; }
        public string Regex { get; set; }
        public string ReplacementText { get; set; }

        public override bool Execute()
        {
    
            var regex = new Regex(this.Regex);
            try
            {

                Log.LogMessage("Updating File \"{0}\".", File);
                string input = System.IO.File.ReadAllText(File, Encoding.UTF8);
                if (!regex.IsMatch(input))
                {
                    Log.LogWarning(string.Format("No updates were performed on file : {0}.", File));
                }
                input = regex.Replace(input, this.ReplacementText);
                System.IO.File.WriteAllText(File, input, Encoding.UTF8);
            }
            catch (Exception exception)
            {
                Log.LogErrorFromException(exception);
                return false;
            }
            return true;
        }

    }
}