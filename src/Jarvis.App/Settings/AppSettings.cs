namespace Jarvis.App.Settings;

public class AppSettings
{
    // Поисковик по умолчанию.
    public string DefaultSearchEngine { get; set; } = "google";

    // Требовать ли обращение "Джарвис".
    public bool RequireWakeWord { get; set; } = false;

    // Список приложений: алиас -> команда или путь к exe.
    public Dictionary<string, string> Applications { get; set; } =
        new Dictionary<string, string>();
}