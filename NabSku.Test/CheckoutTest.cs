using Microsoft.VisualStudio.TestTools.UnitTesting;
using NabSku.Discounts;
using NabSku.Types;
using System.Collections.Generic;

namespace NabSku.Test
{
    [TestClass]
    public class CheckoutTest
    {
        List<DiscountBase> _discountList;

        [TestInitialize]
        public void Setup()
        {
            _discountList = new List<DiscountBase>();
            _discountList.Add(new BuyXGetYDiscount());
            _discountList.Add(new BulkDiscount());
            _discountList.Add(new FreeItemDiscount());
        }

        [TestMethod]
        public void TestNexusTotalPositiveCase1()
        {
            var checkout = new Checkout(_discountList);
            checkout.Scan(AddNexus());
            checkout.Scan(AddNexus());
            checkout.Scan(AddNexus());
            checkout.Scan(AddNexus());
            checkout.Scan(AddNexus());

            var totalPrice = checkout.Total();

            Assert.AreEqual(2499.95, totalPrice);
        }

        [TestMethod]
        public void TestNexusAppleTvTotalPositiveCase2()
        {
            var checkout = new Checkout(_discountList);
            checkout.Scan(AddAppleTv());
            checkout.Scan(AddNexus());
            checkout.Scan(AddNexus());
            checkout.Scan(AddAppleTv());
            checkout.Scan(AddNexus());
            checkout.Scan(AddNexus());
            checkout.Scan(AddNexus());

            var totalPrice = checkout.Total();

            Assert.AreEqual(2718.95, totalPrice);
        }

        [TestMethod]
        public void TestAppleTvTotalPositiveCase1()
        {
            var checkout = new Checkout(_discountList);
            checkout.Scan(AddAppleTv());
            checkout.Scan(AddAppleTv());
            checkout.Scan(AddAppleTv());
            checkout.Scan(AddHdm());

            var totalPrice = checkout.Total();

            Assert.AreEqual(249.00, totalPrice);
        }

        [TestMethod]
        public void TestMacBookProTotalPositiveCase1()
        {
            var checkout = new Checkout(_discountList);
            checkout.Scan(AddMacBookPro());
            checkout.Scan(AddHdm());
            checkout.Scan(AddNexus());

            var totalPrice = checkout.Total();

            Assert.AreEqual(1949.98, totalPrice);
        }



        [TestCleanup]
        public void Cleanup()
        {
            _discountList = null;
        }

        private Product AddNexus()
        {
            return new Product()
            {
                DiscountCode = "BULK",
                Name = "Nexus 9",
                SkuCode = "nx9",
                Price = 549.99
            };
        }

        private Product AddAppleTv()
        {
            return new Product()
            {
                DiscountCode = "BUYGET",
                Name = "Apple TV",
                SkuCode = "atv",
                Price = 109.50
            };
        }

        private Product AddHdm()
        {
            return new Product()
            {
                DiscountCode = "FREEITEM",
                Name = "HDMI adapter",
                SkuCode = "hdm",
                Price = 30.00
            };
        }

        private Product AddMacBookPro()
        {
            return new Product()
            {
                Name = "MacBook Pro",
                SkuCode = "mbp",
                Price = 1399.99
            };
        }
    }
}
