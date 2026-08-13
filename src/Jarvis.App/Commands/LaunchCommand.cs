using System.Diagnostics;
using Jarvis.App.Services;

namespace Jarvis.App.Commands;

public class LaunchCommand : ICommand
{
    private readonly ApplicationRegistry _applicationRegistry;

    public string Name => "launch";

    public string Description => "запустить приложение";

    public LaunchCommand(ApplicationRegistry applicationRegistry)
    {
        // Получаем реестр приложений снаружи.
        _applicationRegistry = applicationRegistry;
    }

    public bool Execute(string? argument)
    {
        if (string.IsNullOrWhiteSpace(argument))
        {
            Console.WriteLine("Укажите приложение для запуска.");
            return true;
        }

        // Ищем команду или путь по алиасу приложения.
        string? executable =
            _applicationRegistry.FindExecutable(argument);

        if (executable == null)
        {
            Console.WriteLine($"Неизвестное приложение: {argument}");
            return true;
        }

        try
        {
            // Передаём запуск операционной системе.
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