using System.Diagnostics;

namespace NTTweakCore;

public class MainClass
{
    public static async Task Main(string[] args)
    {
        var packageManagerCheck = new CheckPackageInstalledStateClass();
        var installPackageManager = new InstallPackageManagersClass();
        
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("   _  ______________  _____            \n  / |/ /_  __/_  __/ / ___/__  _______ \n /    / / /   / /   / /__/ _ \\/ __/ -_)\n/_/|_/ /_/   /_/    \\___/\\___/_/  \\__/ \n                                       ");
        Console.WriteLine("version: v1.5\n");
        Console.ResetColor();
        Console.WriteLine("--------------------------------------------\n");
        
        if (args.Length > 0)
        {
            
            if (args[0] == "--help")
            {
                Console.WriteLine("Помощь по командам в NTTweak Core: \n\nОсновные команды: \n----------------------------\n--reloadExplorer - перезапускает проводник (explorer.exe)\n--version - версия твикера" +
                                  "--help - помощь по командам\n--info - информация о твикере\n--installApp - установить приложение\n----------------------------\nТвики на внешний вид:\n----------------------------\n--enableSecondsOnTaskBar - включает отображение секунд на панели задач\n--disableSecondsOnTaskBar - выключает отображение секунд на панели задач\n\n--enableShowHiddenFilesAndFolders - включает отображение скрытых файлов и папок\n--enableShowHiddenFilesAndFolders - отключает отображение скрытых файлов и папок\n\n" +
                                  "--enableShowFileExtensions - включает отображение расширений файлов\n--disableShowFileExtensions - отключает отображение расширений файлов\n\n--enableShowMyPCOnDesktop - включает отображение ярлыка 'Этот компьютер' на рабочем столе.\n--disableShowMyPCOnDesktop - отключает отображение ярлыка 'Этот компьютер' на рабочем столе.\n" +
                                  "\n----------------------------\nОсновные твики:\n----------------------------\n--enableUpdateCenter - включает центр обновлений\n--disableUpdateCenter - отключает центр обновлений\n\n--enableWindowsDefender - включает Windows Defender\n--disableWindowsDefender - отключает Windows Defender" +
                                  "\n\n--enableUAC - включает UAC в системе\n--disableUAC - отключает UAC в системе\n----------------------------\n\nУстановка приложений\n----------------------------\n--installApp - позволяет установить приложение\n\nwinget (доп. аргумент к --installApp) - пакетный менеджер который будет использоваться для установки приложения" +
                                  "\nchocolatey (доп. аргумент к --installApp) - пакетный менеджер, с которого будет устанавливаться приложение\n\nСписок доступных приложений: steam, spotify, telegram, 64gram, notepad++, discord, firefox, chrome, vlc, python, obs-studio, vscode, 7zip, qbittorrent, geforce-experience, audacity, everything\n----------------------------\n\nУчитывайте также то, что твики в NTTweak применяются подобным образом:\n\nNTTweakCore.exe --(название вашего твика).\nК примеру: \nNTTweakCore.exe --disableUAC .");
            }
            
            else if (args[0] == "--version")
            {
                Console.WriteLine("Версия NTTweak Core: v1.5");
            }
            else if (args[0] == "--info")
            {
                Console.WriteLine("Версия твикера: v1.5\nРазработчик твикера: fayzetwin\n\nGithub: https://github.com/fayzetwin1\n\nNTTweak также является форком утилиты MakuTweaker! \n( https://adderly.fun/soft )");
            }
            else if (args[0] == "--reloadExplorer")
            {
                Console.WriteLine("Минутку...");
                Thread.Sleep(1000);
                string commands = "echo Минутку... && taskkill /f /im explorer.exe && timeout /t 2 && start explorer.exe";
                await CMDClass.ExecuteCommand(commands, waitForExit: false);
            }
            else if (args[0] == "--enableSecondsOnTaskBar")
            {
                Console.WriteLine("Минутку...");
                Thread.Sleep(1000);
                string commands =
                    "reg add \"HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced\" /v ShowSecondsInSystemClock /t REG_DWORD /d 1 /f && taskkill /f /im explorer.exe && timeout /t 2 && start explorer.exe";
                await CMDClass.ExecuteCommand(commands, waitForExit: false);
            }
            else if (args[0] == "--disableSecondsOnTaskBar")
            {
                Console.WriteLine("Минутку...");
                Thread.Sleep(1000);
                string commands =
                    "reg add \"HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced\" /v ShowSecondsInSystemClock /t REG_DWORD /d 0 /f && taskkill /f /im explorer.exe && timeout /t 2 && start explorer.exe";
                await CMDClass.ExecuteCommand(commands, waitForExit: false);
            }
            else if (args[0] == "--enableShowHiddenFilesAndFolders")
            {
                Console.WriteLine("Минутку...");
                Thread.Sleep(1000);
                string commands =
                    "reg add \"HKCU\\Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced\" /v Hidden /t REG_DWORD /d 1 /f && taskkill /f /im explorer.exe && timeout /t 1 && start explorer.exe";
                await CMDClass.ExecuteCommand(commands, waitForExit: false);

            }
            else if (args[0] == "--disableShowHiddenFilesAndFolders")
            {
                Console.WriteLine("Минутку...");
                Thread.Sleep(1000);
                string commands =
                    "reg add \"HKCU\\Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced\" /v Hidden /t REG_DWORD /d 2 /f && taskkill /f /im explorer.exe && timeout /t 1 && start explorer.exe";
                await CMDClass.ExecuteCommand(commands, waitForExit: false);
            }
            else if (args[0] == "--enableShowFileExtensions")
            {
                Console.WriteLine("Минутку...");
                Thread.Sleep(1000);
                string commands =
                    "reg add \"HKCU\\Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced\" /v HideFileExt /t REG_DWORD /d 0 /f && taskkill /f /im explorer.exe && timeout /t 1 && start explorer.exe";
                await CMDClass.ExecuteCommand(commands, waitForExit: false);
            }
            else if (args[0] == "--disableShowFileExtensions")
            {
                Console.WriteLine("Минутку...");
                Thread.Sleep(1000);
                string commands =
                    "reg add \"HKCU\\Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced\" /v HideFileExt /t REG_DWORD /d 1 /f && taskkill /f /im explorer.exe && timeout /t 1 && start explorer.exe";
                await CMDClass.ExecuteCommand(commands, waitForExit: false);
            }
            else if (args[0] == "--enableShowMyPCOnDesktop")
            {
                Console.WriteLine("Минутку...");
                Thread.Sleep(1000);
                string commands =
                    "reg add \"HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\HideDesktopIcons\\NewStartPanel\" /v \"{20D04FE0-3AEA-1069-A2D8-08002B30309D}\" /t REG_DWORD /d 0 /f && taskkill /f /im explorer.exe && timeout /t 1 && start explorer.exe";
                await CMDClass.ExecuteCommand(commands, waitForExit: false);
            }
            else if (args[0] == "--disableShowMyPCOnDesktop")
            {
                Console.WriteLine("Минутку...");
                Thread.Sleep(1000);
                string commands =
                    "reg add \"HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\HideDesktopIcons\\NewStartPanel\" /v \"{20D04FE0-3AEA-1069-A2D8-08002B30309D}\" /t REG_DWORD /d 1 /f && taskkill /f /im explorer.exe && timeout /t 1 && start explorer.exe";
                await CMDClass.ExecuteCommand(commands, waitForExit: false);
            }
            else if (args[0] == "--enableUpdateCenter")
            {
                Console.WriteLine("Минутку...");
                Thread.Sleep(1000);
                string commands =
                    "chcp 65001 && reg add \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Policies\\Microsoft\\Windows\\WindowsUpdate\\AU\" /v NoAutoUpdate /t REG_DWORD /d 0 /f && sc config wuauserv start= auto && sc start wuauserv";
                await CMDClass.ExecuteCommand(commands, waitForExit: true);
            }
            else if (args[0] == "--disableUpdateCenter")
            {
                Console.WriteLine("Минутку...");
                Thread.Sleep(1000);
                string commands =
                    "chcp 65001 && reg add \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Policies\\Microsoft\\Windows\\WindowsUpdate\\AU\" /v NoAutoUpdate /t REG_DWORD /d 1 /f && sc config wuauserv start= disabled && sc stop wuauserv";
                await CMDClass.ExecuteCommand(commands, waitForExit: true);
            }
            else if (args[0] == "--enableWindowsDefender")
            {
                Console.WriteLine("Минутку...");
                Thread.Sleep(1000);
                string commands =
                    "chcp 65001 && powershell Set-MpPreference -DisableRealtimeMonitoring $false && reg delete \"HKLM\\SOFTWARE\\Policies\\Microsoft\\Windows Defender\" /v DisableAntiSpyware /f && reg delete \"HKLM\\SOFTWARE\\Policies\\Microsoft\\Windows Defender\" /v DisableAntiVirus /f && reg delete \"HKLM\\SOFTWARE\\Policies\\Microsoft\\Windows Defender\\Real-Time Protection\" /v DisableScanOnRealtimeEnable /f && reg delete \"HKLM\\SOFTWARE\\Policies\\Microsoft\\Windows Defender\\Real-Time Protection\" /v DisableBehaviorMonitoring /f && reg delete \"HKLM\\SOFTWARE\\Policies\\Microsoft\\Windows Defender\" /v DisableCloudProtection /f && reg delete \"HKLM\\SOFTWARE\\Policies\\Microsoft\\Windows Defender\\Signature Updates\" /v DisableUpdateOnStartupWithoutEngine /f";
                await CMDClass.ExecuteCommand(commands, waitForExit: true);
            }
            else if (args[0] == "--disableWindowsDefender")
            {
                Console.WriteLine("Минутку...");
                Thread.Sleep(1000);
                string commands =
                    "chcp 65001 && powershell Set-MpPreference -DisableRealtimeMonitoring $true && reg add \"HKLM\\SOFTWARE\\Policies\\Microsoft\\Windows Defender\" /v DisableAntiSpyware /t REG_DWORD /d 1 /f && reg add \"HKLM\\SOFTWARE\\Policies\\Microsoft\\Windows Defender\" /v DisableAntiVirus /t REG_DWORD /d 1 /f && reg add \"HKLM\\SOFTWARE\\Policies\\Microsoft\\Windows Defender\\Real-Time Protection\" /v DisableScanOnRealtimeEnable /t REG_DWORD /d 1 /f && reg add \"HKLM\\SOFTWARE\\Policies\\Microsoft\\Windows Defender\\Real-Time Protection\" /v DisableBehaviorMonitoring /t REG_DWORD /d 1 /f && reg add \"HKLM\\SOFTWARE\\Policies\\Microsoft\\Windows Defender\" /v DisableCloudProtection /t REG_DWORD /d 1 /f && reg add \"HKLM\\SOFTWARE\\Policies\\Microsoft\\Windows Defender\\Signature Updates\" /v DisableUpdateOnStartupWithoutEngine /t REG_DWORD /d 1 /f";
                await CMDClass.ExecuteCommand(commands, waitForExit: true);
            }
            else if (args[0] == "--enableUAC")
            {
                Console.WriteLine("Минутку...");
                Thread.Sleep(1000);
                string commands =
                    "chcp 65001 && reg add \"HKLM\\Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System\" /v EnableLUA /t REG_DWORD /d 0 /f";
                await CMDClass.ExecuteCommand(commands, waitForExit: true);
                Console.WriteLine("Для применения изменений, советуем перезагрузить ПК. ");
            }
            else if (args[0] == "--disableUAC")
            {
                Console.WriteLine("Минутку...");
                Thread.Sleep(1000);
                string commands =
                    "chcp 65001 && reg add \"HKLM\\Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System\" /v EnableLUA /t REG_DWORD /d 1 /f";
                await CMDClass.ExecuteCommand(commands, waitForExit: true);
                Console.WriteLine("Для применения изменений, советуем перезагрузить ПК. ");
            }
            else if (args[0] == "--installApp")
            {
                Console.WriteLine("Минутку...");
                if (args[1] == "winget")
                {
                    if (!packageManagerCheck.IsWingetInstalled())
                    {
                        installPackageManager.InstallWinget();
                    }
                    else
                    {
                        if (args[2] == "steam")
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("(при установке приложения с WinGet не забудьте в конце установки нажать на enter)");
                            Console.ResetColor();
                            Thread.Sleep(1000);
                            string commands = "winget install Valve.Steam --accept-package-agreements";
                            await CMDClass.ExecuteCommand(commands, waitForExit: true, inputState: true, useShellExecute: false, createNoWindow: true);
                        }
                        else if (args[2] == "spotify")
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("(при установке приложения с WinGet не забудьте в конце установки нажать на enter)");
                            Console.ResetColor();
                            Thread.Sleep(1000);
                            string commands = "winget install Spotify.Spotify --accept-package-agreements";
                            await CMDClass.ExecuteCommand(commands, waitForExit: true, inputState: true, useShellExecute: false, createNoWindow: true);
                        }

                        else if (args[2] == "telegram")
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("(при установке приложения с WinGet не забудьте в конце установки нажать на enter)");
                            Console.ResetColor();
                            Thread.Sleep(1000);
                            string commands = "winget install Telegram.TelegramDesktop --accept-package-agreements";
                            await CMDClass.ExecuteCommand(commands, waitForExit: true, inputState: true, useShellExecute: false, createNoWindow: true);
                        }

                        else if (args[2] == "firefox")
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("(при установке приложения с WinGet не забудьте в конце установки нажать на enter)");
                            Console.ResetColor();
                            Thread.Sleep(1000);
                            string commands = "winget install Mozilla.Firefox --accept-package-agreements";
                            await CMDClass.ExecuteCommand(commands, waitForExit: true, inputState: true, useShellExecute: false, createNoWindow: true);
                        }

                        else if (args[2] == "notepad++")
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("(при установке приложения с WinGet не забудьте в конце установки нажать на enter)");
                            Console.ResetColor();
                            Thread.Sleep(1000);
                            string commands = "winget install Notepad++.Notepad++ --accept-package-agreements";
                            await CMDClass.ExecuteCommand(commands, waitForExit: true, inputState: true, useShellExecute: false, createNoWindow: true);
                        }

                        else if (args[2] == "chrome")
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("(при установке приложения с WinGet не забудьте в конце установки нажать на enter)");
                            Console.ResetColor();
                            Thread.Sleep(1000);
                            string commands = "winget install Google.Chrome --accept-package-agreements";
                            await CMDClass.ExecuteCommand(commands, waitForExit: true, inputState: true, useShellExecute: false, createNoWindow: true);
                        }

                        else if (args[2] == "vlc")
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("(при установке приложения с WinGet не забудьте в конце установки нажать на enter)");
                            Console.ResetColor();
                            Thread.Sleep(1000);
                            string commands = "winget install VideoLAN.VLC --accept-package-agreements";
                            await CMDClass.ExecuteCommand(commands, waitForExit: true, inputState: true, useShellExecute: false, createNoWindow: true);
                        }

                        else if (args[2] == "python")
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("(при установке приложения с WinGet не забудьте в конце установки нажать на enter)");
                            Console.ResetColor();
                            Thread.Sleep(1000);
                            string commands = "winget install Python.Python.3.11 --accept-package-agreements";
                            await CMDClass.ExecuteCommand(commands, waitForExit: true, inputState: true, useShellExecute: false, createNoWindow: true);
                        }
                        
                        else if (args[2] == "64gram")
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("(при установке приложения с WinGet не забудьте в конце установки нажать на enter)");
                            Console.ResetColor();
                            Thread.Sleep(1000);
                            string commands = "winget install 64Gram.64Gram --accept-package-agreements";
                            await CMDClass.ExecuteCommand(commands, waitForExit: true, inputState: true, useShellExecute: false, createNoWindow: true);
                        }
                        
                        else if (args[2] == "obs-studio")
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("(при установке приложения с WinGet не забудьте в конце установки нажать на enter)");
                            Console.ResetColor();
                            Thread.Sleep(1000);
                            string commands = "winget install OBSProject.OBSStudio --accept-package-agreements";
                            await CMDClass.ExecuteCommand(commands, waitForExit: true, inputState: true, useShellExecute: false, createNoWindow: true);
                        }
                        
                        else if (args[2] == "discord")
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("(при установке приложения с WinGet не забудьте в конце установки нажать на enter)");
                            Console.ResetColor();
                            Thread.Sleep(1000);
                            string commands = "winget install Discord.Discord --accept-package-agreements";
                            await CMDClass.ExecuteCommand(commands, waitForExit: true, inputState: true, useShellExecute: false, createNoWindow: true);
                        }
                        
                        else if (args[2] == "vscode")
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("(при установке приложения с WinGet не забудьте в конце установки нажать на enter)");
                            Console.ResetColor();
                            Thread.Sleep(1000);
                            string commands = "winget install Microsoft.VisualStudioCode --accept-package-agreements";
                            await CMDClass.ExecuteCommand(commands, waitForExit: true, inputState: true, useShellExecute: false, createNoWindow: true);
                        }
                        
                        else if (args[2] == "7zip")
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("(при установке приложения с WinGet не забудьте в конце установки нажать на enter)");
                            Console.ResetColor();
                            Thread.Sleep(1000);
                            string commands = "winget install 7zip.7zip --accept-package-agreements";
                            await CMDClass.ExecuteCommand(commands, waitForExit: true, inputState: true, useShellExecute: false, createNoWindow: true);
                        }
                        
                        else if (args[2] == "geforce-experience")
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("(при установке приложения с WinGet не забудьте в конце установки нажать на enter)");
                            Console.ResetColor();
                            Thread.Sleep(1000);
                            string commands = "winget install Nvidia.GeForceExperience --accept-package-agreements";
                            await CMDClass.ExecuteCommand(commands, waitForExit: true, inputState: true, useShellExecute: false, createNoWindow: true);
                        }
                        
                        else if (args[2] == "qbittorrent")
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("(при установке приложения с WinGet не забудьте в конце установки нажать на enter)");
                            Console.ResetColor();
                            Thread.Sleep(1000);
                            string commands = "winget install qBittorrent.qBittorrent --accept-package-agreements";
                            await CMDClass.ExecuteCommand(commands, waitForExit: true, inputState: true, useShellExecute: false, createNoWindow: true);
                        }
                        
                        else if (args[2] == "audacity")
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("(при установке приложения с WinGet не забудьте в конце установки нажать на enter)");
                            Console.ResetColor();
                            Thread.Sleep(1000);
                            string commands = "winget install Audacity.Audacity --accept-package-agreements";
                            await CMDClass.ExecuteCommand(commands, waitForExit: true, inputState: true, useShellExecute: false, createNoWindow: true);
                        }
                        
                        else if (args[2] == "everything")
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("(при установке приложения с WinGet не забудьте в конце установки нажать на enter)");
                            Console.ResetColor();
                            Thread.Sleep(1000);
                            string commands = "winget install voidtools.Everything --accept-package-agreements";
                            await CMDClass.ExecuteCommand(commands, waitForExit: true, inputState: true, useShellExecute: false, createNoWindow: true);
                        }
                        else
                        {
                            Console.WriteLine("Неизвестная команда. Напишите NTTweakCore.exe --help чтобы узнать список доступных приложений.");
                        }
                        
                        
                        
                    }
                }
                else if (args[1] == "chocolatey")
                {
                    if (!packageManagerCheck.IsChocolateyInstalled())
                    {
                        installPackageManager.InstallChocolatey();
                    }
                    else
                    {
                        if (args[2] == "steam")
                        {
                            Thread.Sleep(1000);
                            string commands = "choco install steam -y";
                            await CMDClass.ExecuteCommand(commands, waitForExit: true);
                        }
                        else if (args[2] == "firefox")
                        {
                            Thread.Sleep(1000);
                            string commands = "choco install firefox -y";
                            await CMDClass.ExecuteCommand(commands, waitForExit: true);
                        }
                        else if (args[2] == "spotify")
                        {
                            Thread.Sleep(1000);
                            string commands = "choco install spotify -y";
                            await CMDClass.ExecuteCommand(commands, waitForExit: true);
                        }
                        else if (args[2] == "python")
                        {
                            Thread.Sleep(1000);
                            string commands = "choco install python3 -y";
                            await CMDClass.ExecuteCommand(commands, waitForExit: true);
                        }
                        else if (args[2] == "notepad++")
                        {
                            Thread.Sleep(1000);
                            string commands = "choco install notepadplusplus.install -y";
                            await CMDClass.ExecuteCommand(commands, waitForExit: true);
                        }
                        else if (args[2] == "vlc")
                        {
                            Thread.Sleep(1000);
                            string commands = "choco install vlc -y";
                            await CMDClass.ExecuteCommand(commands, waitForExit: true);
                        }
                        else if (args[2] == "chrome")
                        {
                            Thread.Sleep(1000);
                            string commands = "choco install googlechrome -y";
                            await CMDClass.ExecuteCommand(commands, waitForExit: true);
                        }
                        else if (args[2] == "discord")
                        {
                            Thread.Sleep(1000);
                            string commands = "choco install discord -y";
                            await CMDClass.ExecuteCommand(commands, waitForExit: true);
                        }
                        else if (args[2] == "vscode")
                        {
                            Thread.Sleep(1000);
                            string commands = "choco install vscode -y";
                            await CMDClass.ExecuteCommand(commands, waitForExit: true);
                        }
                        else if (args[2] == "telegram")
                        {
                            Thread.Sleep(1000);
                            string commands = "choco install telegram -y";
                            await CMDClass.ExecuteCommand(commands, waitForExit: true);
                        }
                        else if (args[2] == "64gram")
                        {
                            Thread.Sleep(1000);
                            string commands = "choco install 64gram -y";
                            await CMDClass.ExecuteCommand(commands, waitForExit: true);
                        }
                        else if (args[2] == "obs-studio")
                        {
                            Thread.Sleep(1000);
                            string commands = "choco install obs-studio -y";
                            await CMDClass.ExecuteCommand(commands, waitForExit: true);
                        }
                        else if (args[2] == "geforce-experience")
                        {
                            Thread.Sleep(1000);
                            string commands = "choco install geforce-experience -y";
                            await CMDClass.ExecuteCommand(commands, waitForExit: true);
                        }
                        else if (args[2] == "qbittorrent")
                        {
                            Thread.Sleep(1000);
                            string commands = "choco install qbittorrent -y";
                            await CMDClass.ExecuteCommand(commands, waitForExit: true);
                        }
                        else if (args[2] == "audacity")
                        {
                            Thread.Sleep(1000);
                            string commands = "choco install audacity -y";
                            await CMDClass.ExecuteCommand(commands, waitForExit: true);
                        }
                        else if (args[2] == "7zip")
                        {
                            Thread.Sleep(1000);
                            string commands = "choco install 7zip -y";
                            await CMDClass.ExecuteCommand(commands, waitForExit: true);
                        }
                        else if (args[2] == "everything")
                        {
                            Thread.Sleep(1000);
                            string commands = "choco install everything -y";
                            await CMDClass.ExecuteCommand(commands, waitForExit: true);
                        }
                        else
                        {
                            Console.WriteLine("Неизвестная команда. Напишите NTTweakCore.exe --help чтобы узнать список доступных приложений.");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Неизвестная команда. Напишите NTTweakCore.exe --help чтобы узнать список доступных пакетных менеджеров.");
                }
            }
                
            else
            {
                Console.WriteLine("Неизвестная команда. Напишите NTTweakCore.exe --help чтобы узнать список доступных команд.");
            }
        }
        else
        {
            Console.WriteLine("Неизвестная команда. Напишите NTTweakCore.exe --help чтобы узнать список доступных команд.");
        }
    }
    
}