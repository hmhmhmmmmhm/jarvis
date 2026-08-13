namespace Jarvis.App.Models;

public class Intent
{
    // Имя внутренней команды, например "open", "search" или "time".
    public string CommandName { get; }

    // Дополнительный аргумент команды.
    // Например для "open youtube" здесь будет "youtube".
    public string? Argument { get; }

    public Intent(string commandName, string? argument = null)
    {
        CommandName = commandName;
        Argument = argument;
    }
}