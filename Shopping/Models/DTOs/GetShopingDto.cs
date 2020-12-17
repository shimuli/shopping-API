using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopping.Models.DTOs
{
    public class GetShopingDto
    {
        public int UserId { get; set; }
        public string Name { get; set; }
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
