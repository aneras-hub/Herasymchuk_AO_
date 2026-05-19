using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.практична_7
{
    public readonly struct ComplexNumber : IEquatable<ComplexNumber>
    {
        public double Real { get; }
        public double Imaginary { get; }

        public ComplexNumber(double real, double imaginary)
        {
            Real = real;
            Imaginary = imaginary;
        }

        public bool Equals(ComplexNumber other)
        {
            return Real.Equals(other.Real) &&
                   Imaginary.Equals(other.Imaginary);
        }

        public override bool Equals(object obj)
        {
            return obj is ComplexNumber other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Real, Imaginary);
        }

        public static bool operator ==(ComplexNumber left, ComplexNumber right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(ComplexNumber left, ComplexNumber right)
        {
            return !(left == right);
        }

        public static ComplexNumber operator +(ComplexNumber a, ComplexNumber b)
        {
            return new ComplexNumber(
                a.Real + b.Real,
                a.Imaginary + b.Imaginary
            );
        }

        public static ComplexNumber operator -(ComplexNumber a, ComplexNumber b)
        {
            return new ComplexNumber(
                a.Real - b.Real,
                a.Imaginary - b.Imaginary
            );
        }

        public void Deconstruct(out double real, out double imaginary)
        {
            real = Real;
            imaginary = Imaginary;
        }

        public override string ToString()
        {
            return $"{Real} + {Imaginary}i";
        }
    }
}
