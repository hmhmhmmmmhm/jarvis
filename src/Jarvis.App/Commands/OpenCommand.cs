using System.Diagnostics;

namespace Jarvis.App.Commands;

public class OpenCommand : ICommand
{
    public string Name => "open";

    public string Description => "открыть сайт в браузере";

    public bool Execute(string? argument)
    {
        if (string.IsNullOrWhiteSpace(argument))
        {
            Console.WriteLine("Укажите адрес сайта.");
            return true;
        }

        string url = argument.Trim();

        url = url.ToLower() switch
        {
            "youtube" or "ютуб" => "https://youtube.com",
            "github" or "гитхаб" => "https://github.com",
            "gmail" or "гмейл" => "https://mail.google.com",
            _ => url
        };

        if (!url.StartsWith("http://") && !url.StartsWith("https://"))
        {
            url = "https://" + url;
        }

        Process.Start(new ProcessStartInfo
        {
            FileName = url,
            UseShellExecute = true
        });

        return true;
    }
}