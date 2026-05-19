using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    // ПРАКТИЧНА 5 5
    public abstract class UniversityMember : Person
    {
        protected UniversityMember(string fullName, DateTime dateOfBirth, string personalEmail, string notes = "Немає нотаток") 
            : base(fullName, dateOfBirth, personalEmail, notes)
        {        }

        // Абстрактний метод
        public abstract decimal CalculateScholarship();

        // Віртуальний метод
        public virtual void Enroll()
        {
            Console.WriteLine(
                $"{FullName} успішно зарахований до університету."
            );
        }
    }
    // К
}
