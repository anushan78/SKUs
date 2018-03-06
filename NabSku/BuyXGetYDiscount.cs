using System;
using System.Collections.Generic;
using System.Text;

namespace NabSku
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
            throw new NotImplementedException();
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
