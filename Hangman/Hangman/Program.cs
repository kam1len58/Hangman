using Hangman;

(string, GameStatus)[] menuItems = [
    ("Новая игра", GameStatus.Start),
    ("Игра на двоих", GameStatus.TwoPlayerGame),
    ("Выйти", GameStatus.Exit)];
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
    }
}















