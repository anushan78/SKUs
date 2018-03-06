namespace NabSku.Discounts.Parameters
{
    public class FreeItemDiscountParameters : DiscountParameterBase
    {
        public string FreeItemSku { get; set; }
        public string EligibleItemSku { get; set; }
    }
}
