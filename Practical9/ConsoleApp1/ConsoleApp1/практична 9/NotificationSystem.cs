using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.практична_9
{
    public class NotificationSystem
    {
        public event EventHandler<StudentEventArgs> StudentAdded;
        public event EventHandler<StudentEventArgs> StudentRemoved;
        public event EventHandler<GroupReportEventArgs> ReportGenerated;

        public List<string> EventHistory { get; } = new List<string>();

        public void OnStudentAdded(Student student)
        {
            string message = $"Студента {student.FullName} додано.";
            EventHistory.Add(message);

            StudentAdded?.Invoke(
                this,
                new StudentEventArgs(student, message)
            );
        }

        public void OnStudentRemoved(Student student)
        {
            string message = $"Студента {student.FullName} видалено.";
            EventHistory.Add(message);

            StudentRemoved?.Invoke(
                this,
                new StudentEventArgs(student, message)
            );
        }

        public void OnReportGenerated(StudentGroup group, string report)
        {
            string message = $"Звіт для групи {group.GroupName} згенеровано.";
            EventHistory.Add(message);

            ReportGenerated?.Invoke(
                this,
                new GroupReportEventArgs(group, report)
            );
        }

        public void ShowHistory()
        {
            Console.WriteLine("=== ІСТОРІЯ ПОДІЙ ===");

            if (EventHistory.Count == 0)
            {
                Console.WriteLine("Подій ще немає.");
                return;
            }

            foreach (string item in EventHistory)
            {
                Console.WriteLine(item);
            }
        }
    }
}
