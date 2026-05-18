using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class WorkingStudent : Student
    {
        public string Workplace { get; set; }
        public int WorkingHoursPerWeek { get; set; }

        public WorkingStudent(
            string fullName,
            DateTime dateOfBirth,
            string personalEmail,
            DateTime enrollmentDate,
            string recordBookNumber,
            string workplace,
            int workingHoursPerWeek,
            string notes = "Немає нотаток"
        ) : base(fullName, dateOfBirth, personalEmail, enrollmentDate, recordBookNumber, notes)
        {
            Workplace = workplace;
            WorkingHoursPerWeek = workingHoursPerWeek;
        }

        public override string GetInfo()
        {
            return base.GetInfo() +
                   $"Місце роботи: {Workplace}\n" +
                   $"Годин роботи на тиждень: {WorkingHoursPerWeek}\n";
        }
    }
}
