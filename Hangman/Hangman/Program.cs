﻿using Hangman;

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
            StartGame();
            break;
        case GameStatus.Exit:
            Console.Clear();
            Console.WriteLine("\nДо новых встреч!");
            Console.ReadKey();
            return;
        case GameStatus.TwoPlayerGame:
            StartTwoPlayerMode();
            break;
        default:
            Console.WriteLine("\nВведите число 1 или 2");
            break;
    }
}

static void StartGame()
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
        bool isLetterInWord = false;

        char letter = char.ToUpper(Console.ReadKey().KeyChar);
        if (!Alphabet.AllowedSymbols.Contains(letter))
        {
            Console.Clear();
            ConsoleWorker.PrintColorText("Использование символов запрещено, используйте только буквы кириллицы!", ConsoleColor.Red);
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
            ConsoleWorker.PrintColorText($"Использованные буквы:\n{string.Join(' ', usedLetters)}", ConsoleColor.Yellow);
            ConsoleWorker.PrintColorText("Вы уже использовали эту букву!", ConsoleColor.Red);
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
            ConsoleWorker.PrintColorText("Вы использовали неверную букву!", ConsoleColor.Red);
        }
        ConsoleWorker.PrintColorText($"Вы нажали на букву - {letter}", ConsoleColor.Cyan);
    }
    DrawingHangman(attempts);
    PrintGameResult(userWord, hiddenWord);
    return new string(userWord);
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

static void StartTwoPlayerMode()
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
    ProgressGame(GameSettings.Attempts, hiddenWord!.ToUpper());
}












