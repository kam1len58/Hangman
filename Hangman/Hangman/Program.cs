using Hangman;
using System.ComponentModel;

(string, GameStatus)[] menuItems = [
    ("Новая игра", GameStatus.Start),
    ("Выйти", GameStatus.Exit),
    ("Игра на двоих", GameStatus.TwoPlayerGame)];
GameSettings.SetConsoleSettings();
while (true)
{
    switch (Menu.SelectFromMenu(menuItems))
    {
        case GameStatus.Start:
            StartGame();
            break;
        case GameStatus.Exit:
            Console.Clear();
            Console.WriteLine("\nДо новых встреч!");
            Console.ReadKey();
            return;
        case GameStatus.TwoPlayerGame:
            TwoPlayerGame();
            break;
        default:
            Console.WriteLine("\nВведите число 1 или 2");
            break;
    }
}

static void StartGame()
{
    List<string> dictionary = [];

    try
    {
        dictionary.AddRange(File.ReadAllLines(GameSettings.FileName));
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

    if (dictionary.Count == 0)
    {
        Console.WriteLine("В файле нет слов. Игра невозможна. До новых встреч!");
        return;
    }
    Random randomWord = new Random();
    int wordIndex = randomWord.Next(dictionary.Count);
    string hiddenWord = dictionary[wordIndex].ToUpper();

    Console.Clear();
    Console.WriteLine("\nИгра началась");
    ProgressGame(GameSettings.Attempts, hiddenWord);
}

static string ProgressGame(int attempts, string hiddenWord)
{
    List<char> usedLetters = new List<char>();
    var userWord = new string('*', hiddenWord.Length).ToCharArray();
    Console.WriteLine($"{new string(userWord)}\n");
    while (attempts > 0 && new string(userWord) != hiddenWord)
    {
        bool letterUsedBefore = false;
        DrawingHangman(attempts);
        Console.WriteLine($"\nУ вас осталось {attempts} попыток");
        Console.WriteLine("\nВведите букву:");
        Console.CancelKeyPress += (sender, args) =>
        {
            args.Cancel = true;
        };
        bool isLetterInWord = false;
        char letter = char.ToUpper(Console.ReadKey().KeyChar);
        if (!Alphabet.AllowedSymbols.Contains(letter))
        {
            Console.Clear();
            ConsoleWorker.PrintColorText("Использование символов запрещено, используйте только буквы кириллицы!", ConsoleColor.Red);
            Console.WriteLine($"\n{new string(userWord)}");
            continue;
        }
        if (usedLetters.Contains(letter))
        {
            letterUsedBefore = true;
        }
        for (int i = 0; i < hiddenWord.Length; i++)
        {
            if (hiddenWord[i] == letter)
            {
                userWord[i] = letter;
                isLetterInWord = true;
            }
        }
        Console.Clear();
        Console.WriteLine($"\n{new string(userWord)}");

        if (letterUsedBefore)
        {
            ConsoleWorker.PrintColorText($"Использованные буквы:\n{string.Join(' ', usedLetters)}", ConsoleColor.Yellow);
            ConsoleWorker.PrintColorText("Вы уже использовали эту букву!\n", ConsoleColor.Red);
            continue;
        }
        else
        {
            usedLetters.Add(letter);
            ConsoleWorker.PrintColorText($"Использованные буквы:\n{string.Join(' ', usedLetters)}", ConsoleColor.Yellow);
        }
        if (!isLetterInWord)
        {
            attempts--;
            ConsoleWorker.PrintColorText("\nВы использовали неверную букву!", ConsoleColor.Red);
        }
        ConsoleWorker.PrintColorText($"Вы нажали на букву - {letter}\n", ConsoleColor.Cyan);
    }
    DrawingHangman(attempts);
    WinOrLoseGame(userWord, hiddenWord);
    return new string(userWord);
}

static void WinOrLoseGame(char[] userWord, string hiddenWord)
{
    if (new string(userWord) == hiddenWord)
    {
        Console.Clear();
        Console.WriteLine($"\nВы выиграли!\nЗагаданное слово - {hiddenWord}");
        Console.WriteLine("\nНажмите на любую клавишу для выхода в меню");
        Console.ReadKey();
    }
    else
    {
        Console.Clear();
        Console.WriteLine($"\nВы проиграли!\nЗагаданное слово - {hiddenWord}");
        Console.WriteLine("\nНажмите на любую клавишу для выхода в меню");
        Console.ReadKey();
    }
}

static void DrawingHangman(int attempts)
{
    switch (attempts)
    {
        case (int)DrawingStatus.Head:
            Console.WriteLine(Drawing.AttemptSix);
            break;
        case (int)DrawingStatus.Body:
            Console.WriteLine(Drawing.AttemptFive);
            break;
        case (int)DrawingStatus.RightHand:
            Console.WriteLine(Drawing.AttemptFour);
            break;
        case (int)DrawingStatus.LeftHand:
            Console.WriteLine(Drawing.AttemptThree);
            break;
        case (int)DrawingStatus.RightLeg:
            Console.WriteLine(Drawing.AttemptTwo);
            break;
        case (int)DrawingStatus.LeftLeg:
            Console.WriteLine(Drawing.AttemptOne);
            break;
        default:
            Console.WriteLine(Drawing.Pillar);
            break;
    }
}

static void TwoPlayerGame()
{
    string? hiddenWord;
    do
    {
        Console.Clear();
        Console.WriteLine("Игрок 1, загадайте слово:\n", ConsoleColor.Gray);
        hiddenWord = ConsoleWorker.InputWord();
    }
    while (hiddenWord == null || hiddenWord.Length == 0);

    Console.Clear();
    Console.WriteLine("Игрок 2, игра началась, отгадайте слово\n");
    ProgressGame(GameSettings.Attempts, hiddenWord.ToUpper());
}












