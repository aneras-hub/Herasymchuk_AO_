using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.практична_7
{
    public readonly struct GradeRecord : IEquatable<GradeRecord>
    {
        public string Subject { get; }
        public double Grade { get; }
        public DateTime Date { get; }

        public GradeRecord(string subject, double grade, DateTime date)
        {
            if (grade < 0 || grade > 100)
                throw new ArgumentException("Оцінка має бути від 0 до 100.");

            Subject = subject;
            Grade = grade;
            Date = date;
        }

        public bool Equals(GradeRecord other)
        {
            return Subject == other.Subject &&
                   Grade.Equals(other.Grade) &&
                   Date.Equals(other.Date);
        }

        public override bool Equals(object obj)
        {
            return obj is GradeRecord other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Subject, Grade, Date);
        }

        public static bool operator ==(GradeRecord left, GradeRecord right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(GradeRecord left, GradeRecord right)
        {
            return !(left == right);
        }

        public void Deconstruct(out string subject, out double grade, out DateTime date)
        {
            subject = Subject;
            grade = Grade;
            date = Date;
        }

        public override string ToString()
        {
            return $"{Subject}: {Grade} балів ({Date:d})";
        }
    }
}