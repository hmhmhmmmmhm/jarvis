using Jarvis.App.Commands;

namespace Jarvis.App.Tests;

public class LaunchCommandTests
{
    [Test]
    public void Execute_WithoutArgument_ReturnsTrue()
    {
        LaunchCommand command = new LaunchCommand();

        bool result = command.Execute(null);

        Assert.That(result, Is.True);
    }

    [Test]
    public void Execute_UnknownApplication_ReturnsTrue()
    {
        LaunchCommand command = new LaunchCommand();

        bool result = command.Execute("definitely-unknown-app");

        Assert.That(result, Is.True);
    }
}