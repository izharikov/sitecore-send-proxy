﻿using System.Collections.Generic;

namespace SitecoreSendProxy.Models.OrderConfirmation
{
    public class ProductXp
    {
        public string Currency { get; set; }
        public IList<ProductImage> Images { get; set; } = new List<ProductImage>();
    }
}