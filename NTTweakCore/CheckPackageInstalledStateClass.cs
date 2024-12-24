using System.Diagnostics;

namespace NTTweakCore;

public class CheckPackageInstalledStateClass
{
    public bool IsWingetInstalled()
    {
        try
        {
            // Проверяем, доступен ли winget
            var process = Process.Start(new ProcessStartInfo
            {
                FileName = "winget",
                Arguments = "--version",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            });

            process.WaitForExit();
            return process.ExitCode == 0;
        }
        catch (Exception)
        {
            return false;
        }
    }
    
    public bool IsChocolateyInstalled()
    {
        try
        {
            // Проверяем, доступен ли chocolatey
            var process = Process.Start(new ProcessStartInfo
            {
                FileName = "choco",
                Arguments = "--version",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            });

            process.WaitForExit();
            return process.ExitCode == 0;
        }
        catch (Exception)
        {
            return false;
        }
    }
}