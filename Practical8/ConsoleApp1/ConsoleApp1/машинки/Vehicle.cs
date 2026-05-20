using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.машинки
{
    public abstract class Vehicle
    {
        public string Model { get; set; }
        public int Year { get; set; }
        public string PlateNumber { get; set; }

        protected Vehicle(string model, int year, string plateNumber)
        {
            Model = model;
            Year = year;
            PlateNumber = plateNumber;
        }

        public virtual string GetInfo()
        {
            return $"Модель: {Model}\nРік: {Year}\nНомер: {PlateNumber}\n";
        }

        public abstract decimal CalculateServiceCost();
    }
}