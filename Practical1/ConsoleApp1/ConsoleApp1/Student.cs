using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public enum StudentStatus { Active, AcademicLeave, Expelled, Graduated }

    public class Student
    {
        private string FullName;
        private string RecordBookNumber;
        private double AverageGrade;
        private string PersonalEmail;
        public DateTime DateOfBirth { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public string Notes { get; set; } = "Немає нотаток";
        public StudentStatus Status { get; private set; } = StudentStatus.Active;
        public GradeJournal Journal { get; set; } = new();
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
        public double averageGrade
        {
            get => AverageGrade;
            private set
            {
                if (value < 0 || value > 100)
                    throw new ArgumentOutOfRangeException("Середній бал має бути від 0 до 100.");
                AverageGrade = Math.Round(value, 2);
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
        public void SetManualGrade(double grade)
        {
            averageGrade = grade;
        }
        public string ShowDetailedInfo()
        {
            return $"--- Картка студента ---\n" +
                   $"ПІБ: {fullName}\n" +
                   $"Вік: {Age} років\n" +
                   $"Залікова: {recordBookNumber}\n" +
                   $"Середній бал: {averageGrade}\n" +
                   $"Статус: {Status}\n" +
                   $"Email: {personalEmail}\n" +
                   $"Нотатки: {Notes}\n";
        }
        public void UpdateAverageGrade(double newGrade)
        {
            averageGrade = newGrade;
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
    }
}