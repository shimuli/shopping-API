using Shopping.Repo.IRepo;
using Shopping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopping.Repo
{
    public class ShopingRepo : IShoppingRepo
    {
        private readonly MyShoppingDBContext database;

        public ShopingRepo(MyShoppingDBContext dBContext)
        {
            database = dBContext;
        }

        public bool CreateShoping(Shoping shoping)
        {
            database.Shopings.Add(shoping);
            return Save();
        }

        public bool DeleteShoping(Shoping shoping)
        {
            database.Shopings.Remove(shoping);
            return Save();
        }

        public ICollection<ShoppingList> GetShopingbyDate(int userId, DateTime? startdate, DateTime? enddate)
        {
            return database.ShoppingLists.Where(c => c.UserId == userId && ((!startdate.HasValue || c.DateCreated >= startdate) && (!enddate.HasValue || c.DateCreated <= enddate))).ToList();
        }

        public ICollection<Shoping> GetShopingbyName(string name)
        {
            return database.Shopings.Where(a => a.ItemName == name).ToList();
        }

        public Shoping GetShopingbyNameandId(int itemId, string name)
        {
            return database.Shopings.Where(c => c.ItemId == itemId && c.ItemName == name).FirstOrDefault();
        }

        public ICollection<ShoppingList> GetShopings()
        {
            return database.ShoppingLists.OrderBy(a => a.ItemName).ToList();
        }

        public ShoppingList GetShopping(int itemId)
        {
            return database.ShoppingLists.FirstOrDefault(a => a.ItemId == itemId);
        }

        public bool Save()
        {
            return database.SaveChanges() >= 0 ? true : false;
        }

        public bool ShopingExist(string itemname)
        {
            return database.Shopings.Any(a => a.ItemName == itemname);
        }

        public bool ShopingExist(int itemId)
        {
            return database.Shopings.Any(a => a.ItemId == itemId);
        }

        public bool UpdateShoping(Shoping shoping)
        {
            database.Shopings.Update(shoping);
            return Save();
        }
    }
}
