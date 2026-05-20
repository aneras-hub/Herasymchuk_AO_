using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.практична_7
{
    public readonly struct StudentRecord : IEquatable<StudentRecord>
    {
        public string FullName { get; }
        public string RecordBookNumber { get; }
        public double AverageGrade { get; }
        public int CourseProgress { get; }

        public StudentRecord(
            string fullName,
            string recordBookNumber,
            double averageGrade,
            int courseProgress)
        {
            FullName = fullName;
            RecordBookNumber = recordBookNumber;
            AverageGrade = averageGrade;
            CourseProgress = courseProgress;
        }

        public bool Equals(StudentRecord other)
        {
            return FullName == other.FullName &&
                   RecordBookNumber == other.RecordBookNumber &&
                   AverageGrade.Equals(other.AverageGrade) &&
                   CourseProgress == other.CourseProgress;
        }

        public override bool Equals(object obj)
        {
            return obj is StudentRecord other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(
                FullName,
                RecordBookNumber,
                AverageGrade,
                CourseProgress
            );
        }

        public static bool operator ==(StudentRecord left, StudentRecord right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(StudentRecord left, StudentRecord right)
        {
            return !(left == right);
        }

        public void Deconstruct(
            out string fullName,
            out string recordBookNumber,
            out double averageGrade,
            out int courseProgress)
        {
            fullName = FullName;
            recordBookNumber = RecordBookNumber;
            averageGrade = AverageGrade;
            courseProgress = CourseProgress;
        }

        public override string ToString()
        {
            return $"{FullName} | Залікова: {RecordBookNumber} | " +
                   $"Середній бал: {AverageGrade} | Прогрес: {CourseProgress}%";
        }
    }
}