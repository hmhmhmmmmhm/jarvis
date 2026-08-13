using Jarvis.App.Services;
using Jarvis.App.Settings;

namespace Jarvis.App.Tests;

public class ApplicationRegistryTests
{
    private string _testFilePath = null!;
    private AppSettings _settings = null!;
    private SettingsService _settingsService = null!;
    private ApplicationRegistry _registry = null!;

    [SetUp]
    public void Setup()
    {
        _testFilePath = Path.Combine(
            Path.GetTempPath(),
            $"jarvis-app-registry-{Guid.NewGuid()}.json");

        _settings = new AppSettings();
        _settingsService = new SettingsService(_testFilePath);

        _registry = new ApplicationRegistry(
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
    public void EnsureDefaults_AddsDefaultApplications()
    {
        _registry.EnsureDefaults();

        Assert.That(
            _settings.Applications.ContainsKey("vscode"),
            Is.True);

        Assert.That(
            _settings.Applications.ContainsKey("chrome"),
            Is.True);
    }

    [Test]
    public void FindExecutable_KnownApplication_ReturnsExecutable()
    {
        _registry.EnsureDefaults();

        string? result = _registry.FindExecutable("хром");

        Assert.That(result, Is.EqualTo("chrome"));
    }

    [Test]
    public void FindExecutable_UnknownApplication_ReturnsNull()
    {
        _registry.EnsureDefaults();

        string? result = _registry.FindExecutable("unknown-app");

        Assert.That(result, Is.Null);
    }
}