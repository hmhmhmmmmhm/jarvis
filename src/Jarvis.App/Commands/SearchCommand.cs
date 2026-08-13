using System.Diagnostics;
using Jarvis.App.Settings;

namespace Jarvis.App.Commands;

public class SearchCommand : ICommand
{
    public string Name => "search";

    public string Description => "поиск в интернете";

    private readonly AppSettings _settings;

    public SearchCommand(AppSettings settings)
    {
        _settings = settings;
    }

    public bool Execute(string? argument)
    {
        if (string.IsNullOrWhiteSpace(argument))
        {
            Console.WriteLine("Укажите поисковый запрос.");
            return true;
        }

        string[] parts = argument.Split(' ', 2);

        string searchEngine = _settings.DefaultSearchEngine;
        string query = argument;

        if (parts.Length == 2)
        {
            string possibleEngine = parts[0].ToLower();

            if (possibleEngine is "google" or "bing" or "duckduckgo")
            {
                searchEngine = possibleEngine;
                query = parts[1];
            }
        }

        string encodedQuery = Uri.EscapeDataString(query);

        string url = searchEngine switch
        {
            "bing" => $"https://www.bing.com/search?q={encodedQuery}",
            "duckduckgo" => $"https://duckduckgo.com/?q={encodedQuery}",
            _ => $"https://www.google.com/search?q={encodedQuery}"
        };

        Process.Start(new ProcessStartInfo
        {
            FileName = url,
            UseShellExecute = true
        });

        return true;
    }
}