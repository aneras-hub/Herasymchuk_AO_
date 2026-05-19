using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.індивідуальні_завдання
{
    public abstract class Payment
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; }

        protected Payment(decimal amount, string currency)
        {
            Amount = amount;
            Currency = currency;
        }

        public virtual string GetInfo()
        {
            return $"Сума: {Amount} {Currency}\n";
        }

        public abstract bool ProcessPayment();
    }
}