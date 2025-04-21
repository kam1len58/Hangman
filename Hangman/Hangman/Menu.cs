namespace Hangman;

public static class Menu
{
    /// <summary>
    /// Метод для работы с меню
    /// </summary>
    /// <typeparam name="TCallMenu">Тип перечисления пунктов меню</typeparam>
    /// <param name="menuItems">Массив-кортеж, где первое поле- строка пункта меню, а второе enum-константа для идентификации пункта меню</param>
    /// <returns>Данный метод возвращает enum-константу выбранного пункта меню</returns>
    /// <exception cref="IncorrectMenuInputDataException">Ошибка возникает при недопустимом кол-ве пунктов меню</exception>

    public static TCallMenu MenuItems<TCallMenu>((string, TCallMenu)[] menuItems) where TCallMenu : Enum
    {
        if (menuItems.Length <= 1)
        {
            throw new IncorrectMenuInputDataException("Недопустимое количество пунктов меню");
        }
        Console.Clear();
        int option = 0;
        while (true)
        {
            for (int i = 0; i < menuItems.Length; i++)
            {
                if (i == option)
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine(menuItems[i].Item1);
                }
                else
                {
                    Console.WriteLine(menuItems[i].Item1);
                }
                Console.ResetColor();
            }
            ConsoleKey key = Console.ReadKey().Key;
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    if (option == 0)
                    {
                        option = menuItems.Length - 1;
                    }
                    else
                    {
                        option--;
                    };
                    break;
                case ConsoleKey.DownArrow:
                    if (option == menuItems.Length - 1)
                    {
                        option = 0;
                    }
                    else
                    {
                        option++;
                    };
                    break;
                case ConsoleKey.Enter:
                    return menuItems[option].Item2;
            }
            Console.Clear();
        }
    }
}






