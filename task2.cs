using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        string inputFilePath = "input_task2.txt";
        string outputFilePath = "output_task2.txt";

        PrepareInputFile(inputFilePath);

        if (!File.Exists(inputFilePath))
        {
            Console.WriteLine($"Помилка: Вхідний файл {inputFilePath} не знайдено.");
            return;
        }

        string text = File.ReadAllText(inputFilePath);
        Console.WriteLine("--- Вміст початкового файлу ---");
        Console.WriteLine(text);
        Console.WriteLine("-------------------------------\n");

        Console.Write("Введіть слово для пошуку в тексті: ");
        string searchWord = Console.ReadLine()?.Trim();

        if (string.IsNullOrEmpty(searchWord))
        {
            Console.WriteLine("Помилка: Слово не може бути порожнім.");
            return;
        }

        // Шукаємо точний збіг слова за допомогою меж \b
        string pattern = $@"\b{Regex.Escape(searchWord)}\b";
        bool isFound = Regex.IsMatch(text, pattern, RegexOptions.IgnoreCase);

        string resultMessage = isFound 
            ? $"Результат: Слово \"{searchWord}\" МІСТИТЬСЯ у тексті." 
            : $"Результат: Слово \"{searchWord}\" НЕ міститься у тексті.";

        Console.WriteLine(resultMessage);
        File.WriteAllText(outputFilePath, resultMessage, Encoding.UTF8);
        Console.WriteLine($"\nРезультат збережено у файл: {outputFilePath}");
    }

    static void PrepareInputFile(string filePath)
    {
        if (!File.Exists(filePath))
        {
            string sampleContent = 
                "Програмування — це чудовий спосіб розвитку логічного мислення.\n" +
                "Рішення лабораторних робіт на мові C# допомагає зрозуміти базові алгоритми.";
            File.WriteAllText(filePath, sampleContent, Encoding.UTF8);
        }
    }
}
