using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NabSku
{
    public class Checkout
    {
        public Checkout(List<DiscountBase> discounts)
        {
            _discounts = discounts;
            _checkoutItems = new List<Product>();
        }

        private List<DiscountBase> _discounts { get; set; }
        private List<Product> _checkoutItems { get; set; }

        public void Scan(Product item)
        {
            _checkoutItems.Add(item);
        }

        public double Total()
        {
            _checkoutItems.OrderBy(item => item.SkuCode);
            var uniqueItems = _checkoutItems.Select(it => it.SkuCode).Distinct().ToList();
            double discountPrice;

            uniqueItems.ForEach(delegate (string item) {
                var product = _checkoutItems.Where(chi => chi.SkuCode == item).First();
                var discount = _discounts.Where(dis => dis.DiscountCode == product.DiscountCode).First();

                switch (discount.DiscountCode)
                {
                    case "BUYGET":
                        var buyGetParameters = new BuyXGetYParameters()
                        {
                            DiscountCode = discount.DiscountCode,
                            MinimumNoOfIems = 3 // configure this to product
                        };

                        var buyGetDiscount = discount as BuyXGetYDiscount;
                        buyGetDiscount.DiscountParameters = buyGetParameters;
                        discountPrice = discount.CalculateDiscountPrice(product.Price, _checkoutItems);
                        break;
                    case "BULK":
                        var bulkParameters = new BulkDiscountParameters()
                        {
                            DiscountCode = discount.DiscountCode,
                            ThresholdItems = 4 // configure this to product
                        };

                        var bulkDiscount = discount as BulkDiscount;
                        bulkDiscount.DiscountParameters = bulkParameters;
                        discountPrice = discount.CalculateDiscountPrice(product.Price, _checkoutItems);
                        break;
                    default:
                        break;
                }
            });

            return double.MinValue;
        }
    }
}
