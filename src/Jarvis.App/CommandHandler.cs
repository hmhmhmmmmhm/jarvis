using Jarvis.App.Commands;
using Jarvis.App.Models;

namespace Jarvis.App;

public class CommandHandler
{
    private readonly List<ICommand> _commands;

    public CommandHandler(List<ICommand> commands)
    {
        // CommandHandler получает зарегистрированные команды снаружи.
        _commands = commands;
    }

    public bool Handle(Intent intent)
    {
        // Ищем команду по имени, которое уже определил IntentParser.
        ICommand? foundCommand = _commands
            .FirstOrDefault(command =>
                command.Name.Equals(
                    intent.CommandName,
                    StringComparison.OrdinalIgnoreCase));

        if (foundCommand == null)
        {
            Console.WriteLine(
                $"Неизвестная команда: {intent.CommandName}");

            return true;
        }

        // Передаём команде только её аргумент.
        return foundCommand.Execute(intent.Argument);
    }
}