using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Bus : Vehicle
    {
        public int Capacity { get; set; }

        public Bus(string model, int year, string plateNumber, int capacity)
            : base(model, year, plateNumber)
        {
            Capacity = capacity;
        }

        public override string GetInfo()
        {
            return base.GetInfo() +
                   $"Тип: Автобус\n" +
                   $"Місткість: {Capacity} пасажирів\n";
        }

        public override decimal CalculateServiceCost()
        {
            return 3000;
        }
    }
}
