using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsoleApp1.практична_8
{
    public class FileManager
    {
        private readonly JsonSerializerOptions options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        public void SaveToJson<T>(T data, string filePath)
        {
            string json = JsonSerializer.Serialize(data, options);
            File.WriteAllText(filePath, json, Encoding.UTF8);
        }

        public T LoadFromJson<T>(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Файл не знайдено.", filePath);

            string json = File.ReadAllText(filePath, Encoding.UTF8);
            return JsonSerializer.Deserialize<T>(json, options);
        }

        public void SaveToText(string content, string filePath)
        {
            using StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8);
            writer.Write(content);
        }

        public string ReadFromText(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Файл не знайдено.", filePath);

            return File.ReadAllText(filePath, Encoding.UTF8);
        }

        public void ExportToCsv(StudentGroup group, string filePath)
        {
            string csv = group.ExportToCsv();
            File.WriteAllText(filePath, csv, Encoding.UTF8);
        }
    }
}