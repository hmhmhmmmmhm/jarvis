namespace Jarvis.App.Commands;

public class DateCommand : ICommand
{
    public string Name => "date";

    public string Description => "показать текущую дату";

    public bool Execute(string? argument)
    {
        Console.WriteLine(DateTime.Now.ToString("dd.MM.yyyy"));

        return true;
    }
}