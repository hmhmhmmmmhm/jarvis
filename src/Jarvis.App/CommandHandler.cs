namespace Jarvis.App;

public static class CommandHandler
{
    public static bool Handle(string command)
    {
        string[] parts = command.Split(' ', 2);

        string commandName = parts[0].ToLower();
        string? argument = parts.Length > 1 ? parts[1] : null;

        return commandName switch
        {
            "help" => HandleHelp(),
            "time" => HandleTime(),
            "echo" => HandleEcho(argument),
            "exit" => HandleExit(),
            _ => HandleUnknownCommand(commandName)
        };
    }

    private static bool HandleHelp()
    {
        Console.WriteLine("Доступные команды:");
        Console.WriteLine("help - список команд");
        Console.WriteLine("time - текущее время");
        Console.WriteLine("echo <текст> - вывести текст");
        Console.WriteLine("exit - выход");

        return true;
    }

    private static bool HandleTime()
    {
        Console.WriteLine(DateTime.Now.ToString("HH:mm:ss"));

        return true;
    }

    private static bool HandleEcho(string? argument)
    {
        if (string.IsNullOrWhiteSpace(argument))
        {
            Console.WriteLine("Укажите текст после команды echo.");
            return true;
        }

        Console.WriteLine(argument);

        return true;
    }

    private static bool HandleExit()
    {
        Console.WriteLine("До встречи.");

        return false;
    }

    private static bool HandleUnknownCommand(string commandName)
    {
        Console.WriteLine($"Неизвестная команда: {commandName}");

        return true;
    }
}