using Jarvis.App.Services;
using Jarvis.App.Settings;

namespace Jarvis.App.Tests;

public class SettingsServiceTests
{
    private string _testFilePath = null!;

    [SetUp]
    public void Setup()
    {
        _testFilePath = Path.Combine(
            Path.GetTempPath(),
            $"jarvis-settings-{Guid.NewGuid()}.json");
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
    public void Load_WhenFileDoesNotExist_ReturnsDefaultSettings()
    {
        SettingsService service = new SettingsService(_testFilePath);

        AppSettings settings = service.Load();

        Assert.That(settings.DefaultSearchEngine, Is.EqualTo("google"));
    }

    [Test]
    public void Save_CreatesSettingsFile()
    {
        SettingsService service = new SettingsService(_testFilePath);

        AppSettings settings = new AppSettings
        {
            DefaultSearchEngine = "bing"
        };

        service.Save(settings);

        Assert.That(File.Exists(_testFilePath), Is.True);
    }

    [Test]
    public void SaveThenLoad_ReturnsSavedSettings()
    {
        SettingsService service = new SettingsService(_testFilePath);

        AppSettings settings = new AppSettings
        {
            DefaultSearchEngine = "duckduckgo"
        };

        service.Save(settings);

        AppSettings loadedSettings = service.Load();

        Assert.That(
            loadedSettings.DefaultSearchEngine,
            Is.EqualTo("duckduckgo"));
    }
}