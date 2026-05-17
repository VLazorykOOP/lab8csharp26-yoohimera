using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        string inputFilePath = "input_task1.txt";
        string outputFilePath = "output_task1.txt";

        PrepareInputFile(inputFilePath);

        if (!File.Exists(inputFilePath))
        {
            Console.WriteLine($"Помилка: Файл {inputFilePath} не знайдено.");
            return;
        }

        string text = File.ReadAllText(inputFilePath);
        Console.WriteLine("--- Вміст початкового файлу ---");
        Console.WriteLine(text);
        Console.WriteLine("-------------------------------\n");

        // Регулярний вираз для формату дд.мм.рррр (1900-2099)
        string pattern = @"\b(0?[1-9]|[12][0-9]|3[01])\.(0?[1-9]|1[012])\.((19|20)\d\d)\b";
        MatchCollection matches = Regex.Matches(text, pattern);

        List<string> validDates = new List<string>();
        foreach (Match match in matches)
        {
            if (IsValidDate(match.Value))
            {
                validDates.Add(match.Value);
            }
        }

        Console.WriteLine($"Знайдено валідних дат: {validDates.Count}");
        foreach (var date in validDates)
        {
            Console.WriteLine($"- {date}");
        }

        File.WriteAllLines(outputFilePath, validDates);
        Console.WriteLine($"\nУсі знайдені дати записано у файл: {outputFilePath}");

        Console.WriteLine("\nОберіть дію:");
        Console.WriteLine("1 - Вилучити конкретну дату");
        Console.WriteLine("2 - Замінити конкретну дату на інший текст");
        Console.WriteLine("Будь-який інший символ - вихід без змін");
        Console.Write("Ваш вибір: ");
        string choice = Console.ReadLine();

        string modifiedText = text;

        if (choice == "1")
        {
            Console.Write("Введіть дату для вилучення: ");
            string targetDate = Console.ReadLine()?.Trim();
            
            if (validDates.Contains(targetDate))
            {
                modifiedText = Regex.Replace(modifiedText, $@"\b{Regex.Escape(targetDate)}\b", "");
                Console.WriteLine($"Дату {targetDate} вилучено.");
            }
            else
            {
                Console.WriteLine("Цієї дати немає серед валідних.");
            }
        }
        else if (choice == "2")
        {
            Console.Write("Введіть дату, яку бажаєте замінити: ");
            string targetDate = Console.ReadLine()?.Trim();

            if (validDates.Contains(targetDate))
            {
                Console.Write("Введіть новий текст/дату для заміни: ");
                string replacement = Console.ReadLine();

                modifiedText = Regex.Replace(modifiedText, $@"\b{Regex.Escape(targetDate)}\b", replacement);
                Console.WriteLine("Заміну виконано.");
            }
            else
            {
                Console.WriteLine("Цієї дати немає серед валідних.");
            }
        }

        if (choice == "1" || choice == "2")
        {
            File.WriteAllText(inputFilePath, modifiedText);
            Console.WriteLine("\n--- Оновлений вміст початкового файлу ---");
            Console.WriteLine(File.ReadAllText(inputFilePath));
        }
    }

    static bool IsValidDate(string dateStr)
    {
        string[] formats = { "d.m.yyyy", "dd.mm.yyyy", "d.mm.yyyy", "dd.m.yyyy" };
        return DateTime.TryParseExact(dateStr, formats, 
                                       System.Globalization.CultureInfo.InvariantCulture, 
                                       System.Globalization.DateTimeStyles.None, 
                                       out _);
    }

    static void PrepareInputFile(string filePath)
    {
        if (!File.Exists(filePath))
        {
            string sampleContent = 
                "Зустріч призначено на 17.05.2026 року, це важлива подія.\n" +
                "Неіснуюча дата 31.02.2025 не повинна пройти перевірку.\n" +
                "Інша правильна дата: 01.09.1900, а також межа 31.12.2099.\n" +
                "Рік поза діапазоном: 15.08.1899 — ігнорується.";
            File.WriteAllText(filePath, sampleContent, Encoding.UTF8);
        }
    }
}
