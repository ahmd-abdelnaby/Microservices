﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedMessages
{
    public class InventoryQuantities
    {
     public List<ProductQuantities> Qts { get; set; }
    }

    public class ProductQuantities
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}