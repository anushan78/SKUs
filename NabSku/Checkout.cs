using NabSku.Discounts;
using NabSku.Discounts.Parameters;
using NabSku.Types;
using System.Collections.Generic;
using System.Linq;

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
            double discountPrice = 0.0;

            uniqueItems.ForEach(delegate (string item) {
                var product = _checkoutItems.Where(chi => chi.SkuCode == item).First();
                var discount = _discounts.Where(dis => dis.DiscountCode == product.DiscountCode).First();

                switch (discount.DiscountCode)
                {
                    case "BUYGET":
                        var buyGetParameters = new BuyXGetYParameters()
                        {
                            DiscountCode = discount.DiscountCode,
                            DiscountSku = product.SkuCode,
                            MinimumNoOfIems = 3 // configure this to product
                        };

                        var buyGetDiscount = discount as BuyXGetYDiscount;
                        buyGetDiscount.DiscountParameters = buyGetParameters;
                        discountPrice += discount.CalculateDiscountPrice(product.Price, _checkoutItems);
                        break;
                    case "BULK":
                        var bulkParameters = new BulkDiscountParameters()
                        {
                            DiscountCode = discount.DiscountCode,
                            DiscountSku = product.SkuCode,
                            ThresholdItems = 4, // configure this to product
                            DiscountUnitPrice = 499.99
                        };

                        var bulkDiscount = discount as BulkDiscount;
                        bulkDiscount.DiscountParameters = bulkParameters;
                        discountPrice += discount.CalculateDiscountPrice(product.Price, _checkoutItems);
                        break;
                    default:
                        break;
                }
            });

            return discountPrice;
        }
    }
}
