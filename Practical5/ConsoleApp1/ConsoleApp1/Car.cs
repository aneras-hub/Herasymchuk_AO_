using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Car : Vehicle
    {
        public int PassengerSeats { get; set; }

        public Car(string model, int year, string plateNumber, int passengerSeats)
            : base(model, year, plateNumber)
        {
            PassengerSeats = passengerSeats;
        }

        public override string GetInfo()
        {
            return base.GetInfo() +
                   $"Тип: Легковий автомобіль\n" +
                   $"Кількість місць: {PassengerSeats}\n";
        }

        public override decimal CalculateServiceCost()
        {
            return 1500;
        }
    }
}