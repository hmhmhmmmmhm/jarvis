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
        // Храним общий объект настроек приложения.
        _settings = settings;

        // Через этот сервис сохраняем изменения в settings.json.
        _settingsService = settingsService;
    }

    public void EnsureDefaults()
    {
        // Если реестр уже заполнен, не перезаписываем пользовательские данные.
        if (_settings.Applications.Count > 0)
        {
            return;
        }

        _settings.Applications["vscode"] = "code";
        _settings.Applications["vs code"] = "code";
        _settings.Applications["код"] = "code";

        _settings.Applications["telegram"] = "telegram";
        _settings.Applications["телеграм"] = "telegram";

        _settings.Applications["chrome"] = "chrome";
        _settings.Applications["хром"] = "chrome";

        _settingsService.Save(_settings);
    }

    public string? FindExecutable(string appName)
    {
        string normalizedName = appName.Trim().ToLower();

        // Возвращаем команду или путь, если алиас найден.
        return _settings.Applications.TryGetValue(
            normalizedName,
            out string? executable)
            ? executable
            : null;
    }

    public IReadOnlyDictionary<string, string> GetAll()
    {
        // Отдаём текущий реестр только для чтения.
        return _settings.Applications;
    }

    public bool Add(string alias, string executable)
    {
        string normalizedAlias = alias.Trim().ToLower();

        // Не разрешаем пустые значения.
        if (string.IsNullOrWhiteSpace(normalizedAlias) ||
            string.IsNullOrWhiteSpace(executable))
        {
            return false;
        }

        // Не перезаписываем существующий алиас молча.
        if (_settings.Applications.ContainsKey(normalizedAlias))
        {
            return false;
        }

        _settings.Applications[normalizedAlias] = executable.Trim();

        // Сразу сохраняем изменение на диск.
        _settingsService.Save(_settings);

        return true;
    }

    public bool Remove(string alias)
    {
        string normalizedAlias = alias.Trim().ToLower();

        // Remove возвращает true, если запись реально существовала.
        bool removed = _settings.Applications.Remove(normalizedAlias);

        if (removed)
        {
            _settingsService.Save(_settings);
        }

        return removed;
    }
}