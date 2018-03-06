using System;
using System.Collections.Generic;
using System.Text;

namespace NabSku
{
    public abstract class DiscountParameterBase
    {
        public string DiscountCode { get; set; }
        public virtual int FreeItem { get; set; }
        public virtual int ThresholdItems { get; set; }
    }
}
