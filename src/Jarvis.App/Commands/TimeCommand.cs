namespace Jarvis.App.Commands;

public class TimeCommand : ICommand
{
    public string Name => "time";

    public string Description => "показать текущее время";

    public bool Execute(string? argument)
    {
        Console.WriteLine(DateTime.Now.ToString("HH:mm:ss"));

        return true;
    }
}