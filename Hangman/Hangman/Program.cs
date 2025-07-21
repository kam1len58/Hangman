using Hangman;

(string, GameStatus)[] menuItems = [
    ("Новая игра", GameStatus.Start),
    ("Выйти", GameStatus.Exit),
    ("Игра на двоих", GameStatus.TwoPlayerGame)];
GameSettings.SetConsoleSettings();
while (true)
{
    GameStatus menuItem = Menu.SelectFromMenu(menuItems);
    switch (menuItem)
    {
        case GameStatus.Start:
            GameLoop.StartGame();
            break;
        case GameStatus.Exit:
            Console.Clear();
            Console.WriteLine("\nДо новых встреч!");
            Console.ReadKey();
            return;
        case GameStatus.TwoPlayerGame:
            GameLoop.StartTwoPlayerMode();
            break;
        default:
            Console.WriteLine("\nВведите число 1 или 2");
            break;
    }
}















