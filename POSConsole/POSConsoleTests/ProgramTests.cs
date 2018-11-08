using Microsoft.VisualStudio.TestTools.UnitTesting;
using POSConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace POSConsole.Tests
{
    [TestClass()]
    public class ProgramTests
    {
        [TestMethod()]
        public void GenerateOrder_NewOrderHasCorrectItemsFromTextFile_True()
        {
            var expected = new List<PurchasedProduct>()
            {
                new PurchasedProduct(){Name="Thing1",Category=Category.typeA,Description="1 thing of type A",Price=1},
                new PurchasedProduct(){Name="Thing2",Category=Category.typeA,Description="2 thing of type A",Price=2},
                new PurchasedProduct(){Name="Thing3",Category=Category.typeA,Description="3 thing of type A",Price=3},
                new PurchasedProduct(){Name="Thing4",Category=Category.typeA,Description="4 thing of type A",Price=4},
                new PurchasedProduct(){Name="Thing5",Category=Category.typeB,Description="5 thing of type B",Price=5},
                new PurchasedProduct(){Name="Thing6",Category=Category.typeB,Description="6 thing of type B",Price=6},
                new PurchasedProduct(){Name="Thing7",Category=Category.typeB,Description="7 thing of type B",Price=7},
                new PurchasedProduct(){Name="Thing8",Category=Category.typeB,Description="8 thing of type B",Price=8},
                new PurchasedProduct(){Name="Thing9",Category=Category.typeC,Description="9 thing of type C",Price=9},
                new PurchasedProduct(){Name="Thing10",Category=Category.typeC,Description="10 thing of type C",Price=10},
                new PurchasedProduct(){Name="Thing11",Category=Category.typeC,Description="11 thing of type C",Price=11},
                new PurchasedProduct(){Name="Thing12",Category=Category.typeC,Description="12 thing of type C",Price=12}
            };
            var actual = Program.GenerateNewOrder();
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GenerateOrder_NewOrderDoesNotHaveIncorrectItemsFromTextFile_False()
        {
            var expected = new List<PurchasedProduct>()
            {
                new PurchasedProduct(){Name="Thing1",Category=Category.typeA,Description="1 thing of type A",Price=2},
                new PurchasedProduct(){Name="Thing2",Category=Category.typeA,Description="2 thing of type A",Price=2},
                new PurchasedProduct(){Name="Thing3",Category=Category.typeA,Description="3 thing of type A",Price=3},
                new PurchasedProduct(){Name="Thing4",Category=Category.typeA,Description="4 thing of type A",Price=4},
                new PurchasedProduct(){Name="Thing5",Category=Category.typeB,Description="5 thing of type B",Price=5},
                new PurchasedProduct(){Name="Thing6",Category=Category.typeB,Description="6 thing of type B",Price=6},
                new PurchasedProduct(){Name="Thing7",Category=Category.typeB,Description="7 thing of type B",Price=7},
                new PurchasedProduct(){Name="Thing8",Category=Category.typeB,Description="8 thing of type B",Price=8},
                new PurchasedProduct(){Name="Thing9",Category=Category.typeC,Description="9 thing of type C",Price=9},
                new PurchasedProduct(){Name="Thing10",Category=Category.typeC,Description="10 thing of type C",Price=10},
                new PurchasedProduct(){Name="Thing11",Category=Category.typeC,Description="11 thing of type C",Price=11},
                new PurchasedProduct(){Name="Thing12",Category=Category.typeC,Description="12 thing of type C",Price=12}
            };
            var actual = Program.GenerateNewOrder();
            CollectionAssert.AreNotEqual(expected, actual);
        }
        [TestMethod()]
        public void PromptUser_ValidatorWorksForCCNumberNoSpaces_True()
        {
            string target = "1234123412341234";
            var expected = target;
            var actual = Validator.promptUser("What is the credit card number? ", (num => num.Replace(" ", String.Empty).Length == 16 && Regex.IsMatch(num, @"^[\d ]+$")), target, 0);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void PromptUser_ValidatorWorksForCCNumberSpaces_True()
        {
            string target = "1234 1234 1234 1234";
            var expected = target;
            var actual = Validator.promptUser("What is the credit card number? ", (num => num.Replace(" ", String.Empty).Length == 16 && Regex.IsMatch(num, @"^[\d ]+$")), target, 0);
            Assert.AreEqual(expected, actual);
        }

        public void PromptUser_ValidatorWorksForWrongCCNumber_False()
        {
            string target = "123412341234123";
            var expected = target;
            var actual = Validator.promptUser("What is the credit card number? ", (num => num.Replace(" ", String.Empty).Length == 16 && Regex.IsMatch(num, @"^[\d ]+$")), target, 0);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void PromptUser_ValidatorWorksForCVV3Digit_True()
        {
            string target = "123";
            var expected = target;
            var actual = Validator.promptUser("What is the CVV? ", (num => (num.Replace(" ", String.Empty).Length == 3 || num.Replace(" ", String.Empty).Length == 4) && Regex.IsMatch(num, @"^[\d ]+$")), target, 0);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void PromptUser_ValidatorWorksForCVV4Digit_True()
        {
            string target = "1234";
            var expected = target;
            var actual = Validator.promptUser("What is the CVV? ", (num => (num.Replace(" ", String.Empty).Length == 3 || num.Replace(" ", String.Empty).Length == 4) && Regex.IsMatch(num, @"^[\d ]+$")), target, 0);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void PromptUser_ValidatorWorksForWrongCVV_False()
        {
            string target = "12345";
            var expected = target;
            var actual = Validator.promptUser("What is the CVV? ", (num => (num.Replace(" ", String.Empty).Length == 3 || num.Replace(" ", String.Empty).Length == 4) && Regex.IsMatch(num, @"^[\d ]+$")), target, 0);
            Assert.AreNotEqual(expected, actual);
        }
    }
}











