using Jarvis.App.Services;

namespace Jarvis.App.Commands;

public class AppAddCommand : ICommand
{
    private readonly ApplicationRegistry _applicationRegistry;

    public string Name => "app-add";

    public string Description => "добавить приложение в реестр";

    public AppAddCommand(ApplicationRegistry applicationRegistry)
    {
        _applicationRegistry = applicationRegistry;
    }

    public bool Execute(string? argument)
    {
        if (string.IsNullOrWhiteSpace(argument))
        {
            Console.WriteLine(
                "Используйте: app-add <alias> <path-or-command>");

            return true;
        }

        // Делим строку только на две части:
        // первое слово — алиас, остальное — путь или команда.
        string[] parts = argument.Split(' ', 2);

        if (parts.Length < 2)
        {
            Console.WriteLine(
                "Используйте: app-add <alias> <path-or-command>");

            return true;
        }

        string alias = parts[0];
        string executable = parts[1].Trim().Trim('"');

        bool added = _applicationRegistry.Add(alias, executable);

        if (!added)
        {
            Console.WriteLine(
                $"Не удалось добавить приложение '{alias}'. " +
                "Возможно, такой алиас уже существует.");

            return true;
        }

        Console.WriteLine($"Приложение '{alias}' добавлено.");

        return true;
    }
}