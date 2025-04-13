using System.Collections.Immutable;

namespace Hangman;

public static class Menu
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="menuOptions"></param>
    /// <param name="menuItems"></param>
    /// <returns>Ошибка количества пунктов меню</returns>
    /// <exception cref="System.UnacceptableNumberOfMenuItemsIsAnException">Недопустимое количество пунктов меню</exception>

    public static T MenuItems<T>(ImmutableList<string> menuOptions, T[] menuItems) where T : Enum
    {
        if(menuOptions.Count<=1)
        {
            throw new IncorrectMenuInputDataException("Недопустимое количество пунктов меню");
        }
        Console.Clear();
        int option = 0;
        while (true)
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
                    return menuItems[option];
            }
            Console.Clear();
        }
    }

}






