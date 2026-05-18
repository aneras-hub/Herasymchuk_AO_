using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public sealed class GraduateStudent : Student
    {
        public string ThesisTopic { get; set; }
        public string ScientificAdvisor { get; set; }

        public GraduateStudent(
            string fullName,
            DateTime dateOfBirth,
            string personalEmail,
            DateTime enrollmentDate,
            string recordBookNumber,
            string thesisTopic,
            string scientificAdvisor,
            string notes = "Немає нотаток"
        ) : base(fullName, dateOfBirth, personalEmail, enrollmentDate, recordBookNumber, notes)
        {
            ThesisTopic = thesisTopic;
            ScientificAdvisor = scientificAdvisor;
        }

        public override string GetInfo()
        {
            return base.GetInfo() +
                   $"Тема дипломної роботи: {ThesisTopic}\n" +
                   $"Науковий керівник: {ScientificAdvisor}\n";
        }
    }
}
