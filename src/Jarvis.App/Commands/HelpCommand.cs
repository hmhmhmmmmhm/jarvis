namespace Jarvis.App.Commands;

public class HelpCommand : ICommand
{
    private readonly List<ICommand> _commands;

    public string Name => "help";

    public string Description => "показать список доступных команд";

    public HelpCommand(List<ICommand> commands)
    {
        _commands = commands;
    }

    public bool Execute(string? argument)
    {
        Console.WriteLine("Доступные команды:");

        foreach (ICommand command in _commands)
        {
            Console.WriteLine($"{command.Name} - {command.Description}");
        }

        return true;
    }
}