namespace Hangman;

static class GameLoop
{
    public static void StartGame()
    {
        List<string> wordList = [];

        try
        {
            wordList.AddRange(File.ReadAllLines(GameSettings.FileName));
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Добавьте файл со словами для игры!");
        }
        catch (IOException)
        {
            Console.WriteLine("Не удалось получить доступ к файлам игры. Проверьте, не открыты ли они в другой программе, и попробуйте снова.");
        }
        catch (Exception)
        {
            Console.WriteLine("Исправьте ошибку для продолжения игры!");
        }

        if (wordList.Count == 0)
        {
            Console.WriteLine("В файле нет слов. Игра невозможна. До новых встреч!");
            return;
        }
        Random randomWord = new Random();
        int wordIndex = randomWord.Next(wordList.Count);
        string hiddenWord = wordList[wordIndex].ToUpper();
        Console.Clear();
        Console.WriteLine("\nИгра началась\n");
        StartGameplay(GameSettings.Attempts, hiddenWord);
    }

    public static string StartGameplay(int attempts, string hiddenWord)
    {
        List<char> usedLetters = new List<char>();
        Console.WriteLine("Отгадываемое слово:");
        var userWord = new string('*', hiddenWord.Length).ToCharArray();
        Console.WriteLine($"{new string(userWord)}\n");

        while (attempts > 0 && new string(userWord) != hiddenWord)
        {
            bool letterUsedBefore = false;
            ConsoleWorker.DrawingHangman(attempts);
            Console.WriteLine($"\nУ вас осталось {attempts} попыток");
            Console.WriteLine("\nВведите букву:");
            bool isLetterInWord = false;

            char letter = char.ToUpper(Console.ReadKey().KeyChar);
            if (!Alphabet.AllowedSymbols.Contains(letter))
            {
                Console.Clear();
                Console.WriteLine("Отгадываемое слово:");
                Console.WriteLine($"{new string(userWord)}");
                ConsoleWorker.PrintColorText($"\nИспользованные буквы:\n{string.Join(' ', usedLetters)}", ConsoleColor.Yellow);
                ConsoleWorker.PrintColorText("Использование символов запрещено, используйте только буквы кириллицы!\n", ConsoleColor.Red);
                continue;
            }

            letterUsedBefore = usedLetters.Contains(letter);

            for (int i = 0; i < hiddenWord.Length; i++)
            {
                if (hiddenWord[i] == letter)
                {
                    userWord[i] = letter;
                    isLetterInWord = true;
                }
            }

            Console.Clear();
            Console.WriteLine("Отгадываемое слово:");
            Console.WriteLine($"{new string(userWord)}");

            if (letterUsedBefore)
            {
                ConsoleWorker.PrintColorText($"\nИспользованные буквы:\n{string.Join(' ', usedLetters)}", ConsoleColor.Yellow);
                ConsoleWorker.PrintColorText($"Вы нажали на букву - {letter}", ConsoleColor.Cyan);
                ConsoleWorker.PrintColorText("Вы уже использовали эту букву!", ConsoleColor.Red);
                continue;
            }
            else
            {
                usedLetters.Add(letter);
                ConsoleWorker.PrintColorText($"\nИспользованные буквы:\n{string.Join(' ', usedLetters)}", ConsoleColor.Yellow);
            }

            ConsoleWorker.PrintColorText($"Вы нажали на букву - {letter}", ConsoleColor.Cyan);

            if (isLetterInWord)
                Console.WriteLine();
            else
            {
                attempts--;
                ConsoleWorker.PrintColorText("Вы использовали неверную букву!", ConsoleColor.Red);
            }
        }
        PrintGameResult(userWord, hiddenWord);
        return new string(userWord);
    }

    public static void PrintGameResult(char[] userWord, string hiddenWord)
    {
        if (new string(userWord) == hiddenWord)
        {
            Console.Clear();
            ConsoleWorker.PrintColorText($"\nВы выиграли!\nЗагаданное слово - {hiddenWord}", ConsoleColor.Green);
            Console.WriteLine("\nНажмите на любую клавишу для выхода в меню");
            Console.ReadKey();
        }
        else
        {
            Console.Clear();
            ConsoleWorker.PrintColorText($"\nВы проиграли!\nЗагаданное слово - {hiddenWord}", ConsoleColor.Red);
            Console.WriteLine("\nНажмите на любую клавишу для выхода в меню");
            Console.ReadKey();
        }
    }

    public static void StartTwoPlayerMode()
    {
        string hiddenWord;
        bool isValidWord;
        do
        {
            Console.Clear();
            Console.WriteLine("Игрок 1, загадайте слово:\n");
            (isValidWord, hiddenWord) = ConsoleWorker.TryInputWord(Alphabet.AllowedSymbols);
            Console.Clear();
            if (!isValidWord)
            {
                Console.Clear();
                ConsoleWorker.PrintColorText("Используйте только русские буквы!", ConsoleColor.Red);
                Thread.Sleep(GameSettings.ErrorDisplayTime);
            }
        }
        while (!isValidWord);

        Console.Clear();
        Console.WriteLine("Игрок 2, игра началась, отгадайте слово\n");
        StartGameplay(GameSettings.Attempts, hiddenWord!.ToUpper());
    }
}
