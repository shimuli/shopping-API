using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopping.Models.DTOs
{
    public class InventoryDto
    {
        public int InventoryId { get; set; }
        public string ProductName { get; set; }
        public int ProductPrice { get; set; }
        public int Quantity { get; set; }
        public int CurrentQuantity { get; set; }
        public byte[] ImageUrl { get; set; }
        public string Barcode { get; set; }
        public DateTime Dateposted { get; set; }
        public int UserId { get; set; }
        public bool Isavailable { get; set; }
    }
}
