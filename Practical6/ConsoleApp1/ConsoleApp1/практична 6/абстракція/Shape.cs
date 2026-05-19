using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.практична_6
{
    public abstract class Shape
    {
        public string Name { get; set; }
        public string Color { get; set; }

        protected Shape(string name, string color)
        {
            Name = name;
            Color = color;
        }

        // Віртуальний метод
        public virtual double CalculateArea()
        {
            return 0;
        }

        // Віртуальний метод
        public virtual double CalculatePerimeter()
        {
            return 0;
        }

        // Абстрактний метод
        public abstract string GetDescription();
    }
}