using Jarvis.App.Models;

namespace Jarvis.App.Parsing;

public class IntentParser
{
    public Intent Parse(string input)
    {
        string normalizedInput = input.Trim();

        // Вопросы о времени преобразуем сразу во внутреннюю команду time.
        if (IsTimeQuestion(normalizedInput))
        {
            return new Intent("time");
        }

        // Вопросы о дате преобразуем во внутреннюю команду date.
        if (IsDateQuestion(normalizedInput))
        {
            return new Intent("date");
        }

        // Фраза "запусти ..." относится к запуску локального приложения.
        if (normalizedInput.StartsWith(
                "запусти ",
                StringComparison.OrdinalIgnoreCase))
        {
            string appName = RemovePrefix(
                normalizedInput,
                "запусти ");

            return new Intent("launch", appName);
        }

        // Фразы "открой ..." пока оставляем для сайтов и веб-ресурсов.
        if (StartsWithAny(
                normalizedInput,
                "открой мне ",
                "открой "))
        {
            string target = RemovePrefix(
                normalizedInput,
                "открой мне ",
                "открой ");

            return new Intent("open", target);
        }

        if (StartsWithAny(
                normalizedInput,
                "найди в интернете ",
                "найди ",
                "поищи "))
        {
            // Получаем поисковый запрос отдельно от имени команды.
            string query = RemovePrefix(
                normalizedInput,
                "найди в интернете ",
                "найди ",
                "поищи ");

            return new Intent("search", query);
        }

        // Если пользователь ввёл старую техническую команду,
        // разбираем её на имя команды и аргумент.
        string[] parts = normalizedInput.Split(' ', 2);

        string commandName = parts[0].ToLower();
        string? argument = parts.Length > 1
            ? parts[1]
            : null;

        return new Intent(commandName, argument);
    }

    private bool IsTimeQuestion(string input)
    {
        string normalized = input.ToLower();

        return normalized is
            "который час" or
            "сколько времени" or
            "сколько сейчас времени" or
            "который сейчас час";
    }

    private bool IsDateQuestion(string input)
    {
        string normalized = input.ToLower();

        return normalized is
            "какая сегодня дата" or
            "какое сегодня число" or
            "что сегодня за день";
    }

    private bool StartsWithAny(
        string input,
        params string[] prefixes)
    {
        foreach (string prefix in prefixes)
        {
            if (input.StartsWith(
                    prefix,
                    StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
        }

        return false;
    }

    private string RemovePrefix(
        string input,
        params string[] prefixes)
    {
        foreach (string prefix in prefixes)
        {
            if (input.StartsWith(
                    prefix,
                    StringComparison.OrdinalIgnoreCase))
            {
                return input[prefix.Length..].Trim();
            }
        }

        return input;
    }
}