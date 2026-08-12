namespace Jarvis.App.Commands;

public class ExitCommand : ICommand
{
    public string Name => "exit";
    public string Description => "завершить работу Jarvis";
    public bool Execute(string? argument)
    {
        Console.WriteLine("До встречи.");

        return false;
    }
}