using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace POSConsole
{
    public class Program
    {
        static void Main(string[] args)
        {
            string response = String.Empty;
            while(true)
            {
                Transaction transaction = new Transaction();
                transaction.Purchases = GenerateNewOrder();
                while (true)
                {
                    DisplayOrder(ref transaction);
                    int idSelected;
                    response = Validator.promptUser("Enter the number of the item you would like to purchase, 'p' to purchase, or 'q' to quit. ", (str => str == "p" || str == "q" || (int.TryParse(str, out idSelected) && idSelected <= transaction.Purchases.Count && idSelected >= 1)));
                    if (int.TryParse(response, out idSelected))
                    {
                        GetQuantity(idSelected, ref transaction);
                    }
                    else if (response == "p")
                    {
                        break;
                    }
                    else if (response == "q")
                    {
                        return;
                    }
                    Console.Clear();
                }
                GetPayment(ref transaction);
                transaction.Payment.Amount = transaction.TotalCost;
                Console.Clear();
                Console.WriteLine("Here is your receipt:");
                DisplayReceipt(ref transaction);
                Console.WriteLine(transaction.Payment.ToString());
                if(!Validator.promptYN("Would you like to start a new transaction? (y/n) "))
                {
                    return;
                }
                Console.Clear();
            }
            
        }

        private static void GetPayment(ref Transaction transaction)
        {
            Console.Write("Would you like to pay with cash (1), card (2), or check (3)? ");
            switch (Console.ReadLine())
            {
                case "1":
                    transaction.Payment = GetCashPayment();
                    break;
                case "2":
                    transaction.Payment = GetCreditCardPayment();
                    break;
                case "3":
                    transaction.Payment = GetCheckPayment();
                    break;
                default:
                    GetPayment(ref transaction);
                    return;
            }
        }

        private static Check GetCheckPayment()
        {
            Check check = new Check();
            int checkNumber = int.Parse(Validator.promptUser("What is the check number? ", (num => int.TryParse(num, out checkNumber) && checkNumber > 0)));
            check.CheckNumber = checkNumber;
            return check;
        }

        private static Credit GetCreditCardPayment()
        {
            Credit credit = new Credit();
            credit.CardNumber = Validator.promptUser("What is the credit card number? ", (num => num.Replace(" ", String.Empty).Length == 16 && Regex.IsMatch(num, @"^[\d ]+$")));
            int expMonth = int.Parse(Validator.promptUser("What is the credit card expiration two digit month? ", (num => int.TryParse(num, out expMonth) && expMonth >= 1 && expMonth <= 12)));
            int expYear = int.Parse(Validator.promptUser("What is the credit card expiration four digit year? ", (num => int.TryParse(num, out expYear) && expYear >= DateTime.Now.Year && expYear <= 9999)));
            credit.Expiration = new DateTime(expYear, expMonth, DateTime.DaysInMonth(expYear, expMonth));
            credit.CVV = Validator.promptUser("What is the CVV? ", (num => (num.Replace(" ", String.Empty).Length == 3 || num.Replace(" ", String.Empty).Length == 4) && Regex.IsMatch(num, @"^[\d ]+$")));
            return credit;
        }

        private static Cash GetCashPayment()
        {
            Cash cash = new Cash();
            double amountTendered = double.Parse(Validator.promptUser("What is the cash amount? ", (num => double.TryParse(num, out amountTendered) && amountTendered > 0)));
            cash.AmountTendered = amountTendered;
            return cash;
        }

        private static void GetQuantity(int idSelected, ref Transaction transaction)
        {
            Console.Write($"How many {transaction.Purchases[idSelected-1].Name}s would you like? ");
            int quantity;
            if (int.TryParse(Console.ReadLine(), out quantity) && quantity > 0)
            {
                transaction.Purchases[idSelected-1].Quantity = quantity;
            }
        }

        public static List<PurchasedProduct> GenerateNewOrder()
        {
            List<PurchasedProduct> order = new List<PurchasedProduct>();
            using (var reader = new StreamReader(Path.GetFullPath("Products.csv")))
            {
                string line = "";
                reader.ReadLine();
                while ((line = reader.ReadLine()) != null)
                {
                    var values = line.Split(',');
                    order.Add(new PurchasedProduct() { Name = values[0], Category = (Category)Enum.Parse(typeof(Category), values[1]), Description = values[2], Price = double.Parse(values[3]), Quantity = 0 });
                }
            }
            return order;
        }

        private static void DisplayOrder(ref Transaction transaction)
        {
            int counter = 0;
            Console.WriteLine($"{"ID",-5}  {"Name",-10}  {"Category",-10}  {"Description",-20}  {"Price",-11}  {"Quantity",-10}  {"Cost",-11}");
            transaction.Purchases.ForEach(item => Console.WriteLine($"{++counter,-5}  {item.ToString()}"));
            Console.WriteLine($"{"Subtotal: $",79}{transaction.SubTotalCost,-11:0.00}");
            Console.WriteLine($"{"Tax(6.25%): $",79}{transaction.TaxCost,-11:0.00}");
            Console.WriteLine($"{"Total: $",79}{transaction.TotalCost,-11:0.00}");
        }

        private static void DisplayReceipt(ref Transaction transaction)
        {
            Console.WriteLine($"{"Name",-10}  {"Category",-10}  {"Description",-20}  {"Price",-11}  {"Quantity",-10}  {"Cost",-11}");
            transaction.Purchases.Where(item => item.Quantity != 0).ToList().ForEach(item => Console.WriteLine($"{item.ToString()}"));
            Console.WriteLine($"{"Subtotal: $",72}{transaction.SubTotalCost,-11:0.00}");
            Console.WriteLine($"{"Tax(6.25%): $",72}{transaction.TaxCost,-11:0.00}");
            Console.WriteLine($"{"Total: $",72}{transaction.TotalCost,-11:0.00}");
        }
    }
}
