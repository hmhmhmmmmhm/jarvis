using Jarvis.App;

namespace Jarvis.App.Tests;

public class CommandHandlerTests
{
    [Test]
    public void Handle_HelpCommand_ReturnsTrue()
    {
        bool result = CommandHandler.Handle("help");

        Assert.That(result, Is.True);
    }

    [Test]
    public void Handle_ExitCommand_ReturnsFalse()
    {
        bool result = CommandHandler.Handle("exit");

        Assert.That(result, Is.False);
    }

    [Test]
    public void Handle_EchoCommand_PrintsArgument()
    {
        StringWriter output = new StringWriter();
        Console.SetOut(output);

        CommandHandler.Handle("echo Привет");

        string result = output.ToString().Trim();

        Assert.That(result, Is.EqualTo("Привет"));
    }
}