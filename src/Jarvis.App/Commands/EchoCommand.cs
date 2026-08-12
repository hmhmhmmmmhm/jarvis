namespace Jarvis.App.Commands;

public class EchoCommand : ICommand
{
    public string Name => "echo";

    public string Description => "вывести указанный текст";

    public bool Execute(string? argument)
    {
        if (string.IsNullOrWhiteSpace(argument))
        {
            Console.WriteLine("Укажите текст после команды echo.");
            return true;
        }

        Console.WriteLine(argument);

        return true;
    }
}