using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    // ПРАКТИЧНА ТРИ 2
    public class TextProcessor
    {
        public string Reverse(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;
            char[] chars = input.ToCharArray();
            Array.Reverse(chars);
            return new string(chars);
        }
        public int CountWords(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return 0;
            return text.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length;
        }
        public int CountCharacters(string text, bool ignoreWhitespace = true)
        {
            if (string.IsNullOrEmpty(text))
                return 0;
            if (ignoreWhitespace)
            {
                text = text.Replace(" ", "");
            }
            return text.Length;
        }
        public string Normalize(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;

            return string.Join(
                " ",
                text.Split(' ', StringSplitOptions.RemoveEmptyEntries)
            ).Trim();
        }
        public bool IsPalindrome(string text, bool ignoreCase = true, bool ignoreSpaces = true)
        {
            if (string.IsNullOrWhiteSpace(text))
                return false;
            string processed = text;
            if (ignoreSpaces)
            {
                processed = processed.Replace(" ", "");
            }
            if (ignoreCase)
            {
                processed = processed.ToLower();
            }
            string reversed = Reverse(processed);
            return processed == reversed;
        }
        public string ReplaceMultiple(string text, Dictionary<string, string> replacements)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;
            foreach (var pair in replacements)
            {
                text = text.Replace(
                    pair.Key,
                    pair.Value,
                    StringComparison.OrdinalIgnoreCase
                );
            }
            return text;
        }
        public string[] SplitIntoSentences(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return Array.Empty<string>();
            return text.Split(new char[] { '.', '!', '?' },
                StringSplitOptions.RemoveEmptyEntries
            );
        }
        public string BuildGroupReport(StudentGroup group)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("=== ЗВІТ ГРУПИ ===");
            sb.AppendLine($"Група: {group.GroupName}");
            sb.AppendLine($"Спеціальність: {group.Specialty}");
            sb.AppendLine($"Курс: {group.Course}");
            sb.AppendLine($"Кількість студентів: {group.GroupSize}");
            sb.AppendLine($"Середній бал групи: {group.AverageGroupGrade}");
            sb.AppendLine();
            sb.AppendLine("=== СТУДЕНТИ ===");
            foreach (var student in group.FindByName(""))
            {
                sb.AppendLine(
                    student.GetFormattedInfo(true)
                );
            }
            return sb.ToString();
        }
        public string ComparePerformance(int iterations)
        {
            Stopwatch swString = new Stopwatch();
            string text = "";
            swString.Start();
            for (int i = 0; i < iterations; i++)
            {
                text += i;
            }
            swString.Stop();
            Stopwatch swBuilder = new Stopwatch();
            StringBuilder sb = new StringBuilder();
            swBuilder.Start();
            for (int i = 0; i < iterations; i++)
            {
                sb.Append(i);
            }
            swBuilder.Stop();
            return
                $"STRING: {swString.ElapsedMilliseconds} ms\n" +
                $"STRINGBUILDER: {swBuilder.ElapsedMilliseconds} ms";
        }
    }
    // Кінцева практика 3
}
