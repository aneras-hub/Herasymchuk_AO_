using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public enum StudentStatus { Active, AcademicLeave, Expelled, Graduated }

    public class Student : ICloneable
    {
        private string FullName;
        private string RecordBookNumber;
        private string PersonalEmail;
        public DateTime DateOfBirth { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public string Notes { get; set; } = "Немає нотаток";
        public StudentStatus Status { get; private set; } = StudentStatus.Active;
        public GradeJournal Journal { get; set; } = new();
        // практична 2
        // масив для зберігання оцінок за лабораторні роботи (макс 10 лаб)
        public byte[] LabGrades { get; set; } = new byte[10];
        // координати для виведення в табличному форматі (ряд та стовпець)
        public int PortRow { get; set; } = -1;
        public int PortCol { get; set; } = -1;
        public required string fullName
        {
            get => FullName;
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Trim().Length < 5)
                    throw new ArgumentException("ПІБ не може бути порожнім і має містити мінімум 5 символів.");
                FullName = value.Trim();
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

        public string ShowDetailedInfo()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("--- Картка студента ---");
            sb.AppendLine($"ПІБ: {fullName}");
            sb.AppendLine($"Вік: {Age} років");
            sb.AppendLine($"Залікова: {recordBookNumber}");
            sb.AppendLine($"Середній бал: {averageGrade}");
            sb.AppendLine($"Оцінки за лабораторні: {string.Join(", ", LabGrades)}");
            sb.AppendLine($"Середній бал лабораторних: {AverageLabGrade}");
            sb.AppendLine($"Статус: {Status}");
            sb.AppendLine($"Email: {personalEmail}");
            sb.AppendLine($"Нотатки: {Notes}");

            if (PortRow != -1 && PortCol != -1)
            {
                sb.AppendLine($"Порт: [{PortRow}, {PortCol}]");
            }
            else
            {
                sb.AppendLine("Порт не призначено");
            }

            return sb.ToString();
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
                PortCol = this.PortCol
            };
        }
    }
}