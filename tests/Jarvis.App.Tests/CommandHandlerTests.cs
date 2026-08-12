using Jarvis.App;
using Jarvis.App.Commands;

namespace Jarvis.App.Tests;

public class CommandHandlerTests
{
    private CommandHandler _commandHandler;

    [SetUp]
    public void Setup()
    {
        List<ICommand> commands = new List<ICommand>();

        commands.Add(new TimeCommand());
        commands.Add(new EchoCommand());
        commands.Add(new ExitCommand());
        commands.Add(new HelpCommand(commands));
        commands.Add(new DateCommand());

        _commandHandler = new CommandHandler(commands);
    }

    [Test]
    public void Handle_HelpCommand_ReturnsTrue()
    {
        bool result = _commandHandler.Handle("help");

        Assert.That(result, Is.True);
    }

    [Test]
    public void Handle_ExitCommand_ReturnsFalse()
    {
        bool result = _commandHandler.Handle("exit");

        Assert.That(result, Is.False);
    }

    [Test]
    public void Handle_EchoCommand_PrintsArgument()
    {
        StringWriter output = new StringWriter();
        Console.SetOut(output);

        _commandHandler.Handle("echo Привет");

        string result = output.ToString().Trim();

        Assert.That(result, Is.EqualTo("Привет"));
    }

    [Test]
    public void Handle_DateCommand_ReturnsTrue()
    {
        bool result = _commandHandler.Handle("date");

        Assert.That(result, Is.True);
    }

    [Test]
    public void Handle_HelpCommand_PrintsRegisteredCommands()
    {
        StringWriter output = new StringWriter();
        Console.SetOut(output);

        _commandHandler.Handle("help");

        string result = output.ToString();

        Assert.That(result, Does.Contain("time"));
        Assert.That(result, Does.Contain("echo"));
        Assert.That(result, Does.Contain("exit"));
        Assert.That(result, Does.Contain("date"));
        Assert.That(result, Does.Contain("help"));
    }
}