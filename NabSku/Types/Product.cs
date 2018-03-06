namespace NabSku.Types
{
    public class Product
    {
        public string SkuCode { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string DiscountCode { get; set; }
        public bool IsTracked { get; set; }
    }
}
