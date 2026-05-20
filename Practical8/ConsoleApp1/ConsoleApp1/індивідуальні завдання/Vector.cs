using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    // ПРАКТИЧНА 4 VECTOR
    public class Vector
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public Vector(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        // Довжина вектора
        public double Length =>
            Math.Sqrt(X * X + Y * Y + Z * Z);

        // +
        public static Vector operator +(Vector a, Vector b)
        {
            return new Vector(
                a.X + b.X,
                a.Y + b.Y,
                a.Z + b.Z
            );
        }

        // -
        public static Vector operator -(Vector a, Vector b)
        {
            return new Vector(
                a.X - b.X,
                a.Y - b.Y,
                a.Z - b.Z
            );
        }

        // * (скалярне множення)
        public static double operator *(Vector a, Vector b)
        {
            return
                a.X * b.X +
                a.Y * b.Y +
                a.Z * b.Z;
        }

        // ==
        public static bool operator ==(Vector a, Vector b)
        {
            if (ReferenceEquals(a, b))
                return true;

            if (a is null || b is null)
                return false;

            return
                a.X == b.X &&
                a.Y == b.Y &&
                a.Z == b.Z;
        }

        // !=
        public static bool operator !=(Vector a, Vector b)
        {
            return !(a == b);
        }

        // >
        public static bool operator >(Vector a, Vector b)
        {
            return a.Length > b.Length;
        }

        // <
        public static bool operator <(Vector a, Vector b)
        {
            return a.Length < b.Length;
        }

        // ++
        public static Vector operator ++(Vector v)
        {
            v.X++;
            v.Y++;
            v.Z++;

            return v;
        }

        // --
        public static Vector operator --(Vector v)
        {
            v.X--;
            v.Y--;
            v.Z--;

            return v;
        }

        // приведення до double
        public static explicit operator double(Vector v)
        {
            return v.Length;
        }

        public override bool Equals(object obj)
        {
            if (obj is not Vector other)
                return false;

            return this == other;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Z);
        }

        public override string ToString()
        {
            return $"({X}, {Y}, {Z})";
        }
    }
}
