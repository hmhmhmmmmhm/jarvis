using Jarvis.App;
using Jarvis.App.Commands;
using Jarvis.App.Models;

namespace Jarvis.App.Tests;

public class CommandHandlerTests
{
    private CommandHandler _commandHandler = null!;

    [SetUp]
    public void Setup()
    {
        List<ICommand> commands = new List<ICommand>();

        commands.Add(new TimeCommand());
        commands.Add(new EchoCommand());
        commands.Add(new ExitCommand());
        commands.Add(new DateCommand());
        commands.Add(new HelpCommand(commands));

        _commandHandler = new CommandHandler(commands);
    }

    [Test]
    public void Handle_HelpIntent_ReturnsTrue()
    {
        Intent intent = new Intent("help");

        bool result = _commandHandler.Handle(intent);

        Assert.That(result, Is.True);
    }

    [Test]
    public void Handle_ExitIntent_ReturnsFalse()
    {
        Intent intent = new Intent("exit");

        bool result = _commandHandler.Handle(intent);

        Assert.That(result, Is.False);
    }

    [Test]
    public void Handle_EchoIntent_PrintsArgument()
    {
        StringWriter output = new StringWriter();
        Console.SetOut(output);

        Intent intent = new Intent("echo", "Привет");

        _commandHandler.Handle(intent);

        string result = output.ToString().Trim();

        Assert.That(result, Is.EqualTo("Привет"));
    }

    [Test]
    public void Handle_DateIntent_ReturnsTrue()
    {
        Intent intent = new Intent("date");

        bool result = _commandHandler.Handle(intent);

        Assert.That(result, Is.True);
    }

    [Test]
    public void Handle_HelpIntent_PrintsRegisteredCommands()
    {
        StringWriter output = new StringWriter();
        Console.SetOut(output);

        Intent intent = new Intent("help");

        _commandHandler.Handle(intent);

        string result = output.ToString();

        Assert.That(result, Does.Contain("time"));
        Assert.That(result, Does.Contain("echo"));
        Assert.That(result, Does.Contain("exit"));
        Assert.That(result, Does.Contain("date"));
        Assert.That(result, Does.Contain("help"));
    }
}