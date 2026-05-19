using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.індивідуальні_завдання
{
    public class CardPayment : Payment
    {
        public string CardNumber { get; set; }

        public CardPayment(decimal amount, string currency, string cardNumber)
            : base(amount, currency)
        {
            CardNumber = cardNumber;
        }

        public override bool ProcessPayment()
        {
            return true;
        }

        public override string GetInfo()
        {
            return base.GetInfo() +
                   $"Тип платежу: Банківська карта\n" +
                   $"Карта: **** **** **** {CardNumber[^4..]}\n";
        }
    }
}