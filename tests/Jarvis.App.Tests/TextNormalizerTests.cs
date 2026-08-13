using Jarvis.App.Text;

namespace Jarvis.App.Tests;

public class TextNormalizerTests
{
    private TextNormalizer _normalizer = null!;

    [SetUp]
    public void Setup()
    {
        _normalizer = new TextNormalizer();
    }

    [TestCase("Джарвис, открой ютуб")]
    [TestCase("Джарвис открой ютуб")]
    [TestCase("jarvis, открой github")]
    [TestCase("jarvis открой github")]
    public void HasWakeWord_WhenWakeWordPresent_ReturnsTrue(string input)
    {
        bool result = _normalizer.HasWakeWord(input);

        Assert.That(result, Is.True);
    }

    [TestCase("открой ютуб")]
    [TestCase("найди погоду")]
    [TestCase("который час")]
    public void HasWakeWord_WhenWakeWordMissing_ReturnsFalse(string input)
    {
        bool result = _normalizer.HasWakeWord(input);

        Assert.That(result, Is.False);
    }

    [TestCase("Джарвис, открой ютуб", "открой ютуб")]
    [TestCase("Джарвис открой ютуб", "открой ютуб")]
    [TestCase("jarvis, открой github", "открой github")]
    public void Normalize_RemovesWakeWord(
        string input,
        string expected)
    {
        string result = _normalizer.Normalize(input);

        Assert.That(result, Is.EqualTo(expected));
    }

    [TestCase(
        "открой ютуб пожалуйста",
        "открой ютуб")]
    [TestCase(
        "открой ютуб, пожалуйста",
        "открой ютуб")]
    [TestCase(
        "открой ютуб плиз",
        "открой ютуб")]
    public void Normalize_RemovesPoliteSuffix(
        string input,
        string expected)
    {
        string result = _normalizer.Normalize(input);

        Assert.That(result, Is.EqualTo(expected));
    }

    [TestCase(
        "Джарвис, открой ютуб, пожалуйста",
        "открой ютуб")]
    [TestCase(
        "Джарвис найди погоду плиз",
        "найди погоду")]
    public void Normalize_RemovesWakeWordAndPoliteSuffix(
        string input,
        string expected)
    {
        string result = _normalizer.Normalize(input);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void Normalize_TrimsWhitespace()
    {
        string result = _normalizer.Normalize("   открой ютуб   ");

        Assert.That(result, Is.EqualTo("открой ютуб"));
    }
}