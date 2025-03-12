using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Hangman
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("1(Новая игра)  |  2(Выйти)");
                Console.WriteLine();
                int game = Convert.ToInt32(Console.ReadLine());
                if (game == 1)
                {
                    string[] words = File.ReadAllLines("hangman.txt");
                    Random s = new Random();
                    int h = s.Next(words.Length);
                    string word = words[h];
                    string word1 = "";
                    int error = 6;


                    Console.WriteLine(word);
                    Console.WriteLine("\nИгра началась");
                    for (int i = 0; i < word.Length; i++)
                    {
                        word1 += "*";
                    }
                    Console.WriteLine(word1 + "\n");



                    string hk = "";
                    
                    do
                    {
                        bool flag1 = false;
                        Console.WriteLine("\nУ вас осталось " + error + " попыток");
                        Console.WriteLine("\nВведите букву:");
                        string word2 = "";
                        bool flag = false;
                        char letter = Convert.ToChar(Console.ReadLine());
                        for (int i =0;i<hk.Length;i++)
                        {
                            if (letter == hk[i])
                            {
                                flag1 = true;
                                break;
                            }
        
                        }
                        if (flag1)
                            Console.WriteLine("\nИспользованные буквы: " + hk);
                        else
                        {
                            hk +=letter+" ";
                            Console.WriteLine("\nИспользованные буквы: " + hk);
                        }



                            for (int i = 0; i < word.Length; i++)
                            {
                                if (word[i] == letter)
                                {
                                    word2 += letter;
                                    flag = true;
                                }
                                else
                                {
                                    word2 += word1[i];


                                }


                            }


                        word1 = word2;


                        Console.WriteLine(word1);

                        if (flag == false)
                        {
                            error--;
                            Console.WriteLine("\nВы использовали неверную букву!");
                        }
                        Console.WriteLine("\nИспользованные буквы: ");
                        switch (error)
                        {
                            case 6:
                                Console.WriteLine("-------|");
                                Console.WriteLine("|       ");
                                Console.WriteLine("|       ");
                                Console.WriteLine("|       ");
                                Console.WriteLine("|       ");
                                Console.WriteLine("|_______");
                                break;
                            case 5:
                                Console.WriteLine("-------|");
                                Console.WriteLine("|      0");
                                Console.WriteLine("|       ");
                                Console.WriteLine("|       ");
                                Console.WriteLine("|       ");
                                Console.WriteLine("|_______");
                                break;
                            case 4:
                                Console.WriteLine("-------|");
                                Console.WriteLine("|      0");
                                Console.WriteLine("|      |");
                                Console.WriteLine("|       ");
                                Console.WriteLine("|       ");
                                Console.WriteLine("|_______");
                                break;
                            case 3:
                                Console.WriteLine("-------|");
                                Console.WriteLine("|      0");
                                Console.WriteLine("|      |\\");
                                Console.WriteLine("|       ");
                                Console.WriteLine("|       ");
                                Console.WriteLine("|_______");
                                break;
                            case 2:
                                Console.WriteLine("-------|");
                                Console.WriteLine("|      0");
                                Console.WriteLine("|     /|\\");
                                Console.WriteLine("|       ");
                                Console.WriteLine("|       ");
                                Console.WriteLine("|_______");
                                break;
                            case 1:
                                Console.WriteLine("-------|");
                                Console.WriteLine("|      0");
                                Console.WriteLine("|     /|\\");
                                Console.WriteLine("|       \\");
                                Console.WriteLine("|       ");
                                Console.WriteLine("|_______");
                                break;
                            case 0:
                                Console.WriteLine("-------|");
                                Console.WriteLine("|      0");
                                Console.WriteLine("|     /|\\");
                                Console.WriteLine("|     / \\");
                                Console.WriteLine("|       ");
                                Console.WriteLine("|_______");
                                break;

                        }

                    }
                    while (error != 0 && word1 != word);
                    {
                        if (word1 == word)
                            Console.WriteLine("Ты выиграл!\nЗагаданное слово - " + word);
                        else
                            Console.WriteLine("Ты Проиграл!\nЗагаданное слово - " + word);

                    }

                }
                else if (game == 2)
                {
                    Console.WriteLine("\nДо новых встреч!");
                    return;
                }
            }
        }
    }
}
