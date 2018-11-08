using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSConsole
{
    public class Product
    {
        public string Name { get; set; }
        public Category Category { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }

        public override bool Equals(object obj)
        {
            Product that = (Product)obj;
            if (this.Name == that.Name && this.Category == that.Category && this.Description == that.Description && this.Price == that.Price)
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode() ^ Category.GetHashCode() ^ Description.GetHashCode() ^ Price.GetHashCode();
        }
    }

    public enum Category
    {
        typeA,
        typeB,
        typeC
    }


}
