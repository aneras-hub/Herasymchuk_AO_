using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class ForeignStudent : Student
    {
        public string Country { get; set; }
        public string VisaNumber { get; set; }

        public ForeignStudent(
            string fullName,
            DateTime dateOfBirth,
            string personalEmail,
            DateTime enrollmentDate,
            string recordBookNumber,
            string country,
            string visaNumber,
            string notes = "Немає нотаток"
        ) : base(fullName, dateOfBirth, personalEmail, enrollmentDate, recordBookNumber, notes)
        {
            Country = country;
            VisaNumber = visaNumber;
        }

        public override string GetInfo()
        {
            return base.GetInfo() +
                   $"Країна: {Country}\n" +
                   $"Номер візи: {VisaNumber}\n";
        }
    }
}
