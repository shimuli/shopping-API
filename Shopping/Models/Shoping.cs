using System;
using System.Collections.Generic;

#nullable disable

namespace Shopping.Models
{
    public partial class Shoping
    {
        public int ItemId { get; set; }
        public int UserId { get; set; }
        public string ItemName { get; set; }
        public int ItemPrice { get; set; }
        public int ItemQuantity { get; set; }
        public string ImageUrl { get; set; }
        public string ItemBarcode { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual User User { get; set; }
    }
}
