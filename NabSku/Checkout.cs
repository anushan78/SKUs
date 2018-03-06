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
            _checkoutItems = _checkoutItems.OrderBy(item => item.SkuCode).ToList();
            var uniqueItems = _checkoutItems
                .Select(it => it.SkuCode)
                .Distinct().ToList();
            double totalDiscountPrice = 0.0;

            uniqueItems.ForEach(delegate (string item) {
                var product = _checkoutItems.Where(chi => chi.SkuCode == item).First();

                if (!string.IsNullOrEmpty(product.DiscountCode))
                {
                    var discount = _discounts
                        .Where(dis => dis.DiscountCode == product.DiscountCode).First();

                    switch (discount.DiscountCode)
                    {
                        case "BUYGET":
                            var buyGetParameters = new BuyXGetYParameters()
                            {
                                DiscountCode = discount.DiscountCode,
                                DiscountSku = product.SkuCode,
                                MinimumNoOfIems = 3, // configure this to product
                                FreeNoOfItems = 1 // configure this to product
                            };

                            var buyGetDiscount = discount as BuyXGetYDiscount;
                            buyGetDiscount.DiscountParameters = buyGetParameters;
                            totalDiscountPrice += discount.CalculateDiscountPrice(product.Price, _checkoutItems);
                            break;
                        case "BULK":
                            var bulkParameters = new BulkDiscountParameters()
                            {
                                DiscountCode = discount.DiscountCode,
                                DiscountSku = product.SkuCode,
                                ThresholdItems = 4, // configure this to product
                                DiscountUnitPrice = 499.99 // configure this to product
                            };

                            var bulkDiscount = discount as BulkDiscount;
                            bulkDiscount.DiscountParameters = bulkParameters;
                            totalDiscountPrice += discount.CalculateDiscountPrice(product.Price, _checkoutItems);
                            break;
                        case "FREEITEM":
                            var freeItemParameters = new FreeItemDiscountParameters()
                            {
                                DiscountCode = discount.DiscountCode,
                                DiscountSku = product.SkuCode,
                                FreeItemSku = "hdm", // configure this to product
                                EligibleItemSku = "mbp" // configure this to product
                            };

                            var freeItemDiscount = discount as FreeItemDiscount;
                            freeItemDiscount.DiscountParameters = freeItemParameters;
                            totalDiscountPrice += discount.CalculateDiscountPrice(product.Price, _checkoutItems);
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    var nonDiscountPriceItemsCount = _checkoutItems.Where(chi => chi.SkuCode == product.SkuCode).Count();
                    totalDiscountPrice += nonDiscountPriceItemsCount * product.Price;
                }
            });

            return totalDiscountPrice;
        }


    }
}
