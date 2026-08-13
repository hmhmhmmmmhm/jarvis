using Jarvis.App.Commands;
using Jarvis.App.Services;
using Jarvis.App.Settings;

namespace Jarvis.App.Tests;

public class AppRegistryCommandTests
{
    private string _testFilePath = null!;
    private ApplicationRegistry _registry = null!;

    [SetUp]
    public void Setup()
    {
        _testFilePath = Path.Combine(
            Path.GetTempPath(),
            $"jarvis-app-commands-{Guid.NewGuid()}.json");

        AppSettings settings = new AppSettings();
        SettingsService settingsService =
            new SettingsService(_testFilePath);

        _registry = new ApplicationRegistry(
            settings,
            settingsService);
    }

    [TearDown]
    public void TearDown()
    {
        if (File.Exists(_testFilePath))
        {
            File.Delete(_testFilePath);
        }
    }

    [Test]
    public void AppAddCommand_AddsApplication()
    {
        AppAddCommand command = new AppAddCommand(_registry);

        command.Execute("calc calc");

        Assert.That(
            _registry.FindExecutable("calc"),
            Is.EqualTo("calc"));
    }

    [Test]
    public void AppRemoveCommand_RemovesApplication()
    {
        _registry.Add("calc", "calc");

        AppRemoveCommand command =
            new AppRemoveCommand(_registry);

        command.Execute("calc");

        Assert.That(
            _registry.FindExecutable("calc"),
            Is.Null);
    }

    [Test]
    public void AppListCommand_ReturnsTrue()
    {
        AppListCommand command =
            new AppListCommand(_registry);

        bool result = command.Execute(null);

        Assert.That(result, Is.True);
    }
}