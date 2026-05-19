using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.студенти
{
    public class GradeJournal
    {
        // словник для зберігання предметів та оцінок
        public Dictionary<string, double> Grades { get; set; } = new();
        // метод для додавання або оновлення оцінки
        public void SetGrade(string subject, double grade)
        {
            if (grade < 0 || grade > 100)
                throw new ArgumentException("Оцінка 0-100");

            Grades[subject] = grade;
        }
        // метод для отримання середнього балу
        public double GetAverage()
        {
            return Grades.Count == 0 ? 0 : Grades.Values.Average();
        }
    }
}