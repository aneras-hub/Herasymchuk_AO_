using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.практична_9
{
    public class DelegateExamples
    {
        // Predicate<Student>
        public List<Student> FilterStudents(
            List<Student> students,
            Predicate<Student> predicate)
        {
            return students.FindAll(predicate);
        }

        // Func<Student, double>
        public double CalculateStudentValue(
            Student student,
            Func<Student, double> calculator)
        {
            return calculator(student);
        }

        // Action<string>
        public void LogMessage(
            string message,
            Action<string> logger)
        {
            logger(message);
        }

        // Func<StudentGroup, string>
        public string GenerateReport(
            StudentGroup group,
            Func<StudentGroup, string> reportGenerator)
        {
            return reportGenerator(group);
        }
    }
}