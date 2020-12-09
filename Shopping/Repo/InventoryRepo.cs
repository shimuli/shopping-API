﻿using Shopping.Repo.IRepo;
using Shopping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopping.Repo
{
    public class InventoryRepo : IInventoryRepo
    {
        private readonly MyShoppingDBContext database;

        public InventoryRepo(MyShoppingDBContext dBContext)
        {
            database = dBContext;
        }
        public bool CreateInventory(Inventory inventory)
        {
            database.Inventories.Add(inventory);
            return Save();
        }

        public ICollection<InventoryList> GetInventorys()
        {
            return database.InventoryLists.OrderBy(a => a.ProductName).ToList();
        }

        public InventoryList GetInventory(int inventoryId)
        {
            return database.InventoryLists.FirstOrDefault(a => a.InventoryId == inventoryId);
        }

        
        public bool UpdateInventory(Inventory inventory)
        {
            database.Inventories.Update(inventory);
            return Save();
        }

        public bool DeleteInventory(Inventory inventory)
        {
            database.Inventories.Remove(inventory);
            return Save();
        }

        public bool Save()
        {
            return database.SaveChanges() >= 0 ? true : false;
        }
        public bool InventoryExist(string itemname)
        {
            bool value = database.Inventories.Any(a => a.ProductName.ToLower().Trim() == itemname.ToLower().ToLower());
            return value;
        }

        public bool InventoryExist(int itemId)
        {
            return database.Inventories.Any(a => a.InventoryId == itemId);
        }
    }
}