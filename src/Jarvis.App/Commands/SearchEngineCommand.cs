using Jarvis.App.Services;
using Jarvis.App.Settings;

namespace Jarvis.App.Commands;

public class SearchEngineCommand : ICommand
{
    private readonly AppSettings _settings;
    private readonly SettingsService _settingsService;

    public string Name => "search-engine";

    public string Description => "изменить поисковик по умолчанию";

    public SearchEngineCommand(
        AppSettings settings,
        SettingsService settingsService)
    {
        _settings = settings;
        _settingsService = settingsService;
    }

    public bool Execute(string? argument)
    {
        if (string.IsNullOrWhiteSpace(argument))
        {
            Console.WriteLine(
                $"Текущий поисковик: {_settings.DefaultSearchEngine}");

            return true;
        }

        string engine = argument.Trim().ToLower();

        if (engine is not ("google" or "bing" or "duckduckgo"))
        {
            Console.WriteLine(
                "Доступные поисковики: google, bing, duckduckgo");

            return true;
        }

        _settings.DefaultSearchEngine = engine;

        _settingsService.Save(_settings);

        Console.WriteLine(
            $"Поисковик по умолчанию изменён на: {engine}");

        return true;
    }
}