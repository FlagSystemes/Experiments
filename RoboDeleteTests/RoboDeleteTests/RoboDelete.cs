    using System.Diagnostics;
    using System.IO;

public static class RoboDelete
{
    public static void Delete(string path)
    {
        DirectoryInfo emptyTempDirectory = null;

        try
        {
            var tempDirPath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            emptyTempDirectory = Directory.CreateDirectory(tempDirPath);
            using (var process = StartProcess(path, emptyTempDirectory))
            {
                process.WaitForExit();
            }
            Directory.Delete(path);
        }
        finally
        {
            if (emptyTempDirectory != null)
            {
                emptyTempDirectory.Delete();
            }
        }
    }

    static Process StartProcess(string path, DirectoryInfo emptyTempDirectory)
    {
        // /W:n :: Wait time between retries: default is 30 seconds.
        // /R:n :: number of Retries on failed copies: default 1 million.
        // /FFT :: assume FAT File Times (2-second granularity).
        // /MIR :: MIRror a directory tree (equivalent to /E plus /PURGE).
        // /NFL :: No File List - don't log file names.
        var arguments = string.Format("\"{0}\" \"{1}\" /W:1  /R:1 /FFT /MIR /NFL", emptyTempDirectory.FullName, path);
        var startInfo = new ProcessStartInfo("robocopy")
                            {
                                Arguments = arguments,
                                WindowStyle = ProcessWindowStyle.Hidden,
                                CreateNoWindow = true
                            };
        return Process.Start(startInfo);
    }
}