using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace NTTweakCore;

public class CMDClass
{
    public static async Task<(string output, string error)> ExecuteCommand(string commands, bool waitForExit = true, bool inputState = false, bool outputState = true, bool errorState = true, bool useShellExecute = false, bool createNoWindow = true)
    {
        string output = string.Empty;
        string error = string.Empty;
        
        if (string.IsNullOrWhiteSpace(commands))
        {
            Console.WriteLine("Нет команд для выполнения.");
            return (output, error);
        }

        try
        {
            var processInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $"/c {commands}",
                RedirectStandardInput = inputState,
                RedirectStandardOutput = outputState,
                RedirectStandardError = errorState,
                StandardOutputEncoding = waitForExit ? Encoding.UTF8 : null, 
                UseShellExecute = useShellExecute, 
                CreateNoWindow = createNoWindow,
                Verb = "runas" 
            };

            using (var process = new Process { StartInfo = processInfo })
            {
                process.Start();
                
                var outputTask = ReadStreamAsync(process.StandardOutput);
                var errorTask = ReadStreamAsync(process.StandardError);

                if (inputState)
                {
                    var inputTask = WriteStreamAsync(process.StandardInput);
                    if (waitForExit)
                    {
                        await Task.WhenAll(outputTask, errorTask, inputTask);
                        process.WaitForExit();
                    }
                    else
                    {
                        Console.WriteLine("Так как команда выполняется со значением waitForExit = false, то завершаю выполнение команды...");
                    }
                }

                if (!inputState || !waitForExit)
                {
                    process.WaitForExit();
                }
                
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            Environment.Exit(0);
        }
        
        return (output, error);
    }
    
    private static async Task ReadStreamAsync(StreamReader stream)
    {
        string line;
        while ((line = await stream.ReadLineAsync()) != null)
        {
            Console.WriteLine(line);
        }
    }
    
    private static async Task WriteStreamAsync(StreamWriter stream)
    {
        string? input;
        while ((input = Console.ReadLine()) != null)
        {
            await stream.WriteLineAsync(input);
            await stream.FlushAsync(); 
        }
    }
}