using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.IO;

namespace NTTweakCore
{
    public class MainClass
    {
        private static readonly Dictionary<string, Func<string[], Task>> Commands = new();
        private static readonly Dictionary<string, string> AppsToInstall = new()
        {
            { "steam", "Valve.Steam" },
            { "spotify", "Spotify.Spotify" },
            { "telegram", "Telegram.TelegramDesktop" },
            { "firefox", "Mozilla.Firefox" },
            { "notepad++", "Notepad++.Notepad++" },
            { "chrome", "Google.Chrome" },
            { "vlc", "VideoLAN.VLC" },
            { "python", "Python.Python.3.11" },
            { "64gram", "64Gram.64Gram" },
            { "obs-studio", "OBSProject.OBSStudio" },
            { "discord", "Discord.Discord" },
            { "vscode", "Microsoft.VisualStudioCode" },
            { "7zip", "7zip.7zip" },
            { "geforce-experience", "Nvidia.GeForceExperience" },
            { "qbittorrent", "qBittorrent.qBittorrent" },
            { "audacity", "Audacity.Audacity" },
            { "everything", "voidtools.Everything" }
        };

        static MainClass()
        {
            // Register commands
            RegisterCommand("--help", args => ShowHelp());
            RegisterCommand("--version", args => ShowVersion());
            RegisterCommand("--info", args => ShowInfo());
            RegisterCommand("--createNewTweak", async args => await CustomTweakClass.CreateNewTweak());
            RegisterCommand("--executeCustomTweak", async args => await CustomTweakClass.ExecuteCustomTweak(args));
            RegisterCommand("--reloadExplorer", args => ExecuteCommand("taskkill /f /im explorer.exe && timeout /t 2 && start explorer.exe", false));
            RegisterCommand("--enableSecondsOnTaskBar", args => ExecuteCommand("reg add \"HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced\" /v ShowSecondsInSystemClock /t REG_DWORD /d 1 /f && taskkill /f /im explorer.exe && timeout /t 2 && start explorer.exe", false));
            RegisterCommand("--disableSecondsOnTaskBar", args => ExecuteCommand("reg add \"HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced\" /v ShowSecondsInSystemClock /t REG_DWORD /d 0 /f && taskkill /f /im explorer.exe && timeout /t 2 && start explorer.exe", false));
            RegisterCommand("--enableShowHiddenFilesAndFolders", args => ExecuteCommand("reg add \"HKCU\\Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced\" /v Hidden /t REG_DWORD /d 1 /f && taskkill /f /im explorer.exe && timeout /t 1 && start explorer.exe", false));
            RegisterCommand("--disableShowHiddenFilesAndFolders", args => ExecuteCommand("reg add \"HKCU\\Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced\" /v Hidden /t REG_DWORD /d 2 /f && taskkill /f /im explorer.exe && timeout /t 1 && start explorer.exe", false));
            RegisterCommand("--enableShowFileExtensions", args => ExecuteCommand("reg add \"HKCU\\Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced\" /v HideFileExt /t REG_DWORD /d 0 /f && taskkill /f /im explorer.exe && timeout /t 1 && start explorer.exe", false));
            RegisterCommand("--disableShowFileExtensions", args => ExecuteCommand("reg add \"HKCU\\Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced\" /v HideFileExt /t REG_DWORD /d 1 /f && taskkill /f /im explorer.exe && timeout /t 1 && start explorer.exe", false));
            RegisterCommand("--enableShowMyPCOnDesktop", args => ExecuteCommand("reg add \"HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\HideDesktopIcons\\NewStartPanel\" /v \"{20D04FE0-3AEA-1069-A2D8-08002B30309D}\" /t REG_DWORD /d 0 /f && taskkill /f /im explorer.exe && timeout /t 1 && start explorer.exe", false));
            RegisterCommand("--disableShowMyPCOnDesktop", args => ExecuteCommand("reg add \"HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\HideDesktopIcons\\NewStartPanel\" /v \"{20D04FE0-3AEA-1069-A2D8-08002B30309D}\" /t REG_DWORD /d 1 /f && taskkill /f /im explorer.exe && timeout /t 1 && start explorer.exe", false));
            RegisterCommand("--enableUpdateCenter", args => ExecuteCommand("chcp 65001 && reg add \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Policies\\Microsoft\\Windows\\WindowsUpdate\\AU\" /v NoAutoUpdate /t REG_DWORD /d 0 /f && sc config wuauserv start= auto && sc start wuauserv", true));
            RegisterCommand("--disableUpdateCenter", args => ExecuteCommand("chcp 65001 && reg add \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Policies\\Microsoft\\Windows\\WindowsUpdate\\AU\" /v NoAutoUpdate /t REG_DWORD /d 1 /f && sc config wuauserv start= disabled && sc stop wuauserv", true));
            RegisterCommand("--enableWindowsDefender", args => ExecuteCommand("chcp 65001 && powershell Set-MpPreference -DisableRealtimeMonitoring $false && reg delete \"HKLM\\SOFTWARE\\Policies\\Microsoft\\Windows Defender\" /v DisableAntiSpyware /f && reg delete \"HKLM\\SOFTWARE\\Policies\\Microsoft\\Windows Defender\" /v DisableAntiVirus /f && reg delete \"HKLM\\SOFTWARE\\Policies\\Microsoft\\Windows Defender\\Real-Time Protection\" /v DisableScanOnRealtimeEnable /f && reg delete \"HKLM\\SOFTWARE\\Policies\\Microsoft\\Windows Defender\\Real-Time Protection\" /v DisableBehaviorMonitoring /f && reg delete \"HKLM\\SOFTWARE\\Policies\\Microsoft\\Windows Defender\" /v DisableCloudProtection /f && reg delete \"HKLM\\SOFTWARE\\Policies\\Microsoft\\Windows Defender\\Signature Updates\" /v DisableUpdateOnStartupWithoutEngine /f", true));
            RegisterCommand("--disableWindowsDefender", args => ExecuteCommand("chcp 65001 && powershell Set-MpPreference -DisableRealtimeMonitoring $true && reg add \"HKLM\\SOFTWARE\\Policies\\Microsoft\\Windows Defender\" /v DisableAntiSpyware /t REG_DWORD /d 1 /f && reg add \"HKLM\\SOFTWARE\\Policies\\Microsoft\\Windows Defender\" /v DisableAntiVirus /t REG_DWORD /d 1 /f && reg add \"HKLM\\SOFTWARE\\Policies\\Microsoft\\Windows Defender\\Real-Time Protection\" /v DisableScanOnRealtimeEnable /t REG_DWORD /d 1 /f && reg add \"HKLM\\SOFTWARE\\Policies\\Microsoft\\Windows Defender\\Real-Time Protection\" /v DisableBehaviorMonitoring /t REG_DWORD /d 1 /f && reg add \"HKLM\\SOFTWARE\\Policies\\Microsoft\\Windows Defender\" /v DisableCloudProtection /t REG_DWORD /d 1 /f && reg add \"HKLM\\SOFTWARE\\Policies\\Microsoft\\Windows Defender\\Signature Updates\" /v DisableUpdateOnStartupWithoutEngine /t REG_DWORD /d 1 /f", true));
            RegisterCommand("--enableUAC", args => ExecuteCommand("chcp 65001 && reg add \"HKLM\\Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System\" /v EnableLUA /t REG_DWORD /d 0 /f && You also have to restart Windows. :3", true));
            RegisterCommand("--disableUAC", args => ExecuteCommand("chcp 65001 && reg add \"HKLM\\Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System\" /v EnableLUA /t REG_DWORD /d 1 /f && You also have to restart Windows. :3", true));

            // Add more commands as needed
        }

        public static async Task Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("   _  ______________  _____            \n  / |/ /_  __/_  __/ / ___/__  _______ \n /    / / /   / /   / /__/ _ \\/ __/ -_)\n/_/|_/ /_/   /_/    \\___/\\___/_/  \\__/ \n                                       ");
            Console.WriteLine("version: v2.25\n");
            Console.ResetColor();
            Console.WriteLine("--------------------------------------------\n");

            if (args.Length > 0 && Commands.ContainsKey(args[0]))
            {
                await Commands[args[0]](args);
            }
            else
            {
                Console.WriteLine("Неизвестная команда. Напишите NTTweakCore.exe --help чтобы узнать список доступных команд.");
            }
        }

        private static void RegisterCommand(string command, Func<string[], Task> action)
        {
            Commands[command] = action;
        }

        private static Task ShowHelp()
        {
            Console.WriteLine("Помощь по командам в NTTweak Core: \n\nОсновные команды: \n----------------------------\n--reloadExplorer - перезапускает проводник (explorer.exe)\n--version - версия твикера" +
                                  "--help - помощь по командам\n--info - информация о твикере\n--installApp - установить приложение\n--createNewTweak - создает файл твика\n--executeCustomTweak - запускает и выполняет кастомный твик. Пример: --executeCustomTweak test.ntt\n----------------------------\nТвики на внешний вид:\n----------------------------\n--enableSecondsOnTaskBar - включает отображение секунд на панели задач\n--disableSecondsOnTaskBar - выключает отображение секунд на панели задач\n\n--enableShowHiddenFilesAndFolders - включает отображение скрытых файлов и папок\n--enableShowHiddenFilesAndFolders - отключает отображение скрытых файлов и папок\n\n" +
                                  "--enableShowFileExtensions - включает отображение расширений файлов\n--disableShowFileExtensions - отключает отображение расширений файлов\n\n--enableShowMyPCOnDesktop - включает отображение ярлыка 'Этот компьютер' на рабочем столе.\n--disableShowMyPCOnDesktop - отключает отображение ярлыка 'Этот компьютер' на рабочем столе.\n" +
                                  "\n----------------------------\nОсновные твики:\n----------------------------\n--enableUpdateCenter - включает центр обновлений\n--disableUpdateCenter - отключает центр обновлений\n\n--enableWindowsDefender - включает Windows Defender\n--disableWindowsDefender - отключает Windows Defender" +
                                  "\n\n--enableUAC - включает UAC в системе\n--disableUAC - отключает UAC в системе\n----------------------------\n\nУстановка приложений\n----------------------------\n--installApp - позволяет установить приложение\n\nwinget (доп. аргумент к --installApp) - пакетный менеджер который будет использоваться для установки приложения" +
                                  "\nchocolatey (доп. аргумент к --installApp) - пакетный менеджер, с которого будет устанавливаться приложение\n\nСписок доступных приложений: steam, spotify, telegram, 64gram, notepad++, discord, firefox, chrome, vlc, python, obs-studio, vscode, 7zip, qbittorrent, geforce-experience, audacity, everything\n----------------------------\n\nУчитывайте также то, что твики в NTTweak применяются подобным образом:\n\nNTTweakCore.exe --(название вашего твика).\nК примеру: \nNTTweakCore.exe --disableUAC .");
            return Task.CompletedTask;
        }

        private static Task ShowVersion()
        {
            Console.WriteLine("Версия NTTweak Core: v2.25");
            return Task.CompletedTask;
        }

        private static Task ShowInfo()
        {
            Console.WriteLine("Разработчик: fayzetwin\nGithub: https://github.com/fayzetwin1");
            return Task.CompletedTask;
        }

        private static async Task ExecuteCommand(string command, bool waitForExit)
        {
            Console.WriteLine("Минутку...");
            await CMDClass.ExecuteCommand(command, waitForExit: waitForExit);
        }



        private static async Task InstallApp(string[] args)
        {
            if (args.Length < 3)
            {
                Console.WriteLine("Не указано приложение для установки. Напишите NTTweakCore.exe --help чтобы узнать список доступных приложений.");
                return;
            }

            string packageManager = args[1].ToLower();
            string appName = args[2].ToLower();

            if (!AppsToInstall.ContainsKey(appName))
            {
                Console.WriteLine("Неизвестное приложение. Напишите NTTweakCore.exe --help чтобы узнать список доступных приложений.");
                return;
            }

            string appId = AppsToInstall[appName];
            string command = packageManager switch
            {
                "winget" => $"winget install {appId} --accept-package-agreements",
                "chocolatey" => $"choco install {appId} -y",
                _ => null
            };

            if (command == null)
            {
                Console.WriteLine("Неизвестный пакетный менеджер. Напишите NTTweakCore.exe --help чтобы узнать список доступных пакетных менеджеров.");
                return;
            }

            Console.WriteLine("Минутку...");
            await CMDClass.ExecuteCommand(command, waitForExit: true);
        }
    }
}
