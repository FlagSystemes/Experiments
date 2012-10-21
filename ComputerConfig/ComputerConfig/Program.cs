using System;
using System.Diagnostics;

namespace ComputerConfig
{
	class Program
	{
		static void Main()
		{
			DisableService("DPS");
			DisableService("WdiServiceHost");
			DisableService("WdiSystemHost");
			DisableService("ShellHWDetection");
			DisableService("stisvc");
		}

		static void DisableService(string service)
		{
			var processStartInfo = GetProcessStartInfo("sc", string.Format("config \"{0}\" start= disabled", service));
			var line = RunProcess(processStartInfo);
			if (line.Contains("ERROR") || line.Contains("FAILED"))
			{
				throw new Exception("SC failed\r\n" + line);
			}
		}

		static ProcessStartInfo GetProcessStartInfo(string exe,string arguments)
		{
			return new ProcessStartInfo(exe, arguments)
			       	{
			       		CreateNoWindow = true,
			       		UseShellExecute = false,
			       		RedirectStandardOutput = true,
			       	};
		}

		static string RunProcess(ProcessStartInfo startInfo)
		{
			using (var process = Process.Start(startInfo))
			{
				var line = process.StandardOutput.ReadToEnd();
				process.WaitForExit(1000);
				return line;
			}
		}
	}
}
