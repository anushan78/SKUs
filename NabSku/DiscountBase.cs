using System;
using System.Collections.Generic;
using System.Text;

namespace NabSku
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
