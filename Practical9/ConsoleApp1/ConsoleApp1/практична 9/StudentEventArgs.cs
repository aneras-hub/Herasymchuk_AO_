using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.практична_9
{
    public class StudentEventArgs : EventArgs
    {
        public Student Student { get; }

        public string Message { get; }

        public StudentEventArgs(Student student, string message)
        {
            Student = student;
            Message = message;
        }
    }
}