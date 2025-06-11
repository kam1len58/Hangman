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

    public static string InputWord()
    {
        string? hiddenWord = Console.ReadLine()!;
        bool validLetter = hiddenWord.All(letter => Alphabet.AllowedSymbols.Contains(letter));
        if (!validLetter)
        {
            hiddenWord = null;
            ConsoleWorker.PrintColorText("Используйте только русские буквы!", ConsoleColor.Red);
            Thread.Sleep(GameSettings.ErrorDisplayTime);
        }
        return hiddenWord!;
    }
}
