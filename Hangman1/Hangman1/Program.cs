using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

while (true)
{
    GameStatus menuItem = Menu();
    switch(menuItem)
    {
        case GameStatus.Start:
            StartGame();
            break;
        case GameStatus.Exit:
            Console.Clear();Console.WriteLine("\nДо новых встреч!"); 
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
    string fileName = "hangman.txt";
    string[] dictionary=new string[0];
    
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
    
    if (dictionary.Length==0)
    {
        Console.WriteLine("В файле нет слов.Игра невозможна.До новых встреч!");
        return;
    }
    Random randomWord = new Random();
    int wordIndex = randomWord.Next(dictionary.Length);
    string hiddenWord = dictionary[wordIndex];
    char[] userWord = new char[hiddenWord.Length];
    int attempts = 6;
    Console.Clear();
    Console.WriteLine("\nИгра началась");
    ProgressGame(attempts, hiddenWord);

}
    


static char[] GenereticWord(string hiddenWord)
{
    char [] userWord = new char[hiddenWord.Length];
    for (int i = 0; i < userWord.Length; i++)
    {
        userWord[i] = '*';
    }
    Console.WriteLine(new string(userWord) + "\n");
    return userWord;
}


static string ProgressGame(int attempts, string hiddenWord)

    {
        string usedLetters = "";
        var userWord = GenereticWord(hiddenWord);
        while (attempts > 0 && new string(userWord) != hiddenWord)
        {
            bool letterUsedBefore = false;
            Console.WriteLine($"\nУ вас осталось {attempts} попыток");
            Console.WriteLine("\nВведите букву:");
            string guessedWord = "";
            bool isLetterInWord = false;

            char letter = char.ToUpper(Console.ReadKey().KeyChar);


            if (!Alphabet.Symbols.Contains(letter))
            {
                Console.Clear();
                Console.WriteLine("Использование символов запрещено ,используйте только буквы!");
                Console.WriteLine($"\n{new string(userWord)}");
                continue;
            }

            for (int i = 0; i < usedLetters.Length; i++)
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
                Console.WriteLine($"Использованные буквы:\n{usedLetters}");
                continue;
            }


            else
            {
                usedLetters += letter + " ";
                Console.WriteLine($"Использованные буквы:\n{usedLetters}");
            }

            if (isLetterInWord == false)
            {
                attempts--;
                Console.WriteLine("\nВы использовали неверную букву!");
                DrawingHangman(attempts);

            }
        }



        WinOrLoseGame(userWord, hiddenWord);

        return new string(userWord);

    }

    static void WinOrLoseGame(char[] HiddenWord, string hiddenWord)
    {
        if (new string(HiddenWord) == hiddenWord)
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
    string attemptOne = """
         -------|
         |      0
         |       
         |       
         |       
         |_______
         """;
    string attemptTwo = """
         -------|
         |      0
         |      |       
         |       
         |       
         |_______
         """;
    string attemptThree = """
         -------|
         |      0
         |      |\       
         |       
         |       
         |_______
         """;
    string attemptFour = """
         -------|
         |      0
         |     /|\       
         |       
         |       
         |_______
         """;
    string attemptFive = """
         -------|
         |      0
         |     /|\       
         |       \
         |       
         |_______
         """;
    string attemptSix = """
         -------|
         |      0
         |     /|\       
         |     / \
         |       
         |_______
         """;

    switch (attempts)
    {
        case (int)DrawingStatus.Head:
            Console.WriteLine(attemptSix);
            break;
        case (int)DrawingStatus.Body:
            Console.WriteLine(attemptFive);
            break;
        case (int)DrawingStatus.RightHand:
            Console.WriteLine(attemptFour);
            break;
        case (int)DrawingStatus.LeftHand:
            Console.WriteLine(attemptThree);
            break;
        case (int)DrawingStatus.RightLeg:
            Console.WriteLine(attemptTwo);
            break;
        case (int)DrawingStatus.LeftLeg:
            Console.WriteLine(attemptOne);
            break;

    }

}
enum GameStatus
{
    Start = 1,
    Exit
}
enum DrawingStatus
{
    Head,
    Body,
    RightHand,
    LeftHand,
    RightLeg,
    LeftLeg,
}
public static class Alphabet
{
    public readonly static ImmutableHashSet<char> Symbols = ImmutableHashSet.Create( 'А','Б','В','Г','Д','Е','Ё','Ж','З','И','Й','К','Л','М','Н','О','П','Р','С','Т','У','Ф','Х','Ц','Ч','Ш','Щ','Ъ','Ы','Ь','Э','Ю','Я',
            'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я' );
}






