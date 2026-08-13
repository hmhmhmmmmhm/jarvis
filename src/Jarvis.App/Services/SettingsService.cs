using System.Text.Json;
using Jarvis.App.Settings;

namespace Jarvis.App.Services;

public class SettingsService
{
    private readonly string _settingsPath;

    public SettingsService(string? settingsPath = null)
    {
        if (settingsPath != null)
        {
            _settingsPath = settingsPath;
            return;
        }

        string jarvisDirectory = Path.Combine(
            Directory.GetCurrentDirectory(),
            "data");

        Directory.CreateDirectory(jarvisDirectory);

        _settingsPath = Path.Combine(
            jarvisDirectory,
            "settings.json");
    }

    public AppSettings Load()
    {
        if (!File.Exists(_settingsPath))
        {
            return new AppSettings();
        }

        string json = File.ReadAllText(_settingsPath);

        AppSettings? settings =
            JsonSerializer.Deserialize<AppSettings>(json);

        return settings ?? new AppSettings();
    }

    public void Save(AppSettings settings)
    {
        string json = JsonSerializer.Serialize(
            settings,
            new JsonSerializerOptions
            {
                WriteIndented = true
            });

        File.WriteAllText(_settingsPath, json);
    }
}