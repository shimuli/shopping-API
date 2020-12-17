using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopping.Models.DTOs
{
    public class PostShopingDto
    {
        public int UserId { get; set; }
        public string ItemName { get; set; }
        public int ItemPrice { get; set; }
        public int ItemQuantity { get; set; }
        public byte[] ImageUrl { get; set; }
        public string ItemBarcode { get; set; }

    }
}
