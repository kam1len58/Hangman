using Hangman;
while (true)
{
    GameStatus menuItem = Menu();
    switch (menuItem)
    {
        case GameStatus.Start:
            StartGame();
            break;
        case GameStatus.Exit:
            Console.Clear(); Console.WriteLine("\nДо новых встреч!");
            return;
        default:
            Console.WriteLine("\nВведите число 1 или 2");
            break;
    }
}

static GameStatus Menu()
{
    Console.WriteLine("\n1(Новая игра)  |  2(Выйти)");
    Console.WriteLine();
    string input = Console.ReadLine();
    bool isValidChoice = GameStatus.TryParse(input, out GameStatus menuItem);
    return menuItem;
}

static void StartGame()
{
    const string fileName = "hangman.txt";
    string[] dictionary = new string[0];

    try
    {
        dictionary = File.ReadAllLines(fileName);
    }
    catch (FileNotFoundException)
    {
        Console.WriteLine("Добавьте файл со словами для игры!");
    }
    catch (IOException)
    {
        Console.WriteLine("Не удалось получить доступ к файлам игры.Проверьте, не открыты ли они в другой программе, и попробуйте снова.");
    }
    catch (Exception)
    {
        Console.WriteLine("Исправьте ошибку для продолжения игры!");
    }

    if (dictionary.Length == 0)
    {
        Console.WriteLine("В файле нет слов.Игра невозможна.До новых встреч!");
        return;
    }
    Random randomWord = new Random();
    int wordIndex = randomWord.Next(dictionary.Length);
    string hiddenWord = dictionary[wordIndex];
    int attempts = 6;
    Console.Clear();
    Console.WriteLine("\nИгра началась");
    ProgressGame(attempts, hiddenWord);

}

static char[] GenereticWord(string hiddenWord)
{
    char[] userWord = new char[hiddenWord.Length];
    userWord = new string('*', hiddenWord.Length).ToCharArray();
    Console.WriteLine(new string(userWord) + "\n");
    return userWord;
}

static string ProgressGame(int attempts, string hiddenWord)
{
    List<char> usedLetters = new List<char>();
    var userWord = GenereticWord(hiddenWord);
    while (attempts > 0 && new string(userWord) != hiddenWord)
    {
        bool letterUsedBefore = false;
        Console.WriteLine($"\nУ вас осталось {attempts} попыток");
        Console.WriteLine("\nВведите букву:");

        bool isLetterInWord = false;

        char letter = char.ToUpper(Console.ReadKey().KeyChar);

        if (!Alphabet.allowedSymbols.Contains(letter))
        {
            Console.Clear();
            Console.WriteLine("Использование символов запрещено ,используйте только буквы кириллицы!");
            Console.WriteLine($"\n{new string(userWord)}");
            continue;
        }

        for (int i = 0; i < usedLetters.Count; i++)
        {
            if (letter == usedLetters[i])
            {
                letterUsedBefore = true;
                break;
            }
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
            Console.WriteLine($"Использованные буквы:\n{String.Join(' ', usedLetters)}");
            continue;
        }
        else
        {
            usedLetters.Add(letter);

            Console.WriteLine($"Использованные буквы:\n{String.Join(' ', usedLetters)}");
        }

        if (!isLetterInWord)
        {
            attempts--;
            Console.WriteLine("\nВы использовали неверную букву!");
            DrawingHangman(attempts);

        }
    }

    WinOrLoseGame(userWord, hiddenWord);

    return new string(userWord);

}

static void WinOrLoseGame(char[] userWord, string hiddenWord)
{
    if (new string(userWord) == hiddenWord)
    {
        Console.WriteLine($"\nТы выиграл!\nЗагаданное слово - {hiddenWord}");
    }
    else
    {
        Console.WriteLine($"\nТы Проиграл!\nЗагаданное слово - {hiddenWord}");
    }
}

static void DrawingHangman(int attempts)
{
    switch (attempts)
    {
        case (int)DrawingStatus.Head:
            Console.WriteLine(Drawing.attemptSix);
            break;
        case (int)DrawingStatus.Body:
            Console.WriteLine(Drawing.attemptFive);
            break;
        case (int)DrawingStatus.RightHand:
            Console.WriteLine(Drawing.attemptFour);
            break;
        case (int)DrawingStatus.LeftHand:
            Console.WriteLine(Drawing.attemptThree);
            break;
        case (int)DrawingStatus.RightLeg:
            Console.WriteLine(Drawing.attemptTwo);
            break;
        case (int)DrawingStatus.LeftLeg:
            Console.WriteLine(Drawing.attemptOne);
            break;
    }
}








