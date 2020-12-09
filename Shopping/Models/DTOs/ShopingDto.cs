using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopping.Models.DTOs
{
    public class ShopingDto
    {
        public int ItemId { get; set; }
        public int UserId { get; set; }
        public string ItemName { get; set; }
        public int ItemPrice { get; set; }
        public int ItemQuantity { get; set; }
        public string ImageUrl { get; set; }
        public string ItemBarcode { get; set; }
        public DateTime DateCreated { get; set; }

        public  UsersDto User { get; set; }
    }
}
