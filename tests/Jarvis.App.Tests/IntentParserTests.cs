using Jarvis.App.Models;
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

    [TestCase("открой ютуб", "open", "ютуб")]
    [TestCase("запусти ютуб", "open", "ютуб")]
    [TestCase("открой мне гитхаб", "open", "гитхаб")]
    public void Parse_OpenPhrases_ReturnsOpenIntent(
        string input,
        string expectedCommand,
        string expectedArgument)
    {
        Intent result = _parser.Parse(input);

        Assert.That(result.CommandName, Is.EqualTo(expectedCommand));
        Assert.That(result.Argument, Is.EqualTo(expectedArgument));
    }

    [TestCase("найди погоду в питере", "search", "погоду в питере")]
    [TestCase("поищи C# interfaces", "search", "C# interfaces")]
    [TestCase(
        "найди в интернете курс доллара",
        "search",
        "курс доллара")]
    public void Parse_SearchPhrases_ReturnsSearchIntent(
        string input,
        string expectedCommand,
        string expectedArgument)
    {
        Intent result = _parser.Parse(input);

        Assert.That(result.CommandName, Is.EqualTo(expectedCommand));
        Assert.That(result.Argument, Is.EqualTo(expectedArgument));
    }

    [TestCase("который час")]
    [TestCase("сколько времени")]
    [TestCase("сколько сейчас времени")]
    [TestCase("который сейчас час")]
    public void Parse_TimeQuestions_ReturnsTimeIntent(string input)
    {
        Intent result = _parser.Parse(input);

        Assert.That(result.CommandName, Is.EqualTo("time"));
        Assert.That(result.Argument, Is.Null);
    }

    [TestCase("какая сегодня дата")]
    [TestCase("какое сегодня число")]
    [TestCase("что сегодня за день")]
    public void Parse_DateQuestions_ReturnsDateIntent(string input)
    {
        Intent result = _parser.Parse(input);

        Assert.That(result.CommandName, Is.EqualTo("date"));
        Assert.That(result.Argument, Is.Null);
    }

    [Test]
    public void Parse_TechnicalCommandWithArgument_ReturnsIntent()
    {
        Intent result = _parser.Parse("echo Привет");

        Assert.That(result.CommandName, Is.EqualTo("echo"));
        Assert.That(result.Argument, Is.EqualTo("Привет"));
    }

    [Test]
    public void Parse_TechnicalCommandWithoutArgument_ReturnsIntent()
    {
        Intent result = _parser.Parse("exit");

        Assert.That(result.CommandName, Is.EqualTo("exit"));
        Assert.That(result.Argument, Is.Null);
    }
}