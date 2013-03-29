using System;
using System.Diagnostics;

public static class ModuleInitializer
{
	public static void Initialize()
	{
		Environment.SetEnvironmentVariable("ModuleInitializer", "true");
	}
}