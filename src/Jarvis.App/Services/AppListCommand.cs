using Jarvis.App.Services;

namespace Jarvis.App.Commands;

public class AppListCommand : ICommand
{
    private readonly ApplicationRegistry _applicationRegistry;

    public string Name => "app-list";

    public string Description => "показать зарегистрированные приложения";

    public AppListCommand(ApplicationRegistry applicationRegistry)
    {
        // Получаем реестр приложений извне.
        _applicationRegistry = applicationRegistry;
    }

    public bool Execute(string? argument)
    {
        IReadOnlyDictionary<string, string> applications =
            _applicationRegistry.GetAll();

        if (applications.Count == 0)
        {
            Console.WriteLine("Реестр приложений пуст.");
            return true;
        }

        Console.WriteLine("Зарегистрированные приложения:");

        foreach (KeyValuePair<string, string> app in applications)
        {
            Console.WriteLine($"{app.Key} -> {app.Value}");
        }

        return true;
    }
}