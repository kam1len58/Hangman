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
}
