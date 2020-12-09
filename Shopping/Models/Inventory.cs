using System;
using System.Collections.Generic;

#nullable disable

namespace Shopping.Models
{
    public partial class Inventory
    {
        public int InventoryId { get; set; }
        public string ProductName { get; set; }
        public int ProductPrice { get; set; }
        public int Quantity { get; set; }
        public int CurrentQuantity { get; set; }
        public string ImageUrl { get; set; }
        public string Barcode { get; set; }
        public DateTime Dateposted { get; set; }
        public int UserId { get; set; }
        public bool Isavailable { get; set; }

        public virtual User User { get; set; }
    }
}
