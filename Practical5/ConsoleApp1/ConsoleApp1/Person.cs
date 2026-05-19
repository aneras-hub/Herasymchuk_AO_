using System;
using System.Net.Mail;
using System.Text;

namespace ConsoleApp1
{
    // ПРАКТИЧНА 5 1
    public class Person
    {
        private string fullName;
        private string personalEmail;

        public string FullName
        {
            get => fullName;
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

                fullName = normalized;
            }
        }

        public DateTime DateOfBirth { get; set; }

        public string PersonalEmail
        {
            get => personalEmail;
            set
            {
                try
                {
                    var addr = new MailAddress(value);
                    personalEmail = addr.Address;
                }
                catch
                {
                    throw new ArgumentException("Некоректний формат email.");
                }
            }
        }

        public string Notes { get; set; } = "Немає нотаток";

        public int Age
        {
            get
            {
                var today = DateTime.Today;
                int age = today.Year - DateOfBirth.Year;

                if (DateOfBirth.Date > today.AddYears(-age))
                    age--;

                return age;
            }
        }

        public Person(string fullName, DateTime dateOfBirth, string personalEmail, string notes = "Немає нотаток")
        {
            FullName = fullName;
            DateOfBirth = dateOfBirth;
            PersonalEmail = personalEmail;
            Notes = notes;
        }

        public virtual string GetInfo()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("--- Інформація про особу ---");
            sb.AppendLine($"ПІБ: {FullName}");
            sb.AppendLine($"Дата народження: {DateOfBirth:d}");
            sb.AppendLine($"Вік: {Age}");
            sb.AppendLine($"Email: {PersonalEmail}");
            sb.AppendLine($"Нотатки: {Notes}");

            return sb.ToString();
        }

    }
}