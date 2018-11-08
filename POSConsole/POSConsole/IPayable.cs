using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSConsole
{
    public interface IPayable
    {
        double Amount { get; set; }
    }

    public class Cash : IPayable
    {
        public double Amount { get; set; }
        public double AmountTendered { get; set; }
        public double Change => AmountTendered - Amount;
        public override string ToString()
        {
            return $"You paid in cash with ${AmountTendered:0.00} and received ${Change:0.00} in change.";
        }
    }

    public class Credit : IPayable
    {
        public double Amount { get; set; }
        public string CardNumber { get; set; }
        public DateTime Expiration { get; set; }
        public string CVV { get; set; }
        public override string ToString()
        {
            return $"You paid ${Amount:0.00} with a credit card ending in {CardNumber.Substring(CardNumber.Length-4)}";
        }
    }

    public class Check : IPayable
    {
        public double Amount { get; set; }
        public int CheckNumber { get; set; }
        public override string ToString()
        {
            return $"You paid ${Amount:0.00} with check number {CheckNumber}.";
        }
    }
}
