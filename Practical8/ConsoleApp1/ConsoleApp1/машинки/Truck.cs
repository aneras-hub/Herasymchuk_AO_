using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.машинки
{
    public class Truck : Vehicle
    {
        public double LoadCapacity { get; set; }

        public Truck(string model, int year, string plateNumber, double loadCapacity)
            : base(model, year, plateNumber)
        {
            LoadCapacity = loadCapacity;
        }

        public override string GetInfo()
        {
            return base.GetInfo() +
                   $"Тип: Вантажівка\n" +
                   $"Вантажопідйомність: {LoadCapacity} т\n";
        }

        public override decimal CalculateServiceCost()
        {
            return 5000;
        }
    }
}