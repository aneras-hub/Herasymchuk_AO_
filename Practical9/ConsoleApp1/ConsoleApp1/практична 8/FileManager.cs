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

            if (Path.GetExtension(filePath).ToLower() != ".json")
                throw new InvalidFileFormatException("Файл повинен мати формат .json");

            try
            {
                string json = File.ReadAllText(filePath, Encoding.UTF8);
                return JsonSerializer.Deserialize<T>(json, options);
            }
            catch (JsonException ex)
            {
                throw new JsonException("Помилка десеріалізації JSON.", ex);
            }
            catch (UnauthorizedAccessException ex)
            {
                throw new UnauthorizedAccessException("Немає доступу до файлу.", ex);
            }
            catch (IOException ex)
            {
                throw new IOException("Помилка роботи з файлом.", ex);
            }
        }

        public void SaveToText(string content, string filePath)
        {
            using StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8);
            writer.Write(content);
        }

        public string ReadFromText(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("Шлях до файлу порожній.");

            if (!File.Exists(filePath))
                throw new FileNotFoundException("Файл не знайдено.", filePath);

            return File.ReadAllText(filePath, Encoding.UTF8);
        }

        public void ExportToCsv(StudentGroup group, string filePath)
        {
            string csv = group.ExportToCsv();
            File.WriteAllText(filePath, csv, Encoding.UTF8);
        }
        public void CreateBackup(string sourcePath)
        {
            try
            {
                if (!File.Exists(sourcePath))
                    throw new FileNotFoundException(
                        "Файл для резервної копії не знайдено.",
                        sourcePath
                    );

                Directory.CreateDirectory("Backups");

                string fileName = Path.GetFileNameWithoutExtension(sourcePath);
                string extension = Path.GetExtension(sourcePath);

                string backupPath = Path.Combine(
                    "Backups",
                    $"{fileName}_backup_{DateTime.Now:yyyyMMdd_HHmmss}{extension}"
                );

                File.Copy(sourcePath, backupPath, true);
            }
            catch (UnauthorizedAccessException ex)
            {
                throw new UnauthorizedAccessException(
                    "Немає доступу до створення резервної копії.",
                    ex
                );
            }
            catch (IOException ex)
            {
                throw new IOException(
                    "Помилка створення резервної копії.",
                    ex
                );
            }
        }

        public void CleanOldBackups(int daysOld)
        {
            Directory.CreateDirectory("Backups");

            string[] files = Directory.GetFiles("Backups");

            foreach (string file in files)
            {
                FileInfo info = new FileInfo(file);

                if (info.CreationTime < DateTime.Now.AddDays(-daysOld))
                {
                    info.Delete();
                }
            }
        }



        public void CreateDefaultDirectories()
        {
            Directory.CreateDirectory("Backups");
            Directory.CreateDirectory("Reports");
            Directory.CreateDirectory("Logs");
        }
        public void CopyFile(string sourcePath, string destinationPath)
        {
            if (!File.Exists(sourcePath))
                throw new FileNotFoundException("Файл для копіювання не знайдено.", sourcePath);

            File.Copy(sourcePath, destinationPath, true);
        }
        public void MoveFile(string sourcePath, string destinationPath)
        {
            if (!File.Exists(sourcePath))
                throw new FileNotFoundException("Файл для переміщення не знайдено.", sourcePath);

            File.Move(sourcePath, destinationPath, true);
        }
        public void DeleteFile(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Файл для видалення не знайдено.", filePath);

            File.Delete(filePath);
        }
        public string[] GetDirectoryFiles(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
                throw new DirectoryNotFoundException("Папку не знайдено.");

            return Directory.GetFiles(directoryPath);
        }





        public void ImportStudentsFromCsv(StudentGroup group, string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("CSV-файл не знайдено.", filePath);

            string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);

            foreach (string line in lines.Skip(1))
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                string[] parts = line.Split(';');

                if (parts.Length < 3)
                    continue;

                try
                {
                    Student student = new Student(
                        parts[0].Trim(),
                        DateTime.Now.AddYears(-18),
                        parts[2].Trim(),
                        DateTime.Now,
                        parts[1].Trim()
                    );

                    group.AddStudent(student);
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Тип помилки: {ex.GetType().Name}");
                    Console.WriteLine($"Повідомлення: {ex.Message}");
                }
                Console.WriteLine("Імпорт студентів завершено.");
            }
        }
    }
}