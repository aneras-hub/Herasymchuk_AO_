using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.практична_9
{
    public class NotificationSystem
    {
        // Події
        public event EventHandler<StudentEventArgs> StudentAdded;

        public event EventHandler<StudentEventArgs> StudentRemoved;

        public event EventHandler<GroupReportEventArgs> ReportGenerated;

        // Виклик події додавання студента
        public void OnStudentAdded(Student student)
        {
            StudentAdded?.Invoke(
                this,
                new StudentEventArgs(
                    student,
                    $"Студента {student.FullName} додано"
                )
            );
        }

        // Виклик події видалення студента
        public void OnStudentRemoved(Student student)
        {
            StudentRemoved?.Invoke(
                this,
                new StudentEventArgs(
                    student,
                    $"Студента {student.FullName} видалено"
                )
            );
        }

        // Виклик події генерації звіту
        public void OnReportGenerated(
            StudentGroup group,
            string report)
        {
            ReportGenerated?.Invoke(
                this,
                new GroupReportEventArgs(
                    group,
                    report
                )
            );
        }
    }
}
