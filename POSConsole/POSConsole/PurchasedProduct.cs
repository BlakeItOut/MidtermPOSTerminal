using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSConsole
{
    public class PurchasedProduct : Product  
    {
        public int Quantity { get; set; }
        public double Cost => base.Price * Quantity;


        public override string ToString()
        {
            return $"{Name,-10}  {Category,-10}  {Description,-20}  ${Price,-10:0.00}  {Quantity,-10}  ${Cost,-10:0.00}";
        }
    }
}
