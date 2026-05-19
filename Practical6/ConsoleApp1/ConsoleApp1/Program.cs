using System;
using System.Text;
using System.Linq;
using ConsoleApp1;
using System.IO;
using ConsoleApp1.студенти;
using ConsoleApp1.порти;
using ConsoleApp1.машинки;
using ConsoleApp1.практична_6.абстракція;
using ConsoleApp1.практична_6;
using ConsoleApp1.практична_6.інтерфейс;
class Program
{
    static void Main()
    {
        StudentGroup group = new StudentGroup
        {
            GroupName = "К-461",
            Specialty = "Комп'ютерні науки",
            Course = 3
        };
        List<Vehicle> vehicles = new List<Vehicle>();
        const string fileName = "students.json";
        TextProcessor textProcessor = new TextProcessor();
        AdvancedLogger advancedLogger = new AdvancedLogger();
        MoodAnalyzer moodAnalyzer = new MoodAnalyzer();
        Console.Clear();

        Console.WriteLine($"=== ГРУПА {group.GroupName} ===");
        Console.WriteLine("1. Основне меню");
        Console.WriteLine("2. Робота з текстом та звітами");
        Console.WriteLine("3. Перевантаження операторів");
        Console.WriteLine("4. Наслідування та поліморфізм");
        Console.WriteLine("5. Транспортні засоби");
        Console.WriteLine("6. Поліморфізм фігур");
        Console.WriteLine("0. Вихід");

        while (true)
        {
            Console.Clear();

            Console.WriteLine($"=== ГРУПА {group.GroupName} ===");
            Console.WriteLine("1. Основне меню");
            Console.WriteLine("2. Робота з текстом та звітами");
            Console.WriteLine("3. Перевантаження операторів");
            Console.WriteLine("4. Наслідування та поліморфізм");
            Console.WriteLine("5. Транспортні засоби");
            Console.WriteLine("6. Поліморфізм фігур");
            Console.WriteLine("0. Вихід");

            string mainChoice = Console.ReadLine();

            switch (mainChoice)
            {
                case "1":
                    MainMenu(group, fileName, textProcessor, advancedLogger);
                    break;

                case "2":
                    TextReportsMenu(group, textProcessor, advancedLogger, moodAnalyzer);
                    break;
                case "3":
                    OperatorsMenu(group);
                    break;
                case "4":
                    InheritanceMenu(group);
                    break;
                case "5":
                    VehicleMenu(vehicles);
                    break;
                case "6":
                    ShapesMenu(group);
                    break;
                case "0":
                    return;

                default:
                    Console.WriteLine("Невірний вибір");
                    Console.ReadKey();
                    break;
            }
        }
        static void MainMenu(StudentGroup group, string fileName, TextProcessor textProcessor, AdvancedLogger advancedLogger)
        {
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
        static void TextReportsMenu(StudentGroup group, TextProcessor textProcessor, AdvancedLogger advancedLogger, MoodAnalyzer moodAnalyzer)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. Пошук за фрагментом ПІБ");
                Console.WriteLine("2. Повний звіт групи");
                Console.WriteLine("3. Експорт групи у CSV");
                Console.WriteLine("4. Імпорт студентів з тексту");
                Console.WriteLine("5. Нормалізація нотаток");
                Console.WriteLine("6. Перевірка паліндромів");
                Console.WriteLine("7. Порівняння продуктивності");
                Console.WriteLine("8. Обробка тексту");
                Console.WriteLine("9. Перегляд логів системи");
                Console.WriteLine("10. Аналіз настрою групи");
                Console.WriteLine("11. Зберегти дані");
                Console.WriteLine("12. Завантажити дані");

                Console.WriteLine("0. Вихід");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": SearchByFragment(group); break;
                    case "2": FullGroupReport(group, textProcessor); break;
                    case "3": ExportCsv(group); break;
                    case "4": ImportStudents(group); break;
                    case "5": NormalizeNotes(group, textProcessor); break;
                    case "6": CheckPalindromes(group, textProcessor); break;
                    case "7": ComparePerformance(textProcessor); break;
                    case "8": TextMenu(textProcessor); break;
                    case "9": ShowAdvancedLogs(advancedLogger); break;
                    case "10": AnalyzeMood(group, moodAnalyzer); break;
                    case "11": group.SaveToFile(fileName); break;
                    case "12": group.LoadFromFile(fileName); break;
                    case "0": return;
                    default: Console.WriteLine("Невірний вибір"); break;
                }
                Console.WriteLine("\nНатисніть клавішу...");
                Console.ReadKey();
            }
        }
        static void OperatorsMenu(StudentGroup group)
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("=== ПЕРЕВАНТАЖЕННЯ ОПЕРАТОРІВ ===");
                Console.WriteLine("1. Порівняти двох студентів");
                Console.WriteLine("2. Об'єднати дві групи");
                Console.WriteLine("3. Демонстрація Vector");
                Console.WriteLine("4. Демонстрація GradePoint");
                Console.WriteLine("5. Знайти найкращого студента");
                Console.WriteLine("6. Тестування операторів груп");
                Console.WriteLine("7. Демонстрація Fraction");
                Console.WriteLine("0. Вихід");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CompareStudentsMenu(group);
                        break;

                    case "2":
                        MergeGroupsMenu(group);
                        break;

                    case "3":
                        VectorDemo();
                        break;

                    case "4":
                        GradePointDemo();
                        break;

                    case "5":
                        BestStudentDemo(group);
                        break;
                    case "6":
                        CompareGroupsDemo(group);
                        break;
                    case "7":
                        FractionDemo();
                        break;
                    case "0":
                        return;

                    default:
                        Console.WriteLine("Невірний вибір");
                        break;
                }

                Console.WriteLine("\nНатисніть клавішу...");
                Console.ReadKey();
            }
        }
        static void InheritanceMenu(StudentGroup group)
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("=== ПРАКТИЧНА 5: НАСЛІДУВАННЯ ===");
                Console.WriteLine("1. Додати звичайного студента");
                Console.WriteLine("2. Додати відмінника");
                Console.WriteLine("3. Додати іноземного студента");
                Console.WriteLine("4. Додати працюючого студента");
                Console.WriteLine("5. Вивести всіх членів університету");
                Console.WriteLine("6. Розрахувати стипендію для всіх");
                Console.WriteLine("7. Показати студентів конкретного типу");
                Console.WriteLine("8. Тестування base/override");
                Console.WriteLine("0. Назад");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": AddRegularStudent(group); break;
                    case "2": AddExcellentStudent(group); break;
                    case "3": AddForeignStudent(group); break;
                    case "4": AddWorkingStudent(group); break;
                    case "5": ShowUniversityMembers(group); break;
                    case "6": ShowTotalScholarship(group); break;
                    case "7": ShowMembersByType(group); break;
                    case "8": TestHierarchy(group); break;
                    case "0": return;
                    default: Console.WriteLine("Невірний вибір"); break;
                }

                Console.WriteLine("\nНатисніть клавішу...");
                Console.ReadKey();
            }
        }
        static void VehicleMenu(List<Vehicle> vehicles)
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("=== ВАРІАНТ 2: ТРАНСПОРТНІ ЗАСОБИ ===");
                Console.WriteLine("1. Додати легковий автомобіль");
                Console.WriteLine("2. Додати автобус");
                Console.WriteLine("3. Додати вантажівку");
                Console.WriteLine("4. Показати весь транспорт");
                Console.WriteLine("5. Розрахувати загальну вартість обслуговування");
                Console.WriteLine("0. Назад");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        vehicles.Add(new Car("Toyota Corolla", 2020, "AA1234BB", 5));
                        Console.WriteLine("Автомобіль додано.");
                        break;

                    case "2":
                        vehicles.Add(new Bus("Bogdan A092", 2018, "BC5678CC", 30));
                        Console.WriteLine("Автобус додано.");
                        break;

                    case "3":
                        vehicles.Add(new Truck("MAN TGS", 2019, "KA9999AA", 12.5));
                        Console.WriteLine("Вантажівку додано.");
                        break;

                    case "4":
                        foreach (var vehicle in vehicles)
                        {
                            Console.WriteLine(vehicle.GetInfo());
                            Console.WriteLine("--------------------");
                        }
                        break;

                    case "5":
                        Console.WriteLine($"Загальна вартість обслуговування: {vehicles.Sum(v => v.CalculateServiceCost())} грн");
                        break;

                    case "0":
                        return;

                    default:
                        Console.WriteLine("Невірний вибір");
                        break;
                }

                Console.WriteLine("\nНатисніть клавішу...");
                Console.ReadKey();
            }
        }
        static void ShapesMenu(StudentGroup group)
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("=== ПРАКТИЧНА 6: ПОЛІМОРФІЗМ ФІГУР ===");
                Console.WriteLine("1. Додати нову фігуру");
                Console.WriteLine("2. Вивести всі фігури");
                Console.WriteLine("3. Розрахувати загальну площу всіх фігур");
                Console.WriteLine("4. Змінити розмір всіх фігур");
                Console.WriteLine("5. Намалювати всі фігури");
                Console.WriteLine("6. Показати інформацію через IPrintable");
                Console.WriteLine("7. Демонстрація динамічного зв’язування");
                Console.WriteLine("0. Назад");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddShapeToStudent(group);
                        break;

                    case "2":
                        ShowAllShapes(group);
                        break;

                    case "3":
                        Console.WriteLine($"Загальна площа: {group.GetTotalAreaOfAllShapes():F2}");
                        break;

                    case "4":
                        double factor = ReadDouble("Коефіцієнт зміни розміру: ");
                        group.ResizeAllShapes(factor);
                        Console.WriteLine("Розмір усіх фігур змінено.");
                        break;

                    case "5":
                        group.DrawAllShapes();
                        break;

                    case "6":
                        ShowPrintableInfo(group);
                        break;

                    case "7":
                        PolymorphismDemo(group);
                        break;

                    case "0":
                        return;

                    default:
                        Console.WriteLine("Невірний вибір");
                        break;
                }

                Console.WriteLine("\nНатисніть клавішу...");
                Console.ReadKey();
            }
        }
        // К
        static void AddStudent(StudentGroup group)
        {
            try
            {
                string name = Read("ПІБ: ");
                string record = Read("Залікова (8 цифр): ");
                string email = Read("Email: ");
                DateTime dob = ReadDate("Дата народження: ");
                DateTime enroll = ReadDate("Дата вступу: ");
                string note = Read("Нотатки: ");

                var student = new Student(name, dob, email, enroll, record, note);
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
        static void RemoveStudent(StudentGroup group)
        {
            Console.Write("Номер: ");
            group.RemoveStudent(Console.ReadLine());
        }
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
            Console.Write($"Нове ім'я (Enter = залишити {s.FullName}): ");
            string name = Console.ReadLine();
            string finalName = string.IsNullOrWhiteSpace(name) ? s.FullName : name;
            Student updated;
            try
            {
                updated = new Student(
                    finalName,
                    s.DateOfBirth,
                    s.PersonalEmail,
                    s.EnrollmentDate,
                    s.RecordBookNumber,
                    s.Notes
                );

                updated.Journal = s.Journal;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
                return;
            }
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
            group.RemoveStudent(s.RecordBookNumber);
            group.AddStudent(updated);

            Console.Write($"Нова нотатка (Enter = залишити поточну): ");
            string newNotes = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(newNotes))
            {
                updated.Notes = newNotes;
            }

            Console.WriteLine("Оновлено");
        }
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

        static void SearchByFragment(StudentGroup group)
        {
            Console.Write("Фрагмент ПІБ: ");
            string fragment = Console.ReadLine();
            Console.WriteLine(group.SearchByNameFragment(fragment));
        }
        static void ExportCsv(StudentGroup group)
        {
            string csv = group.ExportToCsv();
            File.WriteAllText("students.csv", csv);
            Console.WriteLine("CSV експортовано у students.csv");
        }
        static void ImportStudents(StudentGroup group)
        {
            Console.WriteLine("Введіть студентів:");
            Console.WriteLine("Формат:");
            Console.WriteLine("ПІБ;Залікова;Email");
            Console.WriteLine("Порожній рядок = завершення");
            StringBuilder sb = new StringBuilder();
            while (true)
            {
                string line = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                    break;
                sb.AppendLine(line);
            }
            group.ImportStudentsFromText(sb.ToString());
            Console.WriteLine("Імпорт завершено");
        }
        static void NormalizeNotes(StudentGroup group, TextProcessor processor)
        {
            foreach (var student in group.FindByName(""))
            {
                student.Notes = processor.Normalize(student.Notes);
            }
            Console.WriteLine("Нотатки нормалізовано");
        }
        static void CheckPalindromes(StudentGroup group, TextProcessor processor)
        {
            foreach (var student in group.FindByName(""))
            {
                bool result = processor.IsPalindrome(student.Notes);
                Console.WriteLine($"{student.FullName}: " + (result ? "Паліндром" : "Не паліндром"));
            }
        }
        static void ComparePerformance(TextProcessor processor)
        {
            Console.WriteLine(processor.ComparePerformance(100000));
        }
        static void TextMenu(TextProcessor processor)
        {
            Console.Write("Введіть текст: ");
            string text = Console.ReadLine();
            Console.WriteLine($"Реверс: {processor.Reverse(text)}");
            Console.WriteLine($"Слів: {processor.CountWords(text)}");
            Console.WriteLine($"Символів: {processor.CountCharacters(text)}");
            Console.WriteLine($"Нормалізований текст: {processor.Normalize(text)}");
        }
        static void ShowAdvancedLogs(AdvancedLogger logger)
        {
            Console.WriteLine("1. Додати лог");
            Console.WriteLine("2. Показати всі INFO");
            Console.WriteLine("3. Показати останні 5");
            Console.WriteLine("4. Очистити");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.Write("Рівень: ");
                    string level = Console.ReadLine();
                    Console.Write("Повідомлення: ");
                    string msg = Console.ReadLine();
                    logger.Log(level, msg);
                    Console.WriteLine("Лог додано");
                    break;
                case "2":
                    Console.WriteLine(logger.GetLogsByLevel("INFO"));
                    break;
                case "3":
                    Console.WriteLine(logger.GetLast(5));
                    break;
                case "4":
                    logger.Clear();
                    Console.WriteLine("Логи очищено");
                    break;
                case "0": return;
                default: Console.WriteLine("Невірний вибір"); break;
            }
        }
        static void FullGroupReport(StudentGroup group, TextProcessor processor)
        {
            string report = processor.BuildGroupReport(group);
            Console.WriteLine(report);
            File.WriteAllText("group_report.txt", report);

            Console.WriteLine("Звіт збережено у файл group_report.txt");
        }

        static void AnalyzeMood(StudentGroup group, MoodAnalyzer analyzer)
        {
            string result = analyzer.AnalyzeGroupMood(group);

            Console.WriteLine(result);
        }
        static void MergeGroupsMenu(StudentGroup group)
        {
            StudentGroup other = new StudentGroup
            {
                GroupName = "К-999",
                Specialty = "Тестова група",
                Course = 2
            };

            Student testStudent = new Student(
                "Іваненко Іван Іванович",
                new DateTime(2005, 1, 1),
                "test@test.com",
                DateTime.Now,
                "99999999"
            );

            testStudent.CourseProgress = 90;
            other.AddStudent(testStudent);

            StudentGroup merged = group + other;

            Console.WriteLine("Групи об'єднано");
            Console.WriteLine($"Нова кількість студентів: {merged.GroupSize}");
        }
        static void VectorDemo()
        {
            Vector v1 = new Vector(1, 2, 3);
            Vector v2 = new Vector(4, 5, 6);

            Console.WriteLine($"v1 = {v1}");
            Console.WriteLine($"v2 = {v2}");

            Console.WriteLine($"\nv1 + v2 = {v1 + v2}");
            Console.WriteLine($"v1 - v2 = {v1 - v2}");
            Console.WriteLine($"v1 * v2 = {v1 * v2}");

            Console.WriteLine($"\nv1 > v2 = {v1 > v2}");
            Console.WriteLine($"v1 < v2 = {v1 < v2}");

            v1++;
            Console.WriteLine($"\nПісля v1++ : {v1}");

            v2--;
            Console.WriteLine($"Після v2-- : {v2}");

            double len = (double)v1;

            Console.WriteLine($"\nДовжина v1 = {len:F2}");
        }
        static void BestStudentDemo(StudentGroup group)
        {
            Student best = group.BestStudent();

            if (best == null)
            {
                Console.WriteLine("У групі немає студентів");
                return;
            }

            Console.WriteLine("Найкращий студент:");
            Console.WriteLine(best.ShowDetailedInfo());
        }
        static void CompareGroupsDemo(StudentGroup group)
        {
            StudentGroup other = new StudentGroup
            {
                GroupName = "К-777",
                Specialty = "Тест",
                Course = 1
            };

            Student s = new Student(
                "Петров Петро Петрович",
                new DateTime(2004, 5, 5),
                "petro@test.com",
                DateTime.Now,
                "77777777"
            );

            s.CourseProgress = 70;

            s.Journal.SetGrade("C#", 60);

            other.AddStudent(s);

            Console.WriteLine($"Середній бал {group.GroupName}: {group.AverageGroupGrade}");
            Console.WriteLine($"Середній бал {other.GroupName}: {other.AverageGroupGrade}");

            Console.WriteLine();

            Console.WriteLine($"group > other = {group > other}");
            Console.WriteLine($"group < other = {group < other}");
            Console.WriteLine($"group >= other = {group >= other}");
            Console.WriteLine($"group <= other = {group <= other}");
        }
        static void CompareStudentsMenu(StudentGroup group)
        {
            Console.Write("Номер першого студента: ");
            string id1 = Console.ReadLine();

            Console.Write("Номер другого студента: ");
            string id2 = Console.ReadLine();

            Student s1 = group.FindByRecordBook(id1);
            Student s2 = group.FindByRecordBook(id2);

            if (s1 == null || s2 == null)
            {
                Console.WriteLine("Один або обидва студенти не знайдені");
                return;
            }

            Console.WriteLine($"Студент 1: {s1.FullName}");
            Console.WriteLine($"Студент 2: {s2.FullName}");

            Console.WriteLine();

            Console.WriteLine($"s1 > s2 = {s1 > s2}");
            Console.WriteLine($"s1 < s2 = {s1 < s2}");
            Console.WriteLine($"s1 == s2 = {s1 == s2}");
            Console.WriteLine($"s1 != s2 = {s1 != s2}");
        }
        static void GradePointDemo()
        {
            GradePoint g1 = new GradePoint(7.5);
            GradePoint g2 = new GradePoint(9.2);

            Console.WriteLine($"g1 = {g1}");
            Console.WriteLine($"g2 = {g2}");

            Console.WriteLine($"g1 + g2 = {g1 + g2}");

            g1++;
            Console.WriteLine($"g1++ = {g1}");

            g2--;
            Console.WriteLine($"g2-- = {g2}");

            Console.WriteLine($"g1 > g2 = {g1 > g2}");
            Console.WriteLine($"g1 < g2 = {g1 < g2}");

            if (g2)
                Console.WriteLine("g2 >= 8 (true)");
            else
                Console.WriteLine("g2 < 8 (false)");

            double val = g1;
            Console.WriteLine($"неявне double: {val}");
        }
        static void FractionDemo()
        {
            Fraction f1 = new Fraction(2, 4);
            Fraction f2 = new Fraction(1, 3);

            Console.WriteLine($"f1 = {f1}");
            Console.WriteLine($"f2 = {f2}");

            Console.WriteLine($"\nf1 + f2 = {f1 + f2}");
            Console.WriteLine($"f1 - f2 = {f1 - f2}");
            Console.WriteLine($"f1 * f2 = {f1 * f2}");
            Console.WriteLine($"f1 / f2 = {f1 / f2}");

            Console.WriteLine($"\nf1 > f2 = {f1 > f2}");
            Console.WriteLine($"f1 < f2 = {f1 < f2}");
            Console.WriteLine($"f1 == f2 = {f1 == f2}");
            Console.WriteLine($"f1 != f2 = {f1 != f2}");

            f1++;
            Console.WriteLine($"\nПісля f1++ = {f1}");

            f2--;
            Console.WriteLine($"Після f2-- = {f2}");

            double value = f1;

            Console.WriteLine($"\nНеявне приведення до double = {value}");
        }
        // КІНЕЦЬ ПРАКТИЧНОЇ 3
        static void AddRegularStudent(StudentGroup group)
        {
            string name = Read("ПІБ: ");
            string record = Read("Залікова: ");
            string email = Read("Email: ");
            DateTime dob = ReadDate("Дата народження: ");
            DateTime enroll = ReadDate("Дата вступу: ");
            string notes = Read("Нотатки: ");

            Student student = new Student(name, dob, email, enroll, record, notes);

            group.AddMember(student);

            Console.WriteLine("Звичайного студента додано.");
        }

        static void AddExcellentStudent(StudentGroup group)
        {
            string name = Read("ПІБ: ");
            string record = Read("Залікова: ");
            string email = Read("Email: ");
            DateTime dob = ReadDate("Дата народження: ");
            DateTime enroll = ReadDate("Дата вступу: ");
            int olympiads = ReadInt("Кількість олімпіад: ");

            ExcellentStudent student = new ExcellentStudent(
                name,
                dob,
                email,
                enroll,
                record,
                olympiads,
                true
            );

            group.AddMember(student);

            Console.WriteLine("Відмінника додано.");
        }

        static void AddForeignStudent(StudentGroup group)
        {
            string name = Read("ПІБ: ");
            string record = Read("Залікова: ");
            string email = Read("Email: ");
            DateTime dob = ReadDate("Дата народження: ");
            DateTime enroll = ReadDate("Дата вступу: ");
            string country = Read("Країна: ");
            string visa = Read("Номер візи: ");

            ForeignStudent student = new ForeignStudent(
                name,
                dob,
                email,
                enroll,
                record,
                country,
                visa
            );

            group.AddMember(student);

            Console.WriteLine("Іноземного студента додано.");
        }

        static void AddWorkingStudent(StudentGroup group)
        {
            string name = Read("ПІБ: ");
            string record = Read("Залікова: ");
            string email = Read("Email: ");
            DateTime dob = ReadDate("Дата народження: ");
            DateTime enroll = ReadDate("Дата вступу: ");
            string workplace = Read("Місце роботи: ");
            int hours = ReadInt("Годин на тиждень: ");

            WorkingStudent student = new WorkingStudent(
                name,
                dob,
                email,
                enroll,
                record,
                workplace,
                hours
            );

            group.AddMember(student);

            Console.WriteLine("Працюючого студента додано.");
        }

        static void ShowUniversityMembers(StudentGroup group)
        {
            var members = group.GetMembersByType<UniversityMember>();

            foreach (var member in members)
            {
                Console.WriteLine(member.GetInfo());
                Console.WriteLine("--------------------");
            }
        }

        static void ShowTotalScholarship(StudentGroup group)
        {
            Console.WriteLine($"Загальна сума стипендій: {group.GetTotalScholarship()} грн");
        }

        static void ShowMembersByType(StudentGroup group)
        {
            Console.WriteLine("1. Відмінники");
            Console.WriteLine("2. Іноземні студенти");
            Console.WriteLine("3. Працюючі студенти");

            string choice = Console.ReadLine();

            if (choice == "1")
                group.GetMembersByType<ExcellentStudent>().ForEach(s => Console.WriteLine(s.GetInfo()));
            else if (choice == "2")
                group.GetMembersByType<ForeignStudent>().ForEach(s => Console.WriteLine(s.GetInfo()));
            else if (choice == "3")
                group.GetMembersByType<WorkingStudent>().ForEach(s => Console.WriteLine(s.GetInfo()));
            else
                Console.WriteLine("Невірний вибір");
        }

        static void TestHierarchy(StudentGroup group)
        {
            UniversityMember member = new ExcellentStudent(
                "Тестовий Студент Приклад",
                new DateTime(2005, 1, 1),
                "test@student.com",
                DateTime.Now,
                "12345678",
                3,
                true
            );

            group.AddMember(member);

            Console.WriteLine(member.GetInfo());
            member.Enroll();

            Console.WriteLine($"Стипендія: {member.CalculateScholarship()} грн");
        }
        static void PolymorphismDemo(StudentGroup group)
        {
            Student student = new Student(
                "Іваненко Іван Іванович",
                new DateTime(2005, 1, 1),
                "ivanenko@test.com",
                DateTime.Now,
                "12345678",
                "Студент має діаграми"
            );

            student.Shapes.Add(new Circle("Коло проєкту", "Червоний", 5));
            student.Shapes.Add(new Rectangle("Прямокутна діаграма", "Синій", 4, 6));
            student.Shapes.Add(new Triangle("Трикутна схема", "Зелений", 3, 4, 5));
            student.Shapes.Add(new Square("Квадратна модель", "Жовтий", 4));

            group.AddStudent(student);

            Console.WriteLine("=== ПОЛІМОРФНА КОЛЕКЦІЯ ФІГУР ===");

            foreach (Shape shape in student.Shapes)
            {
                Console.WriteLine(shape.GetDescription());
                Console.WriteLine("--------------------");
            }

            Console.WriteLine($"Загальна площа всіх фігур: {group.GetTotalAreaOfAllShapes():F2}");

            Console.WriteLine("\nМалювання всіх фігур:");
            group.DrawAllShapes();

            Console.WriteLine("\nЗбільшення всіх фігур у 2 рази:");
            group.ResizeAllShapes(2);

            foreach (Shape shape in student.Shapes)
            {
                Console.WriteLine(shape.GetDescription());
                Console.WriteLine("--------------------");
            }
        }
        static void AddShapeToStudent(StudentGroup group)
        {
            Console.Write("Номер залікової книжки студента: ");
            string record = Console.ReadLine();

            Student student = group.FindByRecordBook(record);

            if (student == null)
            {
                Console.WriteLine("Студента не знайдено.");
                return;
            }

            Console.WriteLine("Оберіть фігуру:");
            Console.WriteLine("1. Circle");
            Console.WriteLine("2. Rectangle");
            Console.WriteLine("3. Triangle");
            Console.WriteLine("4. Square");

            string choice = Console.ReadLine();

            string name = Read("Назва фігури: ");
            string color = Read("Колір: ");

            switch (choice)
            {
                case "1":
                    double radius = ReadDouble("Радіус: ");
                    student.Shapes.Add(new Circle(name, color, radius));
                    break;

                case "2":
                    double width = ReadDouble("Ширина: ");
                    double height = ReadDouble("Висота: ");
                    student.Shapes.Add(new Rectangle(name, color, width, height));
                    break;

                case "3":
                    double a = ReadDouble("Сторона A: ");
                    double b = ReadDouble("Сторона B: ");
                    double c = ReadDouble("Сторона C: ");
                    student.Shapes.Add(new Triangle(name, color, a, b, c));
                    break;

                case "4":
                    double side = ReadDouble("Сторона: ");
                    student.Shapes.Add(new Square(name, color, side));
                    break;

                default:
                    Console.WriteLine("Невірний вибір фігури.");
                    return;
            }

            Console.WriteLine("Фігуру додано студенту.");
        }
        static void ShowAllShapes(StudentGroup group)
        {
            foreach (Student student in group.GetAllStudents())
            {
                Console.WriteLine($"Студент: {student.FullName}");

                if (student.Shapes.Count == 0)
                {
                    Console.WriteLine("Фігур немає.");
                    continue;
                }

                foreach (Shape shape in student.Shapes)
                {
                    Console.WriteLine(shape.GetDescription());
                    Console.WriteLine("--------------------");
                }
            }
        }
        static void ShowPrintableInfo(StudentGroup group)
        {
            foreach (Student student in group.GetAllStudents())
            {
                foreach (Shape shape in student.Shapes)
                {
                    if (shape is IPrintable printable)
                    {
                        Console.WriteLine(printable.GetPrintInfo());
                        Console.WriteLine("--------------------");
                    }
                }
            }
        }
        static double ReadDouble(string msg)
        {
            while (true)
            {
                Console.Write(msg);

                if (double.TryParse(Console.ReadLine(), out double value))
                    return value;

                Console.WriteLine("Невірне число.");
            }
        }
    }
}