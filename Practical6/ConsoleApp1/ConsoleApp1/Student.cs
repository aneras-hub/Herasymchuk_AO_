using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Threading.Tasks;
using ConsoleApp1.студенти;

namespace ConsoleApp1
{
    public enum StudentStatus { Active, AcademicLeave, Expelled, Graduated }
    public class Student : UniversityMember, ICloneable
    {
        public DateTime EnrollmentDate { get; set; }
        public StudentStatus Status { get; private set; } = StudentStatus.Active;
        public GradeJournal Journal { get; set; } = new();
        public List<GradePoint> GradePoints { get; set; } = new();
        public byte[] LabGrades { get; set; } = new byte[10];
        public int PortRow { get; set; } = -1;
        public int PortCol { get; set; } = -1;
        private int courseProgress;
        public string RecordBookNumber { get; set; }
        public Student(string fullName, DateTime dateOfBirth, string personalEmail, DateTime enrollmentDate, string recordBookNumber, string notes = "Немає нотаток") : base(fullName, dateOfBirth, personalEmail, notes)
        {
            EnrollmentDate = enrollmentDate;
            RecordBookNumber = recordBookNumber;
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
        
        public double averageGrade => Math.Round(Journal.GetAverage(), 2);
        public double AverageLabGrade => Math.Round(GetAverageLabGrade(), 2);
        public string GetFormattedInfo(bool detailed = false)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("--- Картка студента ---");
            sb.AppendLine($"ПІБ: {FullName}");
            sb.AppendLine($"Вік: {Age} років");
            sb.AppendLine($"Залікова книжка: {RecordBookNumber}");
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
                FullName.Contains(keyword,
                    StringComparison.OrdinalIgnoreCase)

                || PersonalEmail.Contains(keyword,
                    StringComparison.OrdinalIgnoreCase)

                || Notes.Contains(keyword,
                    StringComparison.OrdinalIgnoreCase);
        }
        public string ShowDetailedInfo()
        {
            return GetFormattedInfo(true);
        }
        public override string GetInfo()
        {
            return base.GetInfo() +
                   $"Залікова книжка: {RecordBookNumber}\n" +
                   $"Дата вступу: {EnrollmentDate:d}\n" +
                   $"Середній бал: {averageGrade}\n" +
                   $"Статус: {Status}\n";
        }
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
            LabGrades.Any(g => g > 0) ? LabGrades.Where(g => g > 0).Average(g => g) : 0;
        public void SortLabGrades()
        {
            Array.Sort(LabGrades);
        }
        public object Clone()
        {
            Student copy = new Student(
                FullName,
                DateOfBirth,
                PersonalEmail,
                EnrollmentDate,
                RecordBookNumber,
                Notes
            );

            copy.Journal = Journal;
            copy.LabGrades = (byte[])LabGrades.Clone();
            copy.PortRow = PortRow;
            copy.PortCol = PortCol;
            copy.CourseProgress = CourseProgress;

            return copy;
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
            Student merged = new Student(
                $"{a.FullName} & {b.FullName}",
                a.DateOfBirth,
                "team@student.com",
                a.EnrollmentDate,
                DateTime.Now.Ticks.ToString().Substring(0, 8),
                $"Командний профіль: {a.FullName} + {b.FullName}"
            );

            merged.CourseProgress =
                (a.CourseProgress + b.CourseProgress) / 2;

            merged.LabGrades = a.LabGrades
                .Concat(b.LabGrades)
                .Take(10)
                .ToArray();

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
        public double AverageGradePoint => GradePoints.Count == 0 ? 0 : GradePoints.Average(g => g.Value);
    }
}