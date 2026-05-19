using ConsoleApp1.практична_6.інтерфейс;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.практична_6.абстракція
{
    public class Circle : Shape, IResizable, IDrawable, IPrintable
    {
        public double Radius { get; set; }

        public Circle(string name, string color, double radius)
            : base(name, color)
        {
            Radius = radius;
        }

        public override double CalculateArea()
        {
            return Math.PI * Radius * Radius;
        }

        public override double CalculatePerimeter()
        {
            return 2 * Math.PI * Radius;
        }

        public override string GetDescription()
        {
            return
                $"Фігура: {Name}\n" +
                $"Тип: Коло\n" +
                $"Колір: {Color}\n" +
                $"Радіус: {Radius}\n" +
                $"Площа: {CalculateArea():F2}\n" +
                $"Периметр: {CalculatePerimeter():F2}";
        }
        public void Resize(double factor)
        {
            Radius *= factor;
        }

        public void Draw()
        {
            Console.WriteLine($"Малювання кола {Name}");
        }

        public string GetPrintInfo()
        {
            return GetDescription();
        }
    }
}