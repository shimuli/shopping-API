using Shopping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopping.Repo.IRepo
{
    public interface IInventoryRepo
    {
        ICollection<InventoryList> GetInventorys();
        InventoryList GetInventory(int inventoryId);
        Inventory GetInventoryData(int inventoryId);
        Inventory GetInventorybyNameandId(int inventoryId, string name);
        ICollection<Inventory> GetInventorybyName(string name);
        ICollection<InventoryList> GetInventorybyDate(int userId, DateTime? startdate, DateTime? enddate);
        bool InventoryExist(string itemname);
        bool CreateInventory(Inventory inventory);
        bool UpdateInventory(Inventory inventory);
        bool DeleteInventory(Inventory inventory);
        bool InventoryExist(int itemId);
        bool Save();
    }
}
