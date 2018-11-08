using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSConsole
{
    public class Transaction
    {
        const double _taxPercent = 0.0625;
        public List<PurchasedProduct> Purchases { get; set; }
        public double SubTotalCost => Purchases.Sum<PurchasedProduct>(purchase => purchase.Cost);
        public double TaxCost => SubTotalCost * _taxPercent;
        public double TotalCost => SubTotalCost + TaxCost;
        public IPayable Payment { get; set; }
    }
}
