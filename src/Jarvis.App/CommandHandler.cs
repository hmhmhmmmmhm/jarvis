using Jarvis.App.Commands;

namespace Jarvis.App;

public class CommandHandler
{
    private readonly List<ICommand> _commands;

    public CommandHandler(List<ICommand> commands)
    {
        _commands = commands;
    }

    public bool Handle(string command)
    {
        string[] parts = command.Split(' ', 2);

        string commandName = parts[0].ToLower();
        string? argument = parts.Length > 1 ? parts[1] : null;

        ICommand? foundCommand = _commands
            .FirstOrDefault(command => command.Name == commandName);

        if (foundCommand == null)
        {
            Console.WriteLine($"Неизвестная команда: {commandName}");
            return true;
        }

        return foundCommand.Execute(argument);
    }
}