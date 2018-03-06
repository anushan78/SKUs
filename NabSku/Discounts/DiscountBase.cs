using NabSku.Types;
using System.Collections.Generic;

namespace NabSku.Discounts
{
    public abstract class DiscountBase
    {
        public string DiscountCode { get; set; }
        public virtual double CalculateDiscountPrice(double unitPrice, List<Product> products)
        {
            return double.MinValue;
        }
    }
}
