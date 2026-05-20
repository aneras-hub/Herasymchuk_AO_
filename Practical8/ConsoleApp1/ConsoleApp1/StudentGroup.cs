using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;
using ConsoleApp1.порти;
using ConsoleApp1.студенти;
using ConsoleApp1.практична_6.інтерфейс;
using ConsoleApp1.практична_6.абстракція;
using ConsoleApp1.практична_7;
using ConsoleApp1.практична_8;

namespace ConsoleApp1
{
    public class StudentGroup
    {
        private List<UniversityMember> _members = new();
        private List<Student> _students => _members.OfType<Student>().ToList();
        public string GroupName { get; set; }
        public string Specialty { get; set; }
        public int Course { get; set; }
        public int GroupSize => _students.Count;
        public double AverageGroupGrade => _students.Count == 0 ? 0 : Math.Round(_students.Average(s => s.averageGrade), 2);
        private PortMatrix portMatrix = new();
        private PortLogger logger = new();
        private GradeRecord[] gradeHistory = Array.Empty<GradeRecord>();
        private Point[] labPlaces = Array.Empty<Point>();
        private StudentRecord[] studentRecords = Array.Empty<StudentRecord>();
        // ghfrnbxyf 5 5
        public void AddMember(UniversityMember member)
        {
            _members.Add(member);
        }

        public List<T> GetMembersByType<T>() where T : UniversityMember
        {
            return _members.OfType<T>().ToList();
        }

        public decimal GetTotalScholarship()
        {
            return _members.Sum(m => m.CalculateScholarship());
        }
        // ghfrnbxyf 5 5 
        // ghfrnxyf 5 5
        public void AddStudent(Student s)
        {
            if (_members.OfType<Student>().Any(x => x.RecordBookNumber == s.RecordBookNumber))
                throw new Exception("Студент з таким номером вже існує!");

            _members.Add(s);

        }
        // r
        public void RemoveStudent(string recordBookNumber)
        {
            _members.RemoveAll(s => s is Student st && st.RecordBookNumber == recordBookNumber);
        }
        public List<Student> FindByName(string query)
        {
            return _students
                .Where(s => s.FullName.Contains(
                    query,
                    StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
        public string SearchByNameFragment(string fragment)
        {
            StringBuilder sb = new StringBuilder();
            var foundStudents = _students
                .Where(s => s.FullName.Contains(
                    fragment,
                    StringComparison.OrdinalIgnoreCase))
                .ToList();
            if (foundStudents.Count == 0)
            {
                return "Студентів не знайдено.";
            }
            sb.AppendLine("=== РЕЗУЛЬТАТ ПОШУКУ ===");
            foreach (var student in foundStudents)
            {
                sb.AppendLine(student.ShowDetailedInfo());
            }
            return sb.ToString();
        }
        public string ExportToCsv()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("FullName;RecordBookNumber;Email;AverageGrade;Status");
            foreach (var student in _students)
            {
                sb.AppendLine(
                    $"{student.FullName};" +
                    $"{student.RecordBookNumber};" +
                    $"{student.PersonalEmail};" +
                    $"{student.averageGrade};" +
                    $"{student.Status}"
                );
            }
            return sb.ToString();
        }
        public void ImportStudentsFromText(string rawText)
        {
            if (string.IsNullOrWhiteSpace(rawText))
            {
                throw new ArgumentException(
                    "Текст для імпорту порожній."
                );
            }
            string[] lines = rawText.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            foreach (string line in lines)
            {
                string cleanedLine = line.Trim();
                string[] parts = cleanedLine.Split(';');
                if (parts.Length < 3)
                {
                    continue;
                }
                try
                {
                    Student student = new Student(
                        parts[0].Trim(),
                        DateTime.Now.AddYears(-18),
                        parts[2].Trim(),
                        DateTime.Now,
                        parts[1].Trim()
                    );
                    AddStudent(student);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Помилка імпорту рядка: {ex.Message}");
                }
            }
        }
        public Student FindByRecordBook(string recordBookNumber)
        {
            return _students.FirstOrDefault(s => s.RecordBookNumber == recordBookNumber);
        }
        public List<Student> GetExcellentStudents() => _students.Where(s => s.IsExcellent()).ToList();
        public List<Student> GetStudentsByStatus(StudentStatus status) => _students.Where(s => s.Status == status).ToList();
        public void SaveToFile(string path)
        {
            var data = new GroupDto
            {
                GroupName = GroupName,
                Specialty = Specialty,
                Course = Course,
                Students = _students
            };
            File.WriteAllText(path, JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true }));
            Console.WriteLine("Групу збережено у файл.");
        }
        public void LoadFromFile(string path)
        {
            if (!File.Exists(path)) return;
            var data = JsonSerializer.Deserialize<GroupDto>(File.ReadAllText(path));
            GroupName = data.GroupName;
            Specialty = data.Specialty;
            Course = data.Course;
            _members = data.Students?
                .Cast<UniversityMember>()
                .ToList()
                ?? new();
            Console.WriteLine("Групу завантажено з файлу.");
        }
        private class GroupDto
        {
            public string GroupName { get; set; }
            public string Specialty { get; set; }
            public int Course { get; set; }
            public List<Student> Students { get; set; }
        }
        public void AssignStudentToPort(Student s, int row, int col)
        {
            Port port = portMatrix.GetPort(row, col);
            s.PortRow = row;
            s.PortCol = col;
            logger.LogOperation(
                "ASSIGN",
                port.PortNumber,
                $"Студент {s.FullName} прив'язаний до порту"
            );
        }
        public List<Student> GetStudentsByPortStatus(bool isOpen)
        {
            return _students
                .Where(s =>
                    s.PortRow != -1 &&
                    s.PortCol != -1 &&
                    portMatrix.GetPort(s.PortRow, s.PortCol).IsOpen == isOpen)
                .ToList();
        }
        public void SimulateLabWork(Student student, int labNumber, byte grade, byte[] data)
        {
            if (student.PortRow == -1 || student.PortCol == -1)
            {
                throw new Exception("Студент не прив'язаний до порту!");
            }
            Port port = portMatrix.GetPort(student.PortRow, student.PortCol);
            if (!port.IsOpen)
            {
                port.Open();

                logger.LogOperation(
                    "OPEN",
                    port.PortNumber,
                    "Порт відкрито автоматично"
                );
            }
            student.AddLabGrade(labNumber, grade);
            port.WriteData(data);

            logger.LogOperation(
                "LAB_WORK",
                port.PortNumber,
                $"Студент {student.FullName} виконав лабораторну №{labNumber}, оцінка: {grade}"
            );
        }
        public string GetPortLogs()
        {
            return logger.GetFullLog();
        }
        public PortMatrix GetPortMatrix()
        {
            return portMatrix;
        }
        public int GetOpenedPortsCount()
        {
            int count = 0;

            for (int row = 0; row < 16; row++)
            {
                for (int col = 0; col < 16; col++)
                {
                    if (portMatrix.GetPort(row, col).IsOpen)
                        count++;
                }
            }
            return count;
        }
        public Student FindById(int id)
        {
            return _students.FirstOrDefault(s => s.RecordBookNumber == id.ToString());
        }
        public string GenerateBigReport()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("=== ВЕЛИКИЙ ЗВІТ ===");
            for (int i = 0; i < 100; i++)
            {
                foreach (var student in _students)
                {
                    sb.AppendLine(
                        $"Запис #{i + 1} | " +
                        $"Студент: {student.FullName} | " +
                        $"Середній бал: {student.averageGrade} | " +
                        $"Статус: {student.Status}"
                    );
                }
            }
            return sb.ToString();
        }
        public List<Student> GetAllStudents()
        {
            return _students;
        }
        // ПРАКТИЧНА 4 2

        public static StudentGroup operator +(StudentGroup a, StudentGroup b)
        {
            StudentGroup merged = new StudentGroup
            {
                GroupName = $"{a.GroupName}-{b.GroupName}",
                Specialty = a.Specialty,
                Course = Math.Max(a.Course, b.Course)
            };

            foreach (var student in a._students)
            {
                merged.AddStudent((Student)student.Clone());
            }

            foreach (var student in b._students)
            {
                if (!merged._students.Any(s => s.RecordBookNumber == student.RecordBookNumber))
                {
                    merged.AddStudent((Student)student.Clone());
                }
            }

            return merged;
        }

        // Індексатор
        public Student this[string recordBookNumber]
        {
            get
            {
                return _students.FirstOrDefault(s => s.RecordBookNumber == recordBookNumber);
            }
        }

        // К
        //ПРАКТИЧНА 4 3
        public Student BestStudent()
        {
            if (_students.Count == 0)
                return null;

            Student best = _students[0];

            foreach (var student in _students)
            {
                if (student > best)
                {
                    best = student;
                }
            }

            return best;
        }
        public StudentGroup MergeGroups(StudentGroup other)
        {
            return this + other;
        }
        // Порівняння груп за середнім балом

        public static bool operator >(StudentGroup a, StudentGroup b)
        {
            return a.AverageGroupGrade > b.AverageGroupGrade;
        }

        public static bool operator <(StudentGroup a, StudentGroup b)
        {
            return a.AverageGroupGrade < b.AverageGroupGrade;
        }

        public static bool operator >=(StudentGroup a, StudentGroup b)
        {
            return a.AverageGroupGrade >= b.AverageGroupGrade;
        }

        public static bool operator <=(StudentGroup a, StudentGroup b)
        {
            return a.AverageGroupGrade <= b.AverageGroupGrade;
        }
        //К
        public double GetTotalAreaOfAllShapes()
        {
            return _students
                .SelectMany(student => student.Shapes)
                .Sum(shape => shape.CalculateArea());
        }

        public void DrawAllShapes()
        {
            foreach (var shape in _students.SelectMany(student => student.Shapes))
            {
                if (shape is IDrawable drawable)
                {
                    drawable.Draw();
                }
            }
        }

        public void ResizeAllShapes(double factor)
        {
            foreach (var shape in _students.SelectMany(student => student.Shapes))
            {
                if (shape is IResizable resizable)
                {
                    resizable.Resize(factor);
                }
            }
        }
        public void OptimizeStorage()
        {
            studentRecords = GetAllRecords();

            gradeHistory = _students
                .SelectMany(student => student.Journal.Grades.Select(grade =>
                    new GradeRecord(
                        grade.Key,
                        grade.Value,
                        DateTime.Now
                    )))
                .ToArray();

            labPlaces = _students
                .Where(student => student.PortRow >= 0 && student.PortCol >= 0)
                .Select(student => new Point(student.PortRow, student.PortCol))
                .ToArray();
        }
        public StudentRecord[] GetAllRecords()
        {
            return _students
                .Select(student => student.GetRecord())
                .ToArray();
        }
        public GradeRecord[] GetGradeHistory()
        {
            return gradeHistory;
        }
        public Point[] GetLabPlaces()
        {
            return labPlaces;
        }
        private class StudentGroupDto
        {
            public string GroupName { get; set; }
            public string Specialty { get; set; }
            public int Course { get; set; }
            public List<Student> Students { get; set; }
        }
        public void Save(string filePath, StorageFormat format)
        {
            FileManager manager = new FileManager();

            StudentGroupDto dto = new StudentGroupDto
            {
                GroupName = GroupName,
                Specialty = Specialty,
                Course = Course,
                Students = _students
            };

            if (format == StorageFormat.Json)
            {
                manager.SaveToJson(dto, filePath);
            }
            else if (format == StorageFormat.Text)
            {
                manager.SaveToText(GenerateTextReport(), filePath);
            }
        }
        public static StudentGroup Load(string filePath, StorageFormat format)
        {
            FileManager manager = new FileManager();

            if (format != StorageFormat.Json)
                throw new InvalidOperationException("Завантаження підтримується тільки з JSON.");

            StudentGroupDto dto = manager.LoadFromJson<StudentGroupDto>(filePath);

            StudentGroup group = new StudentGroup
            {
                GroupName = dto.GroupName,
                Specialty = dto.Specialty,
                Course = dto.Course
            };

            if (dto.Students != null)
            {
                foreach (Student student in dto.Students)
                {
                    group.AddStudent(student);
                }
            }

            return group;
        }
        public string GenerateTextReport()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("=== ТЕКСТОВИЙ ЗВІТ ГРУПИ ===");
            sb.AppendLine($"Група: {GroupName}");
            sb.AppendLine($"Спеціальність: {Specialty}");
            sb.AppendLine($"Курс: {Course}");
            sb.AppendLine($"Кількість студентів: {GroupSize}");
            sb.AppendLine();

            foreach (Student student in _students)
            {
                sb.AppendLine(student.ShowDetailedInfo());
                sb.AppendLine("----------------------------");
            }

            return sb.ToString();
        }
        public void ExportGradesToCsv(string filePath)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("StudentName;RecordBookNumber;Subject;Grade");

            foreach (Student student in _students)
            {
                foreach (var grade in student.Journal.Grades)
                {
                    sb.AppendLine(
                        $"{student.FullName};" +
                        $"{student.RecordBookNumber};" +
                        $"{grade.Key};" +
                        $"{grade.Value}"
                    );
                }
            }

            File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
        }
        public void CreateBackup(string sourcePath)
        {
            if (!File.Exists(sourcePath))
                throw new FileNotFoundException("Файл для резервної копії не знайдено.", sourcePath);

            Directory.CreateDirectory("Backups");

            string fileName = Path.GetFileNameWithoutExtension(sourcePath);
            string extension = Path.GetExtension(sourcePath);

            string backupPath = Path.Combine(
                "Backups",
                $"{fileName}_backup_{DateTime.Now:yyyyMMdd_HHmmss}{extension}"
            );

            File.Copy(sourcePath, backupPath, true);
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
    }
}