using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryDomain.Entities
{
    public class Inventory: BaseEntity
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Qauntity { get; set; }
    }
}
