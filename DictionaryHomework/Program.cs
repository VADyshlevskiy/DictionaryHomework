using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace DictionaryHomework
{
    class Program
    {
        static void Main(string[] args)
        {
            DictionaryOtus dic = new DictionaryOtus();

            dic.Add(25, ", ");
            dic.Add(15, "!");
            dic.Add(32, "Hello");
            dic.Add(57, "World");
            dic.Add(82, "Привет");
            dic.Add(3, null);
            dic.Add(82, "Мир");
            dic.Add(3, null);
            dic.Add(11, "Hallo");
            dic.Add(87, "Welt");
            dic.Add(17, "Сәлем");
            dic.Add(87, "Әлем");
            dic.Add(89, "Әлем");
            //dic.Add(110, ", ");
            //dic.Add(111, "!");
            //dic.Add(112, "Hello");
            //dic.Add(113, "World");
            //dic.Add(114, "Привет");
            //dic.Add(115, null);
            //dic.Add(116, "Мир");
            //dic.Add(117, null);
            //dic.Add(118, "Hallo");
            //dic.Add(119, "Welt");
            //dic.Add(120, "Сәлем");
            //dic.Add(121, "Әлем");
            //dic.Add(122, "Әлем");

            Console.WriteLine(dic.Get(1) + dic.Get(32) + dic.Get(25) + dic.Get(57) + dic.Get(15));
            Console.WriteLine(dic.Get(3) + dic.Get(82) + dic.Get(25) + dic.Get(82) + dic.Get(15));
            //Console.WriteLine(dic.Get(117) + dic.Get(120) + dic.Get(110) + dic.Get(122) + dic.Get(111));
        }
    }

    public class DictionaryOtus
    {
        static int sizeArray = 7;
        int[] keys = new int[sizeArray];
        string[] words = new string[sizeArray];
        int hash;

        public void Add(int key, string value)
        {

            hash = key % keys.Length;
            try
            {
                //if (!IsFreeSpace()) throw new OverflowException();                                                                // Задание 1
                if (!IsFreeSpace())
                {
                    (keys, words) = ArrayExpansion(keys, words);
                }
                if (value == null) throw new Exception();
               
                while (true)
                {
                    if (hash >= keys.Length) throw new OverflowException();
                    if (keys[hash] == 0)
                    {
                        keys[hash] = key;
                        words[hash] = value;
                        break;
                    }
                    else
                    {
                        if (key == keys[hash]) throw new ExseptionKeyValue("Данный ключ уже добавлен в словарь");
                        hash++;
                        if (hash == keys.Length) hash = 0;
                    }
                }
            }
            catch (OverflowException e)
            {
                Console.WriteLine("В словаре не осталось свободного места:   " + e.Message);
            }
            catch (ExseptionKeyValue e)
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception)
            {
                Console.WriteLine("Задано некорректное значение");
            }

            bool IsFreeSpace()
            {
                int result = 1;
                foreach (var item in keys)
                {
                    result *= item;
                }
                if (result == 0) return true;
                return false;
            }

            (int[], string[]) ArrayExpansion(int[] key, string[] word)
            {
                int[] keysX = new int[key.Length * 2];
                string[] wordsX = new string[word.Length * 2];

                for (int i = 0; i < key.Length; i++)
                {
                    hash = key[i] % keysX.Length;
                    while (true)
                    {
                        if (keysX[hash] == 0)
                        {
                            keysX[hash] = key[i];
                            wordsX[hash] = word[i];
                            break;
                        }
                        else
                        {
                            hash++;
                            if (hash == keysX.Length) hash = 0;
                        }
                    }
                }

                return (keysX, wordsX);
            }

        }


        public string Get(int key)
        {
            string word = "";
            int hash = key % sizeArray;

            try
            {
                while (true)
                {
                    if (key == keys[hash]) return words[hash];
                    else hash++;
                }
            }

            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine($"Элемент с ключем {key} не содержится в словаре");
            }

            catch (Exception e)
            {
                Console.WriteLine("Ошибка!  " + e.Message);
            }

            return word;
        }

        class ExseptionKeyValue : Exception
        {
            public ExseptionKeyValue(string message) : base(message)
            {

            }
        }
    }
}