namespace Hangman;

static class ConsoleWorker
{
    public static void PrintColorText(string text, ConsoleColor consoleColor, ConsoleColor background = ConsoleColor.Black)
    {
        Console.ForegroundColor = consoleColor;
        Console.BackgroundColor = background;
        Console.WriteLine(text);
        Console.ResetColor();
    }

    public static (bool, string?) TryInputWord()
    {
        string? hiddenWord = Console.ReadLine()!;
        bool validLetter = hiddenWord.All(letter => Alphabet.AllowedSymbols.Contains(letter));
        if (!validLetter)
        {
            hiddenWord = null;
        }
        return (validLetter, hiddenWord);
    }
}
