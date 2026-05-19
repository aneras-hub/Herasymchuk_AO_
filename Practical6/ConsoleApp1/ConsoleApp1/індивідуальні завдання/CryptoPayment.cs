using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.індивідуальні_завдання
{
    public class CryptoPayment : Payment
    {
        public string WalletAddress { get; set; }
        public string CryptoCurrency { get; set; }

        public CryptoPayment(decimal amount, string currency, string walletAddress, string cryptoCurrency)
            : base(amount, currency)
        {
            WalletAddress = walletAddress;
            CryptoCurrency = cryptoCurrency;
        }

        public override bool ProcessPayment()
        {
            return true;
        }

        public override string GetInfo()
        {
            return base.GetInfo() +
                   $"Тип платежу: Криптовалюта\n" +
                   $"Криптовалюта: {CryptoCurrency}\n" +
                   $"Гаманець: {WalletAddress}\n";
        }
    }
}