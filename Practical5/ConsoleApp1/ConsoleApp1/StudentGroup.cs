using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApp1
{
    public class StudentGroup
    {
        private List<Student> _students = new();
        public string GroupName { get; set; }
        public string Specialty { get; set; }
        public int Course { get; set; }
        public int GroupSize => _students.Count;
        public double AverageGroupGrade => _students.Count == 0 ? 0 : Math.Round(_students.Average(s => s.averageGrade), 2);
        private PortMatrix portMatrix = new();
        private PortLogger logger = new();
        public void AddStudent(Student s)
        {
            if (_students.Any(x => x.recordBookNumber == s.recordBookNumber))
                throw new Exception("Студент з таким номером вже існує!");
            _students.Add(s);
        }
        public void RemoveStudent(string recordBookNumber)
        {
            _students.RemoveAll(s => s.recordBookNumber == recordBookNumber);
        }
        public List<Student> FindByName(string query)
        {
            return _students
                .Where(s => s.fullName.Contains(
                    query,
                    StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
        public string SearchByNameFragment(string fragment)
        {
            StringBuilder sb = new StringBuilder();
            var foundStudents = _students
                .Where(s => s.fullName.Contains(
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
                    $"{student.fullName};" +
                    $"{student.recordBookNumber};" +
                    $"{student.personalEmail};" +
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
                    Student student = new Student
                    {
                        fullName = parts[0].Trim(),
                        recordBookNumber = parts[1].Trim(),
                        personalEmail = parts[2].Trim(),
                        DateOfBirth = DateTime.Now.AddYears(-18),
                        EnrollmentDate = DateTime.Now
                    };
                    AddStudent(student);
                }
                catch
                {
                    // пропускаємо некоректний запис
                }
            }
        }
        public Student FindByRecordBook(string recordBookNumber)
        {
            return _students.FirstOrDefault(s => s.recordBookNumber == recordBookNumber);
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
            _students = data.Students ?? new();
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
                $"Студент {s.fullName} прив'язаний до порту"
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
                $"Студент {student.fullName} виконав лабораторну №{labNumber}, оцінка: {grade}"
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
            return _students.FirstOrDefault(s => s.recordBookNumber == id.ToString());
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
                        $"Студент: {student.fullName} | " +
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
                if (!merged._students.Any(s =>
                    s.recordBookNumber == student.recordBookNumber))
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
                return _students.FirstOrDefault(s => s.recordBookNumber == recordBookNumber);
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
    }
}