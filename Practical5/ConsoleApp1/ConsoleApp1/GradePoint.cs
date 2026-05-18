using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class GradePoint
    {
        private double value;
        public double Value
        {
            get => value;
            set
            {
                if (value < 0 || value > 10)
                    throw new ArgumentException("Оцінка має бути від 0 до 10.");
                this.value = value;
            }
        }
        public GradePoint(double value)
        {
            Value = value;
        }
        // +
        public static GradePoint operator +(GradePoint a, GradePoint b)
        {
            double result = a.Value + b.Value;
            if (result > 10)
                result = 10;
            return new GradePoint(result);
        }
        // ++
        public static GradePoint operator ++(GradePoint g)
        {
            if (g.Value < 10)
                g.Value++;
            return g;
        }
        // --
        public static GradePoint operator --(GradePoint g)
        {
            if (g.Value > 0)
                g.Value--;
            return g;
        }
        // >
        public static bool operator >(GradePoint a, GradePoint b)
        {
            return a.Value > b.Value;
        }
        // <
        public static bool operator <(GradePoint a, GradePoint b)
        {
            return a.Value < b.Value;
        }
        // >=
        public static bool operator >=(GradePoint a, GradePoint b)
        {
            return a.Value >= b.Value;
        }
        // <=
        public static bool operator <=(GradePoint a, GradePoint b)
        {
            return a.Value <= b.Value;
        }
        // true
        public static bool operator true(GradePoint g)
        {
            return g.Value >= 8;
        }
        // false
        public static bool operator false(GradePoint g)
        {
            return g.Value < 8;
        }
        // GradePoint -> double
        public static implicit operator double(GradePoint g)
        {
            return g.Value;
        }
        // double -> GradePoint
        public static implicit operator GradePoint(double value)
        {
            return new GradePoint(value);
        }
        public override string ToString()
        {
            return Value.ToString("0.##");
        }
    }
}
