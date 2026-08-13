using Jarvis.App.Parsing;

namespace Jarvis.App.Tests;

public class IntentParserTests
{
    private IntentParser _parser = null!;

    [SetUp]
    public void Setup()
    {
        _parser = new IntentParser();
    }

    [TestCase("открой ютуб", "open ютуб")]
    [TestCase("запусти ютуб", "open ютуб")]
    [TestCase("открой мне гитхаб", "open гитхаб")]
    public void Parse_OpenPhrases_ReturnsOpenCommand(
        string input,
        string expected)
    {
        string result = _parser.Parse(input);

        Assert.That(result, Is.EqualTo(expected));
    }

    [TestCase("найди погоду в питере", "search погоду в питере")]
    [TestCase("поищи C# interfaces", "search C# interfaces")]
    [TestCase(
        "найди в интернете курс доллара",
        "search курс доллара")]
    public void Parse_SearchPhrases_ReturnsSearchCommand(
        string input,
        string expected)
    {
        string result = _parser.Parse(input);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void Parse_UnknownPhrase_ReturnsOriginalInput()
    {
        string result = _parser.Parse("что-нибудь непонятное");

        Assert.That(result, Is.EqualTo("что-нибудь непонятное"));
    }


    [TestCase("который час", "time")]
    [TestCase("сколько времени", "time")]
    [TestCase("сколько сейчас времени", "time")]
    public void Parse_TimeQuestions_ReturnsTimeCommand(
        string input,
        string expected)
    {
        string result = _parser.Parse(input);

        Assert.That(result, Is.EqualTo(expected));
    }

    [TestCase("какая сегодня дата", "date")]
    [TestCase("какое сегодня число", "date")]
    [TestCase("что сегодня за день", "date")]
    public void Parse_DateQuestions_ReturnsDateCommand(
        string input,
        string expected)
    {
        string result = _parser.Parse(input);

        Assert.That(result, Is.EqualTo(expected));
    }

    [TestCase("открой ютуб пожалуйста", "open ютуб")]
    [TestCase("открой гитхаб, пожалуйста", "open гитхаб")]
    public void Parse_PoliteOpenPhrase_RemovesPoliteSuffix(
        string input,
        string expected)
    {
        string result = _parser.Parse(input);

        Assert.That(result, Is.EqualTo(expected));
    }

    [TestCase(
        "найди погоду в питере пожалуйста",
        "search погоду в питере")]
    [TestCase(
        "поищи C# interfaces, пожалуйста",
        "search C# interfaces")]
    public void Parse_PoliteSearchPhrase_RemovesPoliteSuffix(
        string input,
        string expected)
    {
        string result = _parser.Parse(input);

        Assert.That(result, Is.EqualTo(expected));
    }
}