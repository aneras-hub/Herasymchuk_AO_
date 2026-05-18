using System;
using System.Text;
using System.Linq;
using ConsoleApp1;
using System.IO;
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
            Console.WriteLine("3. Вивести студентів з лабораторними");
            Console.WriteLine("4. Пошук");
            Console.WriteLine("5. Редагування");
            Console.WriteLine("6. Фільтр");
            Console.WriteLine("7. Додати оцінку");
            Console.WriteLine("8. Ініціалізувати матрицю портів");
            Console.WriteLine("9. Відкрити / закрити порт");
            Console.WriteLine("10. Записати дані в порт");
            Console.WriteLine("11. Прочитати дані з порту");
            Console.WriteLine("12. Вивести матрицю портів");
            Console.WriteLine("13. Прив'язати студента до порту");
            Console.WriteLine("14. Симулювати лабораторну");
            Console.WriteLine("15. Переглянути лог портів");
            Console.WriteLine("16. Зберегти дані");
            Console.WriteLine("17. Завантажити дані");
            Console.WriteLine("18. Статистика");
            Console.WriteLine("19. Пошук відкритих портів");
            Console.WriteLine("20. Великий звіт");
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
                case "7": AddGrade(group); break;
                case "8": InitializePorts(group); break;
                case "9": TogglePort(group); break;
                case "10": WriteToPort(group); break;
                case "11": ReadFromPort(group); break;
                case "12": ShowMatrix(group); break;
                case "13": AssignStudent(group); break;
                case "14": SimulateLab(group); break;
                case "15": ShowLogs(group); break;
                case "16": group.SaveToFile(fileName); break;
                case "17": group.LoadFromFile(fileName); break;
                case "18": Stats(group); break;
                case "19": SearchOpenPorts(group); break;
                case "20": BigReport(group); break;
                case "0": return;
                default: Console.WriteLine("Невірний вибір"); break;
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
            {
                student.Journal.SetGrade("Старт", g);
            }

            group.AddStudent(student);

            Console.WriteLine("Студента додано");
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

        var all = group.FindByName("");

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
        Console.Write("Пошук: ");
        string q = Console.ReadLine();

        var list = group.FindByName(q);

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

        var s = group.FindByRecordBook(id.ToString());
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
                updated.Journal.SetGrade("Редагований бал", g);
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
            group.FindByName("").Where(s => s.IsFailing()).ToList()
                .ForEach(s => Console.WriteLine(s.ShowDetailedInfo()));
    }
    // Статистика
    static void Stats(StudentGroup group)
    {
        Console.WriteLine($"Кількість студентів: {group.GroupSize}");
        Console.WriteLine($"Середній бал групи: {group.AverageGroupGrade}");
        Console.WriteLine($"Активних портів: {group.GetOpenedPortsCount()}");
    }
    static string Read(string msg)
    {
        Console.Write(msg);
        return Console.ReadLine();
    }
    static void AddGrade(StudentGroup group)
    {
        Console.Write("Номер студента: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Невірний номер");
            return;
        }

        Student student = group.FindByRecordBook(id.ToString());

        if (student == null)
        {
            Console.WriteLine("Студента не знайдено");
            return;
        }
        Console.Write("Предмет: ");
        string subject = Console.ReadLine();
        Console.Write("Оцінка: ");
        if (double.TryParse(Console.ReadLine(), out double grade))
        {
            student.Journal.SetGrade(subject, grade);

            Console.WriteLine("Оцінку додано");
        }
        else
        {
            Console.WriteLine("Невірна оцінка");
        }
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
    // запуск порта
    static void InitializePorts(StudentGroup group)
    {
        group.GetPortMatrix().ToString();

        Console.WriteLine("Матриця портів 16x16 успішно ініціалізована");
    }

    static void TogglePort(StudentGroup group)
    {
        int row = ReadInt("Ряд: ");
        int col = ReadInt("Стовпець: ");

        Port port = group.GetPortMatrix().GetPort(row, col);

        if (port.IsOpen)
        {
            group.GetPortMatrix().ClosePort(row, col);

            Console.WriteLine("Порт закрито");
        }
        else
        {
            group.GetPortMatrix().OpenPort(row, col);

            Console.WriteLine("Порт відкрито");
        }
    }
    static void WriteToPort(StudentGroup group)
    {
        int row = ReadInt("Ряд: ");
        int col = ReadInt("Стовпець: ");

        Console.Write("Дані: ");

        string text = Console.ReadLine();

        byte[] data = Encoding.UTF8.GetBytes(text);

        try
        {
            group.GetPortMatrix().WriteToPort(row, col, data);

            Console.WriteLine("Дані записані");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    static void ReadFromPort(StudentGroup group)
    {
        int row = ReadInt("Ряд: ");
        int col = ReadInt("Стовпець: ");

        try
        {
            byte[] data = group.GetPortMatrix().ReadFromPort(row, col);

            string text = Encoding.UTF8.GetString(data);

            Console.WriteLine($"\nДані з порту:\n{text}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    static void ShowMatrix(StudentGroup group)
    {
        Console.WriteLine(
            group.GetPortMatrix().ScanMatrix()
        );
    }
    static void AssignStudent(StudentGroup group)
    {
        Console.Write("Номер студента: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Невірний номер");
            return;
        }

        Student student = group.FindByRecordBook(id.ToString());

        if (student == null)
        {
            Console.WriteLine("Студента не знайдено");
            return;
        }

        int row = ReadInt("Ряд порту: ");
        int col = ReadInt("Стовпець порту: ");

        try
        {
            group.AssignStudentToPort(student, row, col);

            Console.WriteLine("Студента прив'язано до порту");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    static void SimulateLab(StudentGroup group)
    {
        Console.Write("Номер студента: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Невірний номер");
            return;
        }

        Student student = group.FindByRecordBook(id.ToString());

        if (student == null)
        {
            Console.WriteLine("Студента не знайдено");
            return;
        }

        int lab = ReadInt("Номер лабораторної: ");

        Console.Write("Оцінка: ");

        if (!byte.TryParse(Console.ReadLine(), out byte grade))
        {
            Console.WriteLine("Невірна оцінка");
            return;
        }

        Console.Write("Дані для порту: ");

        string text = Console.ReadLine();

        byte[] data = Encoding.UTF8.GetBytes(text);

        group.SimulateLabWork(student, lab, grade, data);

        Console.WriteLine("Лабораторну виконано");
    }
    static void ShowLogs(StudentGroup group)
    {
        Console.WriteLine(group.GetPortLogs());
    }
    static int ReadInt(string msg)
    {
        while (true)
        {
            Console.Write(msg);

            if (int.TryParse(Console.ReadLine(), out int value))
            {
                return value;
            }

            Console.WriteLine("Невірне число");
        }
    }
    static void SearchOpenPorts(StudentGroup group)
    {
        Console.Write("Назва пристрою: ");

        string device = Console.ReadLine();

        var ports = group.GetPortMatrix()
                         .GetOpenPortsByDevice(device);

        if (ports.Count == 0)
        {
            Console.WriteLine("Нічого не знайдено");
            return;
        }

        foreach (var port in ports)
        {
            Console.WriteLine(port.GetPortInfo());
        }
    }
    static void BigReport(StudentGroup group)
    {
        string report = group.GenerateBigReport();

        Console.WriteLine(report);

        File.WriteAllText("big_report.txt", report);

        Console.WriteLine("Звіт збережено у файл big_report.txt");
    }
}