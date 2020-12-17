using System;
using System.Collections.Generic;

#nullable disable

namespace Shopping.Models
{
    public partial class ShoppingList
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public bool IsConfirmed { get; set; }
        public int? ItemId { get; set; }
        public string ItemName { get; set; }
        public int? ItemPrice { get; set; }
        public int? ItemQuantity { get; set; }
        public int? TotalBuyingPrice { get; set; }
        public byte[] ImageUrl { get; set; }
        public string ItemBarcode { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
