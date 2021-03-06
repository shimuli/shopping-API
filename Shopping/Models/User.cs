﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Shopping.Models
{
    public partial class User
    {
        public User()
        {
            Inventories = new HashSet<Inventory>();
            Shopings = new HashSet<Shoping>();
        }

        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
        public bool IsConfirmed { get; set; }
        public string Token { get; set; }

        public virtual ICollection<Inventory> Inventories { get; set; }
        public virtual ICollection<Shoping> Shopings { get; set; }
    }
}
