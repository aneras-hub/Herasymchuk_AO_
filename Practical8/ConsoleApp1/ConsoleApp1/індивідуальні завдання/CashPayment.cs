using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.індивідуальні_завдання
{
    public class CashPayment : Payment
    {
        public string CashierName { get; set; }

        public CashPayment(decimal amount, string currency, string cashierName)
            : base(amount, currency)
        {
            CashierName = cashierName;
        }

        public override bool ProcessPayment()
        {
            return true;
        }

        public override string GetInfo()
        {
            return base.GetInfo() +
                   $"Тип платежу: Готівка\n" +
                   $"Касир: {CashierName}\n";
        }
    }
}