namespace Jarvis.App.Text;

public class TextNormalizer
{
    public string Normalize(string input)
    {
        // Убираем пробелы по краям.
        string result = input.Trim();

        // Убираем wake word, если пользователь обратился к Jarvis по имени.
        result = RemoveWakeWord(result);

        // Убираем вежливые хвосты, которые не влияют на смысл команды.
        result = RemovePoliteSuffix(result);

        return result.Trim();
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

    private string RemovePoliteSuffix(string input)
    {
        string[] suffixes =
        {
            ", пожалуйста",
            " пожалуйста",
            " плиз"
        };

        foreach (string suffix in suffixes)
        {
            if (input.EndsWith(
                    suffix,
                    StringComparison.OrdinalIgnoreCase))
            {
                return input[..^suffix.Length].Trim();
            }
        }

        return input;
    }
}