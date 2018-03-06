using NabSku.Discounts.Parameters;
using NabSku.Types;
using System.Linq;
using System.Collections.Generic;

namespace NabSku.Discounts
{
    public class BuyXGetYDiscount : DiscountBase
    {
        private BuyXGetYParameters _discountParameters;

        public BuyXGetYDiscount()
        {
            DiscountCode = "BUYGET";
        }

        public override double CalculateDiscountPrice(double unitPrice, List<Product> products)
        {
            double totalDiscountedPrice = 0.0;
            var buyGetDiscountProducts = products.Where(pr => pr.SkuCode == _discountParameters.DiscountSku).ToList();

            var buyGetDivisions = buyGetDiscountProducts.Count / _discountParameters.MinimumNoOfIems;
            if (buyGetDivisions > 0)
            {
                var buyGetRemainder = buyGetDiscountProducts.Count % _discountParameters.MinimumNoOfIems;
                totalDiscountedPrice += buyGetDivisions * (_discountParameters.MinimumNoOfIems - _discountParameters.FreeNoOfItems) * unitPrice;
                if (buyGetRemainder > 0)
                    totalDiscountedPrice += buyGetRemainder * unitPrice;
            }
            else
            {
                totalDiscountedPrice = unitPrice * buyGetDiscountProducts.Count;
            }

            products.ForEach(delegate (Product product) {
                if (product.SkuCode == _discountParameters.DiscountSku)
                    product.IsTracked = true;
            });

            return totalDiscountedPrice;
        }

        public BuyXGetYParameters DiscountParameters
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
