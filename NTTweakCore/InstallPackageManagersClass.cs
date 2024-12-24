using System.Diagnostics;

namespace NTTweakCore;

public class InstallPackageManagersClass
{
    public async void InstallWinget()
    {
        try
        {
            // winget можно установить через Microsoft Store, если он не установлен.
            Console.WriteLine("WinGet не был обнаружен на вашем устройстве. Перекидываю вас на ссылку установки. При установке убедитесь, что у вас стоит Microsoft Store. \n" +
                              "иначе мы советуем вам использовать chocolatey для установки приложений.");
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://aka.ms/getwingetpreview",
                UseShellExecute = true
            });
            //Process.Start("explorer", "ms-windows-store://pdp/?productid=9NBLGGH42THS");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при установке winget: {ex.Message}");
        }
    }

    public void InstallChocolatey()
    {
        try
        {
            // deprecated method 
            
            //Console.WriteLine("Для установки Chocolatey откройте Powershell от имени администратора и введите эту команду: \n\nSet-ExecutionPolicy Bypass -Scope Process -Force; [System.Net.ServicePointManager]::SecurityProtocol = [System.Net.ServicePointManager]::SecurityProtocol -bor 3072; iex ((New-Object System.Net.WebClient).DownloadString('https://community.chocolatey.org/install.ps1'))");
            //Console.WriteLine("\nПосле этого, вы сможете продолжить установку.");
            
            
            // Устанавливаем chocolatey с помощью PowerShell
            Console.WriteLine("Устанавливаю Chocolatey...");
            var process = Process.Start(new ProcessStartInfo
            {
                FileName = "powershell",
                Arguments = "-Command \"Set-ExecutionPolicy Bypass -Scope Process -Force; iex ((New-Object System.Net.WebClient).DownloadString('https://community.chocolatey.org/install.ps1'))\"",
                UseShellExecute = true,
                Verb = "runas"
            });
            string commands =
                "powershell -Command \"Set-ExecutionPolicy Bypass -Scope Process -Force; iex ((New-Object System.Net.WebClient).DownloadString('https://community.chocolatey.org/install.ps1'))\"";
            CMDClass.ExecuteCommand(commands, useShellExecute: true);

            process.WaitForExit();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при установке Chocolatey: {ex.Message}");
        }
    }
}