using System;
using System.IO;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        // Змінні конфігурації
        string lastName = "Petrenko"; 
        string baseDir = @"D:\temp"; // Змініть на "C:\temp", якщо у вас немає диска D

        string dir1 = Path.Combine(baseDir, $"{lastName}1");
        string dir2 = Path.Combine(baseDir, $"{lastName}2");
        string dirAll = Path.Combine(baseDir, "ALL");

        try
        {
            if (!Directory.Exists(baseDir))
            {
                Directory.CreateDirectory(baseDir);
            }

            // 1. Створення папок
            Console.WriteLine("Крок 1. Створення папок...");
            Directory.CreateDirectory(dir1);
            Directory.CreateDirectory(dir2);

            // 2. Створення файлів t1.txt та t2.txt
            Console.WriteLine("Крок 2. Запис текстів у файли...");
            string t1Path = Path.Combine(dir1, "t1.txt");
            string t2Path = Path.Combine(dir1, "t2.txt");

            string t1Content = "Лабораторні роботи. Мова програмування C#. Коваленко О.П.\n" +
                               "Шевченко Степан Іванович, 2001 року народження, місце проживання м. Суми";
            string t2Content = "Комар Сергій Федорович, 2000 року народження, місце проживання м. Київ";

            File.WriteAllText(t1Path, t1Content, Encoding.UTF8);
            File.WriteAllText(t2Path, t2Content, Encoding.UTF8);

            // 3. Створення t3.txt в папці 2 (t1 + t2)
            Console.WriteLine("Крок 3. Створення файлу t3.txt (об'єднання t1 та t2)...");
            string t3Path = Path.Combine(dir2, "t3.txt");
            string t3Content = File.ReadAllText(t1Path) + Environment.NewLine + File.ReadAllText(t2Path);
            File.WriteAllText(t3Path, t3Content, Encoding.UTF8);

            // 4. Виведення розгорнутої інформації про створені файли
            Console.WriteLine("\nКрок 4. Інформація про створені файли:");
            PrintFileInfo(t1Path);
            PrintFileInfo(t2Path);
            PrintFileInfo(t3Path);
            Console.WriteLine();

            // 5. Перенесення t2.txt у папку 2
            Console.WriteLine("Крок 5. Перенесення t2.txt...");
            string t2NewPath = Path.Combine(dir2, "t2.txt");
            if (File.Exists(t2NewPath)) File.Delete(t2NewPath); 
            File.Move(t2Path, t2NewPath);

            // 6. Копіювання t1.txt у папку 2
            Console.WriteLine("Крок 6. Копіювання t1.txt...");
            string t1NewPath = Path.Combine(dir2, "t1.txt");
            File.Copy(t1Path, t1NewPath, true); 

            // 7. Перейменування папки K2 в ALL та вилучення папки 1
            Console.WriteLine("Крок 7. Перейменування папки K2 в ALL та видалення папки 1...");
            if (Directory.Exists(dirAll)) 
            {
                Directory.Delete(dirAll, true); 
            }
            Directory.Move(dir2, dirAll); 
            Directory.Delete(dir1, true); 

            // 8. Вивести повну інформацію про файли папки All
            Console.WriteLine("\n--- Крок 8. Повна інформація про файли в папці ALL ---");
            if (Directory.Exists(dirAll))
            {
                string[] filesInAll = Directory.GetFiles(dirAll);
                foreach (string file in filesInAll)
                {
                    PrintFileInfo(file);
                    Console.WriteLine("Вміст файлу:");
                    Console.WriteLine(File.ReadAllText(file));
                    Console.WriteLine(new string('-', 40));
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Сталася помилка: {ex.Message}");
        }
    }

    static void PrintFileInfo(string filePath)
    {
        if (File.Exists(filePath))
        {
            FileInfo info = new FileInfo(filePath);
            Console.WriteLine($"Файл: {info.Name}");
            Console.WriteLine($"  Повний шлях: {info.FullName}");
            Console.WriteLine($"  Розмір: {info.Length} байт");
            Console.WriteLine($"  Час створення: {info.CreationTime}");
        }
        else
        {
            Console.WriteLine($"Файл за шляхом {filePath} не знайдено.");
        }
    }
}
