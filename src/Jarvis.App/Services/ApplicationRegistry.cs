using Jarvis.App.Settings;

namespace Jarvis.App.Services;

public class ApplicationRegistry
{
    private readonly AppSettings _settings;
    private readonly SettingsService _settingsService;

    public ApplicationRegistry(
        AppSettings settings,
        SettingsService settingsService)
    {
        // Используем общий объект настроек приложения.
        _settings = settings;
        _settingsService = settingsService;
    }

    public void EnsureDefaults()
    {
        // Если приложения уже есть в settings.json,
        // ничего не перезаписываем.
        if (_settings.Applications.Count > 0)
        {
            return;
        }

        // Добавляем базовые приложения.
        _settings.Applications["vscode"] = "code";
        _settings.Applications["vs code"] = "code";
        _settings.Applications["код"] = "code";

        _settings.Applications["telegram"] = "telegram";
        _settings.Applications["телеграм"] = "telegram";

        _settings.Applications["chrome"] = "chrome";
        _settings.Applications["хром"] = "chrome";

        // Сохраняем реестр в settings.json.
        _settingsService.Save(_settings);
    }

    public string? FindExecutable(string appName)
    {
        string normalizedName = appName.Trim().ToLower();

        // Ищем приложение по алиасу.
        return _settings.Applications.TryGetValue(
            normalizedName,
            out string? executable)
            ? executable
            : null;
    }
}