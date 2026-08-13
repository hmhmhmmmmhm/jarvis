using Jarvis.App.Services;
using Jarvis.App.Settings;

namespace Jarvis.App.Commands;

public class WakeWordCommand : ICommand
{
    private readonly AppSettings _settings;
    private readonly SettingsService _settingsService;

    public string Name => "wake-word";

    public string Description => "включить или выключить обязательное обращение 'Джарвис'";

    public WakeWordCommand(
        AppSettings settings,
        SettingsService settingsService)
    {
        // Используем общий объект настроек приложения.
        _settings = settings;

        // Через сервис будем сохранять изменение в settings.json.
        _settingsService = settingsService;
    }

    public bool Execute(string? argument)
    {
        // Без аргумента просто показываем текущее состояние.
        if (string.IsNullOrWhiteSpace(argument))
        {
            string state = _settings.RequireWakeWord
                ? "включён"
                : "выключен";

            Console.WriteLine($"Режим wake word: {state}");

            return true;
        }

        string value = argument.Trim().ToLower();

        // Включаем обязательное обращение.
        if (value == "on")
        {
            _settings.RequireWakeWord = true;
            _settingsService.Save(_settings);

            Console.WriteLine("Теперь обращение 'Джарвис' обязательно.");

            return true;
        }

        // Выключаем обязательное обращение.
        if (value == "off")
        {
            _settings.RequireWakeWord = false;
            _settingsService.Save(_settings);

            Console.WriteLine("Обязательное обращение 'Джарвис' выключено.");

            return true;
        }

        Console.WriteLine("Используйте: wake-word on или wake-word off");

        return true;
    }
}