namespace Hangman;

public static class GameSettings
{
    public const string FileName = "hangman.txt";
    public const int Attempts = 6;
    public static void SetConsoleSize()
    {
        if (OperatingSystem.IsWindows())
        {
            Console.SetWindowSize(70, 30);
            Console.SetBufferSize(70, 30);
        }
        Console.CursorVisible = false;
        Console.CancelKeyPress += (sender, args) =>
        {
            args.Cancel = true;
        };
    }
}
