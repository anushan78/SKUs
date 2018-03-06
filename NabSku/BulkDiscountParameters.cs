using System;
using System.Collections.Generic;
using System.Text;

namespace NabSku
{
    public class BulkDiscountParameters : DiscountParameterBase
    {
        public override int ThresholdItems { get; set; }
    }
}
