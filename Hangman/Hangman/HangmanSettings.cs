namespace Hangman;

public static class HangmanSettings
{
    public const string fileName = "hangman.txt";
    public const int attempts = 6;
    public static void ConsoleHangman()
    {
        Console.SetWindowSize(70,30);
        Console.SetBufferSize(70, 30);
    }
}
