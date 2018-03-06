using Microsoft.VisualStudio.TestTools.UnitTesting;
using NabSku.Discounts;
using NabSku.Discounts.Parameters;
using NabSku.Types;
using System.Collections.Generic;

namespace NabSku.Test
{
    [TestClass]
    public class CheckoutTest
    {
        [TestMethod]
        public void TestNexusTotalPositiveCase1()
        {
            var buyXGetYDiscountParameters = new BuyXGetYParameters();
            buyXGetYDiscountParameters.MinimumNoOfIems = 3;

            var bulkDiscountParameters = new BulkDiscountParameters();
            bulkDiscountParameters.ThresholdItems = 4;

            var discountList = new List<DiscountBase>();
            var buyXGetYDiscount = new BuyXGetYDiscount();
            var bulkDiscount = new BulkDiscount();
            discountList.Add(buyXGetYDiscount);
            discountList.Add(bulkDiscount);

            var checkout = new Checkout(discountList);
            checkout.Scan(AddNexus());
            checkout.Scan(AddNexus());
            checkout.Scan(AddNexus());
            checkout.Scan(AddNexus());
            checkout.Scan(AddNexus());

            var totalPrice = checkout.Total();

            Assert.AreEqual(2499.95, totalPrice);
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
    }
}
