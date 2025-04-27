using Hangman;
using System;
using System.ComponentModel;

SetSettings.SetConsoleSize();
while (true)
{
    GameStatus menuItem = Call();
    switch (menuItem)
    {
        case GameStatus.Start:
            StartGame();
            break;
        case GameStatus.Exit:
            Console.Clear();
            Console.WriteLine("\nДо новых встреч!");
            Console.ReadKey();
            return;
        default:
            Console.WriteLine("\nВведите число 1 или 2");
            break;
    }
}

static GameStatus Call()
{
    Console.Clear();
    Console.WriteLine("\n1(Новая игра)  |  2(Выйти)");
    Console.WriteLine();
    string? input = Console.ReadLine();
    bool isValidChoice = GameStatus.TryParse(input, out GameStatus menuItem);
    return menuItem;
}

static void StartGame()
{

    string[] dictionary = new string[0];
    try
    {
        dictionary = ReadingDictionary();
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

    if (dictionary.Length == 0)
    {
        Console.WriteLine("В файле нет слов. Игра невозможна. До новых встреч!");
        return;
    }
    Random randomWord = new Random();
    int wordIndex = randomWord.Next(dictionary.Length);
    string hiddenWord = dictionary[wordIndex].ToUpper();
    Console.Clear();
    Console.WriteLine("\nИгра началась\n");
    ProgressGame(SetSettings.attempts, hiddenWord);
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
        bool isLetterInWord = false;
        char letter = char.ToUpper(Console.ReadKey().KeyChar);

        if (!Alphabet.allowedSymbols.Contains(letter))
        {
            Console.Clear();
            PrintColoutText("Использование символов запрещено, используйте только буквы кириллицы!", ConsoleColor.Red);
            Console.WriteLine($"\n{new string(userWord)}");
            continue;
        }

        letterUsedBefore = usedLetters.Contains(letter) ? true : false;

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
            PrintLetterStatus(usedLetters);
            continue;
        }
        else
        {
            usedLetters.Add(letter);
            PrintColoutText($"Использованные буквы:\n{string.Join(' ', usedLetters)}", ConsoleColor.Yellow);
        }

        if (!isLetterInWord)
        {
            attempts--;
            PrintColoutText("Вы использовали неверную букву!", ConsoleColor.Red);
        }
        PrintColoutText($"Вы нажали на букву - {letter}", ConsoleColor.Cyan);
    }
    DrawingHangman(attempts);
    PrintGameResult(userWord, hiddenWord);
    return new string(userWord);
}

static void PrintLetterStatus(List<char> usedLetters)
{
    PrintColoutText($"Использованные буквы:\n{string.Join(' ', usedLetters)}", ConsoleColor.Yellow);
    PrintColoutText("Вы уже использовали эту букву!", ConsoleColor.Red);
}

static void PrintGameResult(char[] userWord, string hiddenWord)
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
    string drawing = attempts switch
    {
        (int)DrawingStatus.Head => Drawing.AttemptSix,
        (int)DrawingStatus.Body => Drawing.AttemptFive,
        (int)DrawingStatus.RightHand => Drawing.AttemptFour,
        (int)DrawingStatus.LeftHand => Drawing.AttemptThree,
        (int)DrawingStatus.RightLeg => Drawing.AttemptTwo,
        (int)DrawingStatus.LeftLeg => Drawing.AttemptOne,
        _ => Drawing.Pillar,
    };
    Console.WriteLine(drawing);
}

static string[] ReadingDictionary()
{
    return File.ReadAllLines(SetSettings.fileName);
}

static void PrintColoutText(string text, ConsoleColor consoleColor, ConsoleColor background = ConsoleColor.Black)
{
    Console.ForegroundColor = consoleColor;
    Console.BackgroundColor= background;
    Console.WriteLine(text);
    Console.ResetColor();
}







