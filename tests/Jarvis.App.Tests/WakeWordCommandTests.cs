using Jarvis.App.Commands;
using Jarvis.App.Services;
using Jarvis.App.Settings;

namespace Jarvis.App.Tests;

public class WakeWordCommandTests
{
    private string _testFilePath = null!;
    private AppSettings _settings = null!;
    private SettingsService _settingsService = null!;
    private WakeWordCommand _command = null!;

    [SetUp]
    public void Setup()
    {
        _testFilePath = Path.Combine(
            Path.GetTempPath(),
            $"jarvis-wake-word-{Guid.NewGuid()}.json");

        _settings = new AppSettings();
        _settingsService = new SettingsService(_testFilePath);

        _command = new WakeWordCommand(
            _settings,
            _settingsService);
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
    public void Execute_On_EnablesWakeWord()
    {
        _command.Execute("on");

        Assert.That(_settings.RequireWakeWord, Is.True);
    }

    [Test]
    public void Execute_Off_DisablesWakeWord()
    {
        _settings.RequireWakeWord = true;

        _command.Execute("off");

        Assert.That(_settings.RequireWakeWord, Is.False);
    }

    [Test]
    public void Execute_On_SavesSetting()
    {
        _command.Execute("on");

        AppSettings loadedSettings = _settingsService.Load();

        Assert.That(loadedSettings.RequireWakeWord, Is.True);
    }
}