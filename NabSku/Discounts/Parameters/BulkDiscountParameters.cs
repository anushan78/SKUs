namespace NabSku.Discounts.Parameters
{
    public class BulkDiscountParameters : DiscountParameterBase
    {
        public int ThresholdItems { get; set; }
        public double DiscountUnitPrice { get; set; }
    }
}
