using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.практична_6.абстракція
{
    public class Square : Rectangle
    {
        public double Side { get; set; }

        public Square(string name, string color, double side)
            : base(name, color, side, side)
        {
            Side = side;
        }

        public override double CalculateArea()
        {
            return Side * Side;
        }

        public override double CalculatePerimeter()
        {
            return 4 * Side;
        }

        public override string GetDescription()
        {
            return
                $"Фігура: {Name}\n" +
                $"Тип: Квадрат\n" +
                $"Колір: {Color}\n" +
                $"Сторона: {Side}\n" +
                $"Площа: {CalculateArea():F2}\n" +
                $"Периметр: {CalculatePerimeter():F2}";
        }
        public void Resize(double factor)
        {
            Side *= factor;
        }
        public void Draw()
        {
            Console.WriteLine($"Малювання квадрата {Name}");
        }
        public string GetPrintInfo()
        {
            return GetDescription();
        }
    }
}