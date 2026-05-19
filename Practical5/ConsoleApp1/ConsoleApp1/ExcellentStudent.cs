using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    // ПРАКТИЧНА 5 4
    public class ExcellentStudent : Student
    {
        public int OlympiadCount { get; set; }
        public bool HasPersonalScholarship { get; set; }

        public ExcellentStudent(
            string fullName,
            DateTime dateOfBirth,
            string personalEmail,
            DateTime enrollmentDate,
            string recordBookNumber,
            int olympiadCount,
            bool hasPersonalScholarship,
            string notes = "Немає нотаток"
        ) : base(fullName, dateOfBirth, personalEmail, enrollmentDate, recordBookNumber, notes)
        {
            OlympiadCount = olympiadCount;
            HasPersonalScholarship = hasPersonalScholarship;
        }

        public override string GetInfo()
        {
            return base.GetInfo() +
                   $"Кількість олімпіад: {OlympiadCount}\n" +
                   $"Іменна стипендія: {(HasPersonalScholarship ? "Так" : "Ні")}\n";
        }
    }
    // К
}
