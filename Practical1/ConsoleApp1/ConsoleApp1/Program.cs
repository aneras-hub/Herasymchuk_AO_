using System;
using System.Text;
using System.Linq;
using ConsoleApp1;

class Program
{
    static void Main()
    {
        // Ініціалізація групи з даними
        StudentGroup group = new StudentGroup
        {
            GroupName = "К-461",
            Specialty = "Комп'ютерні науки",
            Course = 3
        };
        //назва файлу json
        const string fileName = "students.json";
        // головне меню
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"=== ГРУПА {group.GroupName} ===");
            Console.WriteLine("1. Додати студента");
            Console.WriteLine("2. Видалити студента");
            Console.WriteLine("3. Вивести всіх (пагінація)");
            Console.WriteLine("4. Пошук");
            Console.WriteLine("5. Редагування");
            Console.WriteLine("6. Фільтр");
            Console.WriteLine("7. Статистика");
            Console.WriteLine("8. Зберегти");
            Console.WriteLine("9. Завантажити");
            Console.WriteLine("10. Додати оцінку");
            Console.WriteLine("0. Вихід");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1": AddStudent(group); break;
                case "2": RemoveStudent(group); break;
                case "3": ShowAll(group); break;
                case "4": Search(group); break;
                case "5": Edit(group); break;
                case "6": Filter(group); break;
                case "7": Stats(group); break;
                case "8": group.SaveToFile(fileName); break;
                case "9": group.LoadFromFile(fileName); break;
                case "10": AddGrade(group); break;
                case "0": return;
            }

            Console.WriteLine("\nНатисніть клавішу...");
            Console.ReadKey();
        }
    }
    //Додавання студента з валідацією та обробкою помилок
    static void AddStudent(StudentGroup group)
    {
        try
        {
            string name = Read("ПІБ: ");
            string record = Read("Залікова (8 цифр): ");
            string email = Read("Email: ");
            DateTime dob = ReadDate("Дата народження: ");
            DateTime enroll = ReadDate("Дата вступу: ");

            var student = new Student
            {
                fullName = name,
                recordBookNumber = record,
                personalEmail = email,
                DateOfBirth = dob,
                EnrollmentDate = enroll
            };

            Console.Write("Початковий бал: ");
            if (double.TryParse(Console.ReadLine(), out double g))
                student.Journal.SetGrade("Старт", g);

            group.AddStudent(student);

            Console.WriteLine("Додано");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    // Видалення студента з обробкою помилок
    static void RemoveStudent(StudentGroup group)
    {
        Console.Write("Номер: ");
        group.RemoveStudent(Console.ReadLine());
    }
    // Вивід з пагінацією
    static void ShowAll(StudentGroup group)
    {
        int page = 0;
        int size = 10;

        var all = group.FindStudent("");

        while (true)
        {
            Console.Clear();
            var items = all.Skip(page * size).Take(size);

            foreach (var s in items)
                Console.WriteLine(s.ShowDetailedInfo());

            Console.WriteLine("[N] next | [P] prev | [Q] exit");
            var k = Console.ReadKey().Key;

            if (k == ConsoleKey.N) page++;
            else if (k == ConsoleKey.P && page > 0) page--;
            else if (k == ConsoleKey.Q) break;
        }
    }
    // Пошук
    static void Search(StudentGroup group)
    {
        Console.Write("Пошук за ім'ям: ");
        string q = Console.ReadLine();

        var list = group.FindStudent(q);

        if (list.Count == 0)
            Console.WriteLine("Нічого не знайдено");
        else
            list.ForEach(s => Console.WriteLine(s.ShowDetailedInfo()));
    }
    // Редагування з валідацією
    static void Edit(StudentGroup group)
    {
        Console.Write("Номер: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Невірний формат");
            return;
        }

        var s = group.FindStudent(id);
        if (s == null)
        {
            Console.WriteLine("Не знайдено");
            return;
        }

        Console.Write($"Нове ім'я (Enter = залишити {s.fullName}): ");
        string name = Console.ReadLine();
        // якщо ім'я порожнє, залишаємо старе, інакше використовуємо нове
        string finalName = string.IsNullOrWhiteSpace(name) ? s.fullName : name;
        // створюємо новий об'єкт з оновленим ім'ям, щоб не порушувати принцип незмінності
        Student updated;
        // обробка помилок при створенні нового об'єкта
        try
        {
            updated = new Student
            {
                fullName = finalName,
                recordBookNumber = s.recordBookNumber,
                personalEmail = s.personalEmail,
                DateOfBirth = s.DateOfBirth,
                EnrollmentDate = s.EnrollmentDate,
                Notes = s.Notes,
                Journal = s.Journal
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
            return;
        }
        // запит на оновлення середнього балу
        Console.Write("Новий бал (Enter = пропустити): ");
        string gradeInput = Console.ReadLine();

        if (double.TryParse(gradeInput, out double g))
        {
            try
            {
                updated.UpdateAverageGrade(g);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        // видаляємо старого студента та додаємо оновленого
        group.RemoveStudent(s.recordBookNumber);
        group.AddStudent(updated);

        Console.WriteLine("Оновлено");
    }
    // Фільтр відмінників та двієчників
    static void Filter(StudentGroup group)
    {
        Console.WriteLine("1 - Відмінники");
        Console.WriteLine("2 - Неуспішні");

        var c = Console.ReadLine();

        if (c == "1")
            group.GetExcellentStudents().ForEach(s => Console.WriteLine(s.ShowDetailedInfo()));
        else
            group.FindStudent("").Where(s => s.IsFailing()).ToList()
                .ForEach(s => Console.WriteLine(s.ShowDetailedInfo()));
    }
    // Статистика
    static void Stats(StudentGroup group)
    {
        Console.WriteLine($"Кількість: {group.GroupSize}");
        Console.WriteLine($"Середній: {group.AverageGroupGrade}");
    }
    // Додавання оцінки з валідацією
    static void AddGrade(StudentGroup group)
    {
        Console.Write("Номер: ");
        if (!int.TryParse(Console.ReadLine(), out int id)) return;

        var s = group.FindStudent(id);
        if (s == null) return;

        Console.Write("Предмет: ");
        string sub = Console.ReadLine();

        Console.Write("Оцінка: ");
        if (double.TryParse(Console.ReadLine(), out double g))
            s.Journal.SetGrade(sub, g);
    }
    // Допоміжні методи для читання з консолі з валідацією
    static string Read(string msg)
    {
        Console.Write(msg);
        return Console.ReadLine();
    }
    // Метод для читання дати з валідацією
    static DateTime ReadDate(string msg)
    {
        while (true)
        {
            Console.Write(msg);
            if (DateTime.TryParse(Console.ReadLine(), out var d))
                return d;
            Console.WriteLine("Невірна дата");
        }
    }
}