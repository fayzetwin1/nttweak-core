using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NTTweakCore;

public class CustomTweakClass {
    private static readonly Dictionary<string, Func<string, string, Task>> Commands = new()
    {
        { "print", HandlePrintCommand },
        { "execute", HandleExecuteCommand }
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

        if (Path.GetExtension(fileName) != ".ntt") {
            Console.WriteLine("Неизвестное разрешение файла. NTT поддерживает только .ntt файлы в контексте кастомных твиков.");
            return;
        }

        foreach (var line in File.ReadLines(fileName)) {
            string commandName = line.Split(' ')[0];
            if (Commands.ContainsKey(commandName)) {
                int firstQuoteIndex = line.IndexOf('"');
                int lastQuoteIndex = line.LastIndexOf('"');

                if (firstQuoteIndex != -1 && lastQuoteIndex > firstQuoteIndex) {
                    string context = line.Substring(firstQuoteIndex + 1, lastQuoteIndex - firstQuoteIndex - 1);
                    string parameters = line.Substring(lastQuoteIndex + 1).Trim();
                    await Commands[commandName](context, parameters);
                } else {
                    Console.WriteLine($"Ошибка: некорректный формат команды {commandName}.");
                }
            } else {
                Console.WriteLine($"Неизвестная команда: {commandName}");
            }
        }
    }

    private static Task HandlePrintCommand(string context, string parameters) {
        if (parameters.Contains("--uppercase")) {
            context = context.ToUpper();
        }
        if (parameters.Contains("--reverse")) {
            char[] charArray = context.ToCharArray();
            Array.Reverse(charArray);
            context = new string(charArray);
        }
        Console.WriteLine(context);
        return Task.CompletedTask;
    }

    private static async Task HandleExecuteCommand(string context, string parameters) {
        bool waitForExit = parameters.Contains("--waitForExit");
        await CMDClass.ExecuteCommand(context, waitForExit);
    }
}