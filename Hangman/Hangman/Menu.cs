using System.Collections.Immutable;

namespace Hangman;

public static class Menu
{
    public static GameStatus NewGameOrExit()
    {
        Console.Clear();
        int option = 0;
        ImmutableList<string> menuOptions = ImmutableList.Create
        (
            "Новая игра",
            "Выйти",
            "Игра на двоих"     
        );
        while(true)
        {
            for (int i = 0; i < menuOptions.Count; i++)
            {
                if (i == option)
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine(menuOptions[i]);
                }
                else
                { 
                Console.WriteLine(menuOptions[i]);
                }
                Console.ResetColor();
            }
            ConsoleKey key = Console.ReadKey().Key;
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    if (option == 0)
                    {
                        option = menuOptions.Count - 1;
                    }
                    else
                    {
                        option--;
                    };
                    break;
                case ConsoleKey.DownArrow:
                    if (option == menuOptions.Count - 1)
                    { option = 0;

                    }
                    else
                    {
                        option++;
                    };
                    break;
                case ConsoleKey.Enter:
                    if (option == 0)
                    {
                        return GameStatus.Start;
                    }
                    else if (option == 1)
                    {
                        return GameStatus.Exit;
                    }
                    break;
            }
            Console.Clear(); 
        } 
    }
}


