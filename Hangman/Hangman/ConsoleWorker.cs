using System.Collections.Immutable;

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

    public static (bool isValidWord, string inputWord) TryInputWord(ImmutableHashSet<char>? allowedSymbols = null)
    {
        bool isValidWord = true;
        string inputWord = Console.ReadLine()!;
        if (allowedSymbols == null)
            return (isValidWord, inputWord);

        isValidWord = inputWord.All(letter => Alphabet.AllowedSymbols.Contains(letter));

        if (inputWord.Length == 0)
        {
            isValidWord = false;
        }

        return (isValidWord, inputWord);
    }
}
