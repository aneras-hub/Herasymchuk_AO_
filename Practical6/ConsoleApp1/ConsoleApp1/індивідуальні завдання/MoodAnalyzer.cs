using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp1
{
    //ПРАКТИЧНА 3 ІНДИВІДУАЛЬНЕ ЗАВДАННЯ
    public class MoodAnalyzer
    {
        private readonly string[] positiveWords = { "відмінно", "добре", "чудово", "успіх", "класно" };
        private readonly string[] negativeWords = { "погано", "проблема", "важко", "жах", "незадовільно" };
        private readonly string[] neutralWords = { "відпустка", "канікули", "перерва" };
        public string AnalyzeGroupMood(StudentGroup group)
        {
            int positive = 0;
            int negative = 0;
            int neutral = 0;
            foreach (var student in group.GetAllStudents())
            {
                string notes = student.Notes.ToLower();

                positive += CountKeywords(notes, positiveWords);
                negative += CountKeywords(notes, negativeWords);
                neutral += CountKeywords(notes, neutralWords);
            }

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("=== АНАЛІЗ НАСТРОЮ ГРУПИ ===");
            sb.AppendLine($"Позитивні слова: {positive}");
            sb.AppendLine($"Негативні слова: {negative}");
            sb.AppendLine($"Нейтральні слова: {neutral}");

            if (positive > negative)
                sb.AppendLine("Настрій групи: ПОЗИТИВНИЙ");
            else if (negative > positive)
                sb.AppendLine("Настрій групи: НЕГАТИВНИЙ");
            else
                sb.AppendLine("Настрій групи: НЕЙТРАЛЬНИЙ");

            return sb.ToString();
        }

        private int CountKeywords(string text, string[] keywords)
        {
            int count = 0;

            string[] words = text
                .ToLower()
                .Split(new char[]
                {
            ' ', ',', '.', '!', '?', ';', ':', '\n', '\r'
                },
                StringSplitOptions.RemoveEmptyEntries);

            foreach (string word in words)
            {
                if (keywords.Contains(word))
                {
                    count++;
                }
            }

            return count;
        }
    }
    //К
}