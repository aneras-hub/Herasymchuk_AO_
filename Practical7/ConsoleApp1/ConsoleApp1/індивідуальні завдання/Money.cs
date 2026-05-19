using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.індивідуальні_завдання
{
    public readonly struct Money : IEquatable<Money>
    {
        public decimal Amount { get; }
        public string Currency { get; }

        public Money(decimal amount, string currency)
        {
            Amount = amount;
            Currency = currency;
        }

        public bool Equals(Money other)
        {
            return Amount == other.Amount &&
                   Currency == other.Currency;
        }

        public override bool Equals(object obj)
        {
            return obj is Money other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Amount, Currency);
        }

        public static bool operator ==(Money left, Money right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Money left, Money right)
        {
            return !(left == right);
        }

        public static Money operator +(Money a, Money b)
        {
            if (a.Currency != b.Currency)
                throw new InvalidOperationException(
                    "Неможливо додати різні валюти."
                );

            return new Money(a.Amount + b.Amount, a.Currency);
        }

        public static Money operator -(Money a, Money b)
        {
            if (a.Currency != b.Currency)
                throw new InvalidOperationException(
                    "Неможливо відняти різні валюти."
                );

            return new Money(a.Amount - b.Amount, a.Currency);
        }

        public static bool operator >(Money a, Money b)
        {
            if (a.Currency != b.Currency)
                throw new InvalidOperationException(
                    "Порівняння різних валют неможливе."
                );

            return a.Amount > b.Amount;
        }

        public static bool operator <(Money a, Money b)
        {
            if (a.Currency != b.Currency)
                throw new InvalidOperationException(
                    "Порівняння різних валют неможливе."
                );

            return a.Amount < b.Amount;
        }

        public void Deconstruct(out decimal amount, out string currency)
        {
            amount = Amount;
            currency = Currency;
        }

        public override string ToString()
        {
            return $"{Amount} {Currency}";
        }
    }
}