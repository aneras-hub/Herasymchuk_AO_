using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.практична_6.абстракція
{
    public class Triangle : Shape
    {
        public double SideA { get; set; }
        public double SideB { get; set; }
        public double SideC { get; set; }

        public Triangle(
            string name,
            string color,
            double sideA,
            double sideB,
            double sideC
        ) : base(name, color)
        {
            SideA = sideA;
            SideB = sideB;
            SideC = sideC;
        }

        public override double CalculateArea()
        {
            double p = CalculatePerimeter() / 2;

            return Math.Sqrt(
                p *
                (p - SideA) *
                (p - SideB) *
                (p - SideC)
            );
        }

        public override double CalculatePerimeter()
        {
            return SideA + SideB + SideC;
        }

        public override string GetDescription()
        {
            return
                $"Фігура: {Name}\n" +
                $"Тип: Трикутник\n" +
                $"Колір: {Color}\n" +
                $"Сторони: {SideA}, {SideB}, {SideC}\n" +
                $"Площа: {CalculateArea():F2}\n" +
                $"Периметр: {CalculatePerimeter():F2}";
        }
    }
}