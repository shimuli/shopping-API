using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopping.Models.DTOs
{
    public class UpdateUserDto
    {
        public int UserId { get; set; }
        public string Role { get; set; }
        public bool IsConfirmed { get; set; }
    }
}
