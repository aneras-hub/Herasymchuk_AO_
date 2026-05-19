using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    //ПРАКТИЧНЕ 4 ІНДИВІДУАЛЬНЕ
    public class Fraction
    {
        private int numerator;
        private int denominator;

        public int Numerator
        {
            get => numerator;
            set => numerator = value;
        }

        public int Denominator
        {
            get => denominator;
            set
            {
                if (value == 0)
                    throw new DivideByZeroException("Знаменник не може бути 0.");

                denominator = value;
            }
        }

        public Fraction(int numerator, int denominator)
        {
            Numerator = numerator;
            Denominator = denominator;

            Reduce();
        }

        private int GCD(int a, int b)
        {
            a = Math.Abs(a);
            b = Math.Abs(b);

            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }

            return a;
        }

        private void Reduce()
        {
            int gcd = GCD(Numerator, Denominator);

            Numerator /= gcd;
            Denominator /= gcd;

            if (Denominator < 0)
            {
                Numerator *= -1;
                Denominator *= -1;
            }
        }

        public static Fraction operator +(Fraction a, Fraction b)
        {
            return new Fraction(
                a.Numerator * b.Denominator +
                b.Numerator * a.Denominator,

                a.Denominator * b.Denominator
            );
        }

        public static Fraction operator -(Fraction a, Fraction b)
        {
            return new Fraction(
                a.Numerator * b.Denominator -
                b.Numerator * a.Denominator,

                a.Denominator * b.Denominator
            );
        }

        public static Fraction operator *(Fraction a, Fraction b)
        {
            return new Fraction(
                a.Numerator * b.Numerator,
                a.Denominator * b.Denominator
            );
        }

        public static Fraction operator /(Fraction a, Fraction b)
        {
            if (b.Numerator == 0)
                throw new DivideByZeroException("Ділення на 0.");

            return new Fraction(
                a.Numerator * b.Denominator,
                a.Denominator * b.Numerator
            );
        }

        public static bool operator ==(Fraction a, Fraction b)
        {
            return
                a.Numerator == b.Numerator &&
                a.Denominator == b.Denominator;
        }

        public static bool operator !=(Fraction a, Fraction b)
        {
            return !(a == b);
        }

        public static bool operator >(Fraction a, Fraction b)
        {
            return
                a.Numerator * b.Denominator >
                b.Numerator * a.Denominator;
        }

        public static bool operator <(Fraction a, Fraction b)
        {
            return
                a.Numerator * b.Denominator <
                b.Numerator * a.Denominator;
        }

        public static Fraction operator ++(Fraction a)
        {
            return new Fraction(
                a.Numerator + a.Denominator,
                a.Denominator
            );
        }

        public static Fraction operator --(Fraction a)
        {
            return new Fraction(
                a.Numerator - a.Denominator,
                a.Denominator
            );
        }

        public static implicit operator double(Fraction f)
        {
            return (double)f.Numerator / f.Denominator;
        }

        public override bool Equals(object obj)
        {
            if (obj is not Fraction other)
                return false;

            return this == other;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Numerator, Denominator
            );
        }

        public override string ToString()
        {
            return $"{Numerator}/{Denominator}";
        }
    }
    // К 
}