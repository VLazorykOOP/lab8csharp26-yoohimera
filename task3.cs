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

        string inputFilePath = "input_task3.txt";
        string outputFilePath = "output_task3.txt";

        PrepareInputFile(inputFilePath);

        if (!File.Exists(inputFilePath))
        {
            Console.WriteLine($"Помилка: Вхідний файл {inputFilePath} не знайдено.");
            return;
        }

        string text = File.ReadAllText(inputFilePath);
        Console.WriteLine("--- Вхідний текст (Вірш) ---");
        Console.WriteLine(text);
        Console.WriteLine("---------------------\n");

        MatchCollection matches = Regex.Matches(text, @"[\w']+");
        List<string> words = new List<string>();
        foreach (Match match in matches)
        {
            words.Add(match.Value);
        }

        if (words.Count == 0)
        {
            Console.WriteLine("У тексті не знайдено слів.");
            return;
        }

        int currentStartIndex = 0;
        int currentLength = 1;
        int maxStartIndex = 0;
        int maxLength = 1;

        for (int i = 1; i < words.Count; i++)
        {
            if (words[i].Length == words[i - 1].Length)
            {
                currentLength++;
            }
            else
            {
                if (currentLength > maxLength)
                {
                    maxLength = currentLength;
                    maxStartIndex = currentStartIndex;
                }
                currentStartIndex = i;
                currentLength = 1;
            }
        }

        if (currentLength > maxLength)
        {
            maxLength = currentLength;
            maxStartIndex = currentStartIndex;
        }

        List<string> longestChain = new List<string>();
        for (int i = maxStartIndex; i < maxStartIndex + maxLength; i++)
        {
            longestChain.Add(words[i]);
        }

        int targetWordLength = longestChain[0].Length;
        string resultMessage = $"Найдовший ланцюжок складається з {maxLength} слів (довжина кожного — {targetWordLength} симв.):\n" +
                               string.Join(" -> ", longestChain);

        Console.WriteLine("--- Результат роботи алгоритму ---");
        Console.WriteLine(resultMessage);
        Console.WriteLine("----------------------------------");

        File.WriteAllText(outputFilePath, resultMessage, Encoding.UTF8);
        Console.WriteLine($"\nРезультат збережено у файл: {outputFilePath}");
    }

    static void PrepareInputFile(string filePath)
    {
        if (File.Exists(filePath)) File.Delete(filePath);

        string sampleContent = 
            "Вечірнє сонце, дякую за день!\n" +
            "Вечірнє сонце, дякую за втому.\n" +
            "За цю от тишу, що навколо, тут і там,\n" +
            "за кожен спалах у моєму серці.\n\n" +
            "Осінній день, osinnij den, осінній!\n" +
            "О ні, цей світ не став уже пустий.\n" +
            "Хай буде людська рідна мова, край і дім,\n" +
            "і цей прекрасний, дивний, чистий світ."; // дивний (6) -> чистий (6)
        
        File.WriteAllText(filePath, sampleContent, Encoding.UTF8);
    }
}
