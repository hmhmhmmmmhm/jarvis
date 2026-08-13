using Jarvis.App.Services;

namespace Jarvis.App.Commands;

public class AppRemoveCommand : ICommand
{
    private readonly ApplicationRegistry _applicationRegistry;

    public string Name => "app-remove";

    public string Description => "удалить приложение из реестра";

    public AppRemoveCommand(ApplicationRegistry applicationRegistry)
    {
        _applicationRegistry = applicationRegistry;
    }

    public bool Execute(string? argument)
    {
        if (string.IsNullOrWhiteSpace(argument))
        {
            Console.WriteLine("Укажите алиас приложения.");
            return true;
        }

        string alias = argument.Trim();

        bool removed = _applicationRegistry.Remove(alias);

        if (!removed)
        {
            Console.WriteLine($"Приложение '{alias}' не найдено.");
            return true;
        }

        Console.WriteLine($"Приложение '{alias}' удалено.");

        return true;
    }
}