using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApp1
{
    public class AdvancedLogger
    {
        private StringBuilder logs = new StringBuilder();
        public void Log(string level, string message)
        {
            logs.AppendLine(
                $"[{DateTime.Now:dd.MM.yyyy HH:mm:ss}] " +
                $"[{level.ToUpper()}] " +
                $"{message}"
            );
        }
        public void SaveToFile(string path)
        {
            File.WriteAllText(path, logs.ToString());
        }
        public string GetLogsByLevel(string level)
        {
            StringBuilder result = new StringBuilder();
            string[] lines = logs.ToString().Split('\n', StringSplitOptions.RemoveEmptyEntries);
            foreach (string line in lines)
            {
                if (line.Contains(
                    $"[{level.ToUpper()}]",
                    StringComparison.OrdinalIgnoreCase))
                {
                    result.AppendLine(line);
                }
            }
            return result.ToString();
        }
        public void Clear()
        {
            logs.Clear();
        }
        public string GetLast(int count)
        {
            var lines = logs.ToString()
                .Split('\n', StringSplitOptions.RemoveEmptyEntries)
                .TakeLast(count);
            StringBuilder result = new StringBuilder();
            foreach (var line in lines)
            {
                result.AppendLine(line);
            }
            return result.ToString();
        }
        // Кінцева практика 3







        public void ExecuteLogAction(string message, Action<string> logger)
        {
            logger(message);
        }
    }
}
