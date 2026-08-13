namespace Jarvis.App.Parsing;

public class IntentParser
{
    public string Parse(string input)
    {

        string normalizedInput = input.Trim();
        normalizedInput = RemoveWakeWord(normalizedInput);

        if (IsTimeQuestion(normalizedInput))
        {
            return "time";
        }

        if (IsDateQuestion(normalizedInput))
        {
            return "date";
        }

        if (StartsWithAny(
                normalizedInput,
                "открой мне ",
                "открой ",
                "запусти "))
        {
            string target = RemovePrefix(
                normalizedInput,
                "открой мне ",
                "открой ",
                "запусти ");

            target = CleanArgument(target);

            return $"open {target}";
        }

        if (StartsWithAny(
                normalizedInput,
                "найди в интернете ",
                "найди ",
                "поищи "))
        {
            string query = RemovePrefix(
                normalizedInput,
                "найди в интернете ",
                "найди ",
                "поищи ");

            query = CleanArgument(query);

            return $"search {query}";
        }

        return normalizedInput;
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
    
        private string CleanArgument(string value)
    {
        string result = value.Trim();

        string[] suffixes =
        {
            ", пожалуйста",
            " пожалуйста",
            " плиз"
        };

        foreach (string suffix in suffixes)
        {
            if (result.EndsWith(
                    suffix,
                    StringComparison.OrdinalIgnoreCase))
            {
                result = result[..^suffix.Length].Trim();
                break;
            }
        }

        return result;
    }

        private string RemoveWakeWord(string input)
    {
        string[] wakeWords =
        {
            "джарвис, ",
            "джарвис ",
            "jarvis, ",
            "jarvis "
        };

        foreach (string wakeWord in wakeWords)
        {
            if (input.StartsWith(
                    wakeWord,
                    StringComparison.OrdinalIgnoreCase))
            {
                return input[wakeWord.Length..].Trim();
            }
        }

        return input;
    }


        public bool HasWakeWord(string input)
    {
        string normalizedInput = input.Trim();

        // Проверяем русское и английское написание имени.
        return normalizedInput.StartsWith(
                "джарвис,",
                StringComparison.OrdinalIgnoreCase)
            ||
            normalizedInput.StartsWith(
                "джарвис ",
                StringComparison.OrdinalIgnoreCase)
            ||
            normalizedInput.StartsWith(
                "jarvis,",
                StringComparison.OrdinalIgnoreCase)
            ||
            normalizedInput.StartsWith(
                "jarvis ",
                StringComparison.OrdinalIgnoreCase);
    }
}