using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Task1
{
    internal class Program
    {

        //делаем новую запись в консоле
        static string Input(int id, DateTime date, string fullName, byte age, byte height, DateTime birthday, string bornPlace)
        {
            Console.WriteLine($"ID: {id}");

            date = DateTime.Now;

            Console.WriteLine($"Дата и время добавления записи: {date}");
            Console.Write("Ваше Ф.И.О.: ");

            fullName = Console.ReadLine();

            Console.Write("Ваш возраст: ");

            age = Convert.ToByte(Console.ReadLine());

            Console.Write("Ваш рост: ");

            height = Convert.ToByte(Console.ReadLine());

            Console.Write("Ваша дата рождения (дд.мм.гггг): ");

            birthday = DateTime.Parse(Console.ReadLine());

            Console.Write("Ваше место рождения: ");

            bornPlace = Console.ReadLine();

            string newRecord = Convert.ToString(id) + "#" + Convert.ToString(date) + "#" +
                fullName + "#" + Convert.ToString(age) + "#" + Convert.ToString(height) + "#" + birthday.ToString("d") + "#" + bornPlace + "#";

            return newRecord;

        }

        //показываем базу данных из файла
        static void Output(string nameFile)
        {
            if (File.Exists(nameFile))
            {
                string records = File.ReadAllText(nameFile);
                string[] split = records.Split('#');

                for (int i = 0; i < split.Length; i++)
                {
                    Console.Write($" {split[i]}");
                }
            }
            else
            {
                Console.WriteLine("Базы данных нету!!!");
            }
        }

        //сохраняем ID, чтобы каждый раз не было заново
        static int LoadID(string nameFile)
        {
            int id = 1;

            if (File.Exists(nameFile))
            {
                string records = File.ReadAllText(nameFile);
                string[] split = records.Split('#');
                id = Convert.ToInt32(split[split.Length - 8]) + 1;
            }

            return id;
        }

        //записываем в файл
        static void Record(string nameFile, string newRecord, int id)
        {
            File.AppendAllText(nameFile, newRecord + "\n");
        }

        //меню и главные значения
        static void Main(string[] args)
        {
            //Переменные
            DateTime date = new DateTime();
            DateTime birthday = new DateTime();

            string fullName = "";
            string nameFile = "DateBase.txt";
            string bornPlace = "";
            string newRecord;
            string menu;

            byte age = 0;
            byte height = 0;

            int id = LoadID(nameFile);


            //Меню
            while (true)
            {

                Console.WriteLine("Главное меню!\n1 - чтение базы данных сотрудников\n2 - ввести новую запись");
                menu = Console.ReadLine();
                Console.Clear();

                if (menu == "1")
                {
                    Console.WriteLine("База данных!");

                    Output(nameFile);

                    Console.WriteLine("Нажмите 1 для выхода в главное меню");

                    menu = Console.ReadLine();

                    while (menu != "1")
                    {
                        menu = Console.ReadLine();
                    }
                    Console.Clear();
                }

                else if (menu == "2")
                {
                    newRecord = Input(id, date, fullName, age, height, birthday, bornPlace);
                    Record(nameFile, newRecord, id);
                    Console.WriteLine("Нажмите 1 для выхода в главное меню, 2 - для того, чтобы ввести ещё одну запись!");

                    while (true)
                    {
                        menu = Console.ReadLine();

                        if (menu == "1") 
                        {
                            Console.Clear();
                            break;
                        } 
                        else if (menu == "2")
                        {
                            Console.Clear();
                            int newID = LoadID(nameFile);
                            newRecord = Input(newID, date, fullName, age, height, birthday, bornPlace);
                            Record(nameFile, newRecord, id);
                            Console.WriteLine("Нажмите 1 для выхода в главное меню, 2 - для того, чтобы ввести ещё одну запись!");
                        }
                    }
                }
            }
        }
    }
}
