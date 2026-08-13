namespace Jarvis.App.Settings;

public class AppSettings
{
    // Поисковик, который используется, если пользователь не указал другой.
    public string DefaultSearchEngine { get; set; } = "google";

    // Если true, Jarvis реагирует только на команды с обращением по имени.
    public bool RequireWakeWord { get; set; } = false;
}