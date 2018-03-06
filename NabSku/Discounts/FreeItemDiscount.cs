using NabSku.Discounts.Parameters;
using NabSku.Types;
using System.Linq;
using System.Collections.Generic;

namespace NabSku.Discounts
{
    public class FreeItemDiscount : DiscountBase
    {
        private FreeItemDiscountParameters _discountParameters;

        public FreeItemDiscount()
        {
            DiscountCode = "FREEITEM";
        }

        public override double CalculateDiscountPrice(double unitPrice, List<Product> products)
        {
            double totalDiscountedPrice = 0.0;
            var freeItemDiscountProducts = products
                .Where(pr => pr.SkuCode == _discountParameters.DiscountSku).ToList();

            if (products.Any(pr => pr.SkuCode == _discountParameters.EligibleItemSku))
            {
                var elgibleItemsCount = products.Where(pr => pr.SkuCode == _discountParameters.EligibleItemSku).ToList().Count;
                if (freeItemDiscountProducts.Count > elgibleItemsCount)
                {
                    totalDiscountedPrice = (freeItemDiscountProducts.Count - elgibleItemsCount) * unitPrice;
                }
            }
            else
            {
                totalDiscountedPrice = unitPrice * freeItemDiscountProducts.Count;
            }

            products.ForEach(delegate (Product product) {
                if (product.SkuCode == _discountParameters.DiscountSku)
                    product.IsTracked = true;
            });

            return totalDiscountedPrice;
        }

        public FreeItemDiscountParameters DiscountParameters
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
