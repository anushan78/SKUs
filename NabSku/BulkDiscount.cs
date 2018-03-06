using System;
using System.Collections.Generic;
using System.Text;

namespace NabSku
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

            throw new NotImplementedException();
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
