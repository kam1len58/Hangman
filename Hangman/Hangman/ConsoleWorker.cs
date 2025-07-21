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

    public static void DrawingHangman(int attempts)
    {
        string drawing = attempts switch
        {
            (int)DrawingStatus.Head => Drawing.AttemptSix,
            (int)DrawingStatus.Body => Drawing.AttemptFive,
            (int)DrawingStatus.RightHand => Drawing.AttemptFour,
            (int)DrawingStatus.LeftHand => Drawing.AttemptThree,
            (int)DrawingStatus.RightLeg => Drawing.AttemptTwo,
            (int)DrawingStatus.LeftLeg => Drawing.AttemptOne,
            _ => Drawing.Pillar,
        };
        Console.WriteLine(drawing);
    }
}
