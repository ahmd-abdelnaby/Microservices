using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryAppliction.Models
{
    public class ProductAvaliblity
    {
        public int Id { get; set; }
        public bool Avalible { get; set; }
        public string? Reason { get; set; }

    }
}
