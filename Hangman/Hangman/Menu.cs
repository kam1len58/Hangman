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
    public static TCallMenu SelectFromMenu<TCallMenu>((string MenuOptions, TCallMenu Action)[] menuItems) where TCallMenu : Enum
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
                    ConsoleWorker.PrintColorText(menuItems[i].MenuOptions, ConsoleColor.Black, ConsoleColor.Gray);
                }
                else
                {
                    Console.WriteLine(menuItems[i].MenuOptions);
                }
            }

            ConsoleKey key = Console.ReadKey().Key;
            option = key switch
            {
                ConsoleKey.UpArrow => option == 0 ? menuItems.Length - 1 : option - 1,
                ConsoleKey.DownArrow => option == menuItems.Length - 1 ? 0 : option + 1,
                _ => option
            };
            if (key == ConsoleKey.Enter)
            {
                return menuItems[option].Action;
            }

            Console.Clear();
        }
    }
}






