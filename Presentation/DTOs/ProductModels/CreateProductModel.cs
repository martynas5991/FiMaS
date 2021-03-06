﻿using System;

namespace Presentation.DTOs.ProductModels
{
    public class CreateProductModel
    {
        public string Name { get;  set; }
        public string Category { get;  set; }
        public double Price { get;  set; }
        public DateTime BoughtAt { get;  set; }
        public string Provider { get;  set; }
        public double Quantity { get; set; }
        public Guid ReceiptId { get; set; }
    }
}
