using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.практична_7
{
    public readonly struct Point : IEquatable<Point>
    {
        public int Row { get; }
        public int Column { get; }

        public Point(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public bool Equals(Point other)
        {
            return Row == other.Row && Column == other.Column;
        }

        public override bool Equals(object obj)
        {
            return obj is Point other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Row, Column);
        }

        public static bool operator ==(Point left, Point right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Point left, Point right)
        {
            return !(left == right);
        }

        public void Deconstruct(out int row, out int column)
        {
            row = Row;
            column = Column;
        }

        public override string ToString()
        {
            return $"Ряд: {Row}, Стовпець: {Column}";
        }
    }
}
