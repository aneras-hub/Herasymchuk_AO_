using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public enum StudentStatus { Active, AcademicLeave, Expelled, Graduated }
    // ПРАКТИЧНА 5 1
    // ПРАКТИЧНА 5 5
    public class Student : UniversityMember, ICloneable
    // К
    //К
    {
        public DateTime EnrollmentDate { get; set; }
        public StudentStatus Status { get; private set; } = StudentStatus.Active;
        public GradeJournal Journal { get; set; } = new();
        public List<GradePoint> GradePoints { get; set; } = new();
        public byte[] LabGrades { get; set; } = new byte[10];
        public int PortRow { get; set; } = -1;
        public int PortCol { get; set; } = -1;
        private int courseProgress;
        // ПРАКТИЧНА 5 1
        public Student(string fullName, DateTime dateOfBirth, string personalEmail, DateTime enrollmentDate, string recordBookNumber, string notes = "Немає нотаток") : base(fullName, dateOfBirth, personalEmail, notes)
        {
            EnrollmentDate = enrollmentDate;
            this.recordBookNumber = recordBookNumber;
        }
        //К
        public required string fullName
        {
            get => FullName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("ПІБ не може бути порожнім.");

                string normalized = value.Trim();

                string[] parts = normalized.Split(
                    ' ',
                    StringSplitOptions.RemoveEmptyEntries
                );

                if (parts.Length < 3)
                    throw new ArgumentException(
                        "ПІБ має містити мінімум 3 слова: прізвище, ім'я та по батькові."
                    );

                FullName = normalized;
            }
        }
        public string recordBookNumber
        {
            get => RecordBookNumber;
            set
            {
                if (value?.Length != 8 || !long.TryParse(value, out _))
                    throw new ArgumentException("Номер залікової книжки має бути унікальним 8-значним числом.");
                RecordBookNumber = value;
            }
        }
        public int Age
        {
            get
            {
                var today = DateTime.Today;
                int age = today.Year - DateOfBirth.Year;
                if (DateOfBirth.Date > today.AddYears(-age)) age--;
                return age;
            }
        }
        public string personalEmail
        {
            get => PersonalEmail;
            set
            {
                try
                {
                    var addr = new MailAddress(value);
                    PersonalEmail = addr.Address;
                }
                catch
                {
                    throw new ArgumentException("Некоректний формат email.");
                }
            }
        }
        public double averageGrade => Math.Round(Journal.GetAverage(), 2);
        public double AverageLabGrade => Math.Round(GetAverageLabGrade(), 2);
        public string GetFormattedInfo(bool detailed = false)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("--- Картка студента ---");
            sb.AppendLine($"ПІБ: {fullName}");
            sb.AppendLine($"Вік: {Age} років");
            sb.AppendLine($"Залікова книжка: {recordBookNumber}");
            sb.AppendLine($"Середній бал: {averageGrade}");
            sb.AppendLine($"Статус: {Status}");
            sb.AppendLine($"Прогрес навчання: {CourseProgress}%");
            if (detailed)
            {
                sb.AppendLine($"Дата народження: {DateOfBirth:d}");
                sb.AppendLine($"Дата вступу: {EnrollmentDate:d}");
                sb.AppendLine($"Вік: {Age}");
                sb.AppendLine($"Нотатки: {Notes}");
                sb.AppendLine($"Оцінки за лабораторні: {string.Join(", ", LabGrades)}");
                sb.AppendLine($"Середній бал лабораторних: {AverageLabGrade}");

                if (PortRow != -1 && PortCol != -1)
                {
                    sb.AppendLine($"Порт: [{PortRow}, {PortCol}]");
                }
                else
                {
                    sb.AppendLine("Порт не призначено");
                }
            }
            return sb.ToString();
        }
        public bool ContainsKeyword(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return false;

            return
                fullName.Contains(keyword,
                    StringComparison.OrdinalIgnoreCase)

                || personalEmail.Contains(keyword,
                    StringComparison.OrdinalIgnoreCase)

                || Notes.Contains(keyword,
                    StringComparison.OrdinalIgnoreCase);
        }
        public string ShowDetailedInfo()
        {
            return GetFormattedInfo(true);
        }
        //ПРАКТИЧНА 5 1
        public override string GetInfo()
        {
            return base.GetInfo() +
                   $"Залікова книжка: {recordBookNumber}\n" +
                   $"Дата вступу: {EnrollmentDate:d}\n" +
                   $"Середній бал: {averageGrade}\n" +
                   $"Статус: {Status}\n";
        }
        //К
        // ПРАКТИЧНА 5 5
        public override decimal CalculateScholarship()
        {
            if (averageGrade >= 90)
                return 3000;

            if (averageGrade >= 75)
                return 2000;

            if (averageGrade >= 60)
                return 1000;

            return 0;
        }
        //К
        public bool IsExcellent() => averageGrade >= 90;
        public bool IsFailing() => averageGrade < 60;
        public int CalculateAge()
        {
            var today = DateTime.Today;
            int age = today.Year - DateOfBirth.Year;
            if (DateOfBirth.Date > today.AddYears(-age)) age--;
            return age;
        }
        public int GetYearsToGraduation()
        {
            int graduationYear = EnrollmentDate.Year + 4;
            int yearsLeft = graduationYear - DateTime.Today.Year;
            return Math.Max(0, yearsLeft);
        }
        public void AddLabGrade(int labNumber, byte grade)
        {
            if (labNumber < 0 || labNumber >= LabGrades.Length)
                throw new IndexOutOfRangeException($"Номер лабораторної має бути від 0 до {LabGrades.Length - 1}.");

            if (grade > 100)
                throw new ArgumentException("Оцінка не може бути більшою за 100.");

            LabGrades[labNumber] = grade;
        }
        public double GetAverageLabGrade() =>
            LabGrades.Any(g => g > 0) ? LabGrades.Where(g => g > 0).Average(g => (int)g) : 0;
        public void SortLabGrades()
        {
            Array.Sort(LabGrades);
        }
        public object Clone()
        {
            return new Student
            {
                fullName = this.fullName,
                recordBookNumber = this.recordBookNumber,
                personalEmail = this.personalEmail,
                DateOfBirth = this.DateOfBirth,
                EnrollmentDate = this.EnrollmentDate,
                Notes = this.Notes,
                Journal = this.Journal,
                LabGrades = (byte[])this.LabGrades.Clone(),
                PortRow = this.PortRow,
                PortCol = this.PortCol,
                CourseProgress = this.CourseProgress
            };
        }
        public int CourseProgress
        {
            get => courseProgress;
            set
            {
                if (value < 0 || value > 100)
                    throw new ArgumentException("Прогрес навчання має бути від 0 до 100.");
                courseProgress = value;
            }
        }
        private static int CompareStudents(Student a, Student b)
        {
            if (a is null && b is null)
                return 0;

            if (a is null)
                return -1;

            if (b is null)
                return 1;

            if (a.averageGrade > b.averageGrade)
                return 1;

            if (a.averageGrade < b.averageGrade)
                return -1;

            if (a.CourseProgress > b.CourseProgress)
                return 1;

            if (a.CourseProgress < b.CourseProgress)
                return -1;

            return 0;
        }
        public static bool operator >(Student a, Student b)
        {
            return CompareStudents(a, b) > 0;
        }
        public static bool operator <(Student a, Student b)
        {
            return CompareStudents(a, b) < 0;
        }
        public static bool operator >=(Student a, Student b)
        {
            return CompareStudents(a, b) >= 0;
        }
        public static bool operator <=(Student a, Student b)
        {
            return CompareStudents(a, b) <= 0;
        }
        public static bool operator ==(Student a, Student b)
        {
            if (ReferenceEquals(a, b))
                return true;

            if (a is null || b is null)
                return false;

            return CompareStudents(a, b) == 0;
        }
        public static bool operator !=(Student a, Student b)
        {
            return !(a == b);
        }
        public override bool Equals(object obj)
        {
            if (obj is not Student other)
                return false;

            return this == other;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(
                averageGrade,
                CourseProgress
            );
        }
        public static Student operator +(Student a, Student b)
        {
            Student merged = new Student
            {
                fullName = $"{a.fullName} & {b.fullName}",
                recordBookNumber = DateTime.Now.Ticks
                .ToString()
                .Substring(0, 8),
                personalEmail = "team@student.com",
                DateOfBirth = a.DateOfBirth,
                EnrollmentDate = a.EnrollmentDate,
                Notes = $"Командний профіль: {a.fullName} + {b.fullName}",

                CourseProgress = (a.CourseProgress + b.CourseProgress) / 2,

                LabGrades = a.LabGrades
                .Concat(b.LabGrades)
                .Take(10)
                .ToArray()
            };

            foreach (var grade in a.Journal.Grades)
            {
                merged.Journal.Grades[grade.Key] = grade.Value;
            }

            foreach (var grade in b.Journal.Grades)
            {
                if (!merged.Journal.Grades.ContainsKey(grade.Key))
                {
                    merged.Journal.Grades[grade.Key] = grade.Value;
                }
            }
            return merged;
        }
        // К
        // ПРАКТИЧНА 4 3
        public double AverageGradePoint => GradePoints.Count == 0 ? 0 : GradePoints.Average(g => g.Value);
    }
    //К
}