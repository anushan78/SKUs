﻿namespace NabSku.Discounts.Parameters
{
    public class BuyXGetYParameters : DiscountParameterBase
    {
        public int MinimumNoOfIems { get; set; }
        public int FreeNoOfItems { get; set; }
    }
}
