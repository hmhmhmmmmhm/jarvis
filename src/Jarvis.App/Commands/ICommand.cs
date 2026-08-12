namespace Jarvis.App.Commands;

public interface ICommand
{
    string Name { get; }

    string Description { get; }

    bool Execute(string? argument);
}