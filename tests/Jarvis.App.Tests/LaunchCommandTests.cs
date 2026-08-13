using Jarvis.App.Commands;
using Jarvis.App.Services;
using Jarvis.App.Settings;

namespace Jarvis.App.Tests;

public class LaunchCommandTests
{
    private string _testFilePath = null!;
    private LaunchCommand _command = null!;

    [SetUp]
    public void Setup()
    {
        _testFilePath = Path.Combine(
            Path.GetTempPath(),
            $"jarvis-launch-{Guid.NewGuid()}.json");

        AppSettings settings = new AppSettings();
        SettingsService settingsService =
            new SettingsService(_testFilePath);

        ApplicationRegistry registry =
            new ApplicationRegistry(settings, settingsService);

        registry.EnsureDefaults();

        _command = new LaunchCommand(registry);
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
    public void Execute_WithoutArgument_ReturnsTrue()
    {
        bool result = _command.Execute(null);

        Assert.That(result, Is.True);
    }

    [Test]
    public void Execute_UnknownApplication_ReturnsTrue()
    {
        bool result = _command.Execute("definitely-unknown-app");

        Assert.That(result, Is.True);
    }
}
