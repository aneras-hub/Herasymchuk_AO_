using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.практична_6.абстракція
{
    public class Rectangle : Shape
    {
        public double Width { get; set; }
        public double Height { get; set; }

        public Rectangle(string name, string color, double width, double height)
            : base(name, color)
        {
            Width = width;
            Height = height;
        }

        public override double CalculateArea()
        {
            return Width * Height;
        }

        public override double CalculatePerimeter()
        {
            return 2 * (Width + Height);
        }

        public override string GetDescription()
        {
            return
                $"Фігура: {Name}\n" +
                $"Тип: Прямокутник\n" +
                $"Колір: {Color}\n" +
                $"Ширина: {Width}\n" +
                $"Висота: {Height}\n" +
                $"Площа: {CalculateArea():F2}\n" +
                $"Периметр: {CalculatePerimeter():F2}";
        }
    }
}
