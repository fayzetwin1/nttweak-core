using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace NTTweakCore;

public class CustomTweakClass {
    private static readonly Dictionary<string, Func<string, string, Task>> Commands = new()
    {
        { "print", HandlePrintCommand },
        { "execute", HandleExecuteCommand },
        { "wait", HandleWaitCommand },
        { "colortext", HandleColorTextCommand }
    };

    public static async Task CreateNewTweak() {
        
        
        Console.WriteLine("Для начала, укажи название твика:");
        var tweakName = Console.ReadLine();
        Console.WriteLine("Теперь, укажи автора твика:");
        var tweakAuthor = Console.ReadLine();
        Console.WriteLine("Теперь, укажи версию твика:");
        var tweakVersion = Console.ReadLine();
        File.Create($"{tweakName}.ntt");
    }
    
    
    public static async Task ExecuteCustomTweak(string[] args) {
        if (args.Length < 2) {
            Console.WriteLine("Укажите название файла для его выполнения. К примеру: example.ntt.");
            return;
        }

        var fileName = args[1];
        if (!File.Exists(fileName)) {
            Console.WriteLine("Файл не найден.");
            return;
        }

        foreach (var line in File.ReadLines(fileName)) {
            string[] parts = line.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 0) continue;

            string commandName = parts[0];
            string context = parts.Length > 1 ? parts[1].Trim('"') : string.Empty;

            if (Commands.ContainsKey(commandName)) {
                await Commands[commandName](context, parts.Length > 1 ? parts[1] : string.Empty);
            } else {
                Console.WriteLine($"Неизвестная команда: {commandName}");
            }
        }
    }

    private static Task HandlePrintCommand(string context, string parameters) {
        // Удаляем параметры из контекста перед обработкой
        int paramIndex = context.IndexOf(" --");
        if (paramIndex != -1) {
            context = context.Substring(0, paramIndex);
        }

        string[] paramArray = parameters.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        foreach (var param in paramArray) {
            if (param == "--uppercase") {
                context = context.ToUpper();
            } else if (param == "--reverse") {
                char[] charArray = context.ToCharArray();
                Array.Reverse(charArray);
                context = new string(charArray).Trim('"');
            }
        }
        Console.WriteLine(context);
        return Task.CompletedTask;
    }


    private static async Task HandleExecuteCommand(string context, string parameters) {
        bool waitForExit = parameters.Contains("--waitForExit");
        await CMDClass.ExecuteCommand(context, waitForExit);
    }

    private static Task HandleWaitCommand(string context, string parameters) {
        if (int.TryParse(context, out int seconds)) {
            Thread.Sleep(seconds * 1000);
        } else {
            Console.WriteLine("Ошибка: Некорректное значение для команды wait.");
        }
        return Task.CompletedTask;
    }

    private static Task HandleColorTextCommand(string context, string parameters) {
        ConsoleColor color;
        switch (context.ToLower()) {
            case "black":
                color = ConsoleColor.Black;
                break;
            case "blue":
                color = ConsoleColor.Blue;
                break;
            case "cyan":
                color = ConsoleColor.Cyan;
                break;
            case "darkblue":
                color = ConsoleColor.DarkBlue;
                break;
            case "darkcyan":
                color = ConsoleColor.DarkCyan;
                break;
            case "darkgray":
                color = ConsoleColor.DarkGray;
                break;
            case "darkgreen":
                color = ConsoleColor.DarkGreen;
                break;
            case "darkmagenta":
                color = ConsoleColor.DarkMagenta;
                break;
            case "darkred":
                color = ConsoleColor.DarkRed;
                break;
            case "darkyellow":
                color = ConsoleColor.DarkYellow;
                break;
            case "gray":
                color = ConsoleColor.Gray;
                break;
            case "green":
                color = ConsoleColor.Green;
                break;
            case "magenta":
                color = ConsoleColor.Magenta;
                break;
            case "red":
                color = ConsoleColor.Red;
                break;
            case "white":
                color = ConsoleColor.White;
                break;
            case "yellow":
                color = ConsoleColor.Yellow;
                break;
            default:
                Console.WriteLine($"Ошибка: неизвестный цвет '{context}'.");
                return Task.CompletedTask;
        }

        Console.ForegroundColor = color;
        return Task.CompletedTask;
    }
}