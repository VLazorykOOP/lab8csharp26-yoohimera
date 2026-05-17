using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        string binaryFilePath = "words.dat";
        CreateBinaryFile(binaryFilePath);

        if (!File.Exists(binaryFilePath)) return;

        Console.Write("Введіть ОДНУ літеру, на яку мають починатися слова: ");
        string input = Console.ReadLine()?.Trim();

        if (string.IsNullOrEmpty(input))
        {
            Console.WriteLine("Помилка: Ви нічого не ввели.");
            return;
        }

        char searchChar = char.ToLower(input[0]);
        Console.WriteLine($"\n--- Шукаємо слова на літеру '{searchChar}' ---");

        int foundCount = 0;

        using (FileStream fs = new FileStream(binaryFilePath, FileMode.Open, FileAccess.Read))
        using (BinaryReader reader = new BinaryReader(fs, Encoding.UTF8))
        {
            while (fs.Position < fs.Length)
            {
                string word = reader.ReadString();

                if (word.Length > 0 && char.ToLower(word[0]) == searchChar)
                {
                    Console.WriteLine($"- {word}");
                    foundCount++;
                }
            }
        }

        if (foundCount == 0)
            Console.WriteLine($"Слів на літеру '{searchChar}' не знайдено.");
        else
            Console.WriteLine($"\nВсього знайдено слів: {foundCount}");
    }

    static void CreateBinaryFile(string filePath)
    {
        string[] sampleWords = {
            "Сонце", "Україна", "Студент", "Алгоритм", "Слово", 
            "Програмування", "Код", "Край", "Університет", "Дім", "Світ"
        };

        try
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            using (BinaryWriter writer = new BinaryWriter(fs, Encoding.UTF8))
            {
                foreach (string word in sampleWords)
                {
                    writer.Write(word);
                }
            }
            Console.WriteLine($"Двійковий файл '{filePath}' створено.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
        }
    }
}
