using System.Diagnostics;

namespace Jarvis.App.Commands;

public class LaunchCommand : ICommand
{
    public string Name => "launch";

    public string Description => "запустить приложение";

    public bool Execute(string? argument)
    {
        // Без имени приложения запускать нечего.
        if (string.IsNullOrWhiteSpace(argument))
        {
            Console.WriteLine("Укажите приложение для запуска.");
            return true;
        }

        string appName = argument.Trim().ToLower();

        // Преобразуем человеческое имя приложения
        // в команду или путь, который понимает Windows.
        string? executable = appName switch
        {
            "vscode" or "vs code" or "код" => "code",
            "telegram" or "телеграм" => "telegram",
            "chrome" or "хром" => "chrome",
            _ => null
        };

        if (executable == null)
        {
            Console.WriteLine($"Неизвестное приложение: {argument}");
            return true;
        }

        try
        {
            // Просим Windows запустить зарегистрированное приложение.
            Process.Start(new ProcessStartInfo
            {
                FileName = executable,
                UseShellExecute = true
            });
        }
        catch (Exception)
        {
            Console.WriteLine(
                $"Не удалось запустить приложение: {argument}");
        }

        return true;
    }
}