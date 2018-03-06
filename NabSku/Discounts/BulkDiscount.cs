using NabSku.Discounts.Parameters;
using NabSku.Types;
using System.Collections.Generic;
using System.Linq;

namespace NabSku.Discounts
{
    public class BulkDiscount : DiscountBase
    {
        private BulkDiscountParameters _discountParameters;

        public BulkDiscount()
        {
            DiscountCode = "BULK";
        }

        public override double CalculateDiscountPrice(double unitPrice, List<Product> products)
        {
            double totalDiscountedPrice = 0.0;
            var bulkDiscountProducts = products.Where(pr => pr.SkuCode == _discountParameters.DiscountSku).ToList();

            if (bulkDiscountProducts.Count >= _discountParameters.ThresholdItems)
            {
                totalDiscountedPrice = _discountParameters.DiscountUnitPrice * bulkDiscountProducts.Count;
            }
            else
            {
                totalDiscountedPrice = unitPrice * bulkDiscountProducts.Count;
            }

            products.ForEach(delegate (Product product) {
                if (product.SkuCode == _discountParameters.DiscountSku)
                    product.IsTracked = true;
            });

            return totalDiscountedPrice;
        }

        public BulkDiscountParameters DiscountParameters
        {
            get
            {
                return _discountParameters;
            }
            set
            {
                _discountParameters = value;
            }
        }
    }
}
