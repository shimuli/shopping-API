using Shopping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopping.Repo.IRepo
{
   public interface IShoppingRepo
    {
        ICollection<ShoppingList> GetShopings();
        ShoppingList GetShopping(int itemId);
        Shoping GetShopingbyNameandId(int itemId, string name);
        ICollection<Shoping> GetShopingbyName(string name);
        ICollection<ShoppingList> GetShopingbyDate(int userId, DateTime? startdate, DateTime? enddate);
        bool ShopingExist(string itemname);
        bool CreateShoping(Shoping shoping);
        bool UpdateShoping(Shoping shoping);
        bool DeleteShoping(Shoping shoping);
        bool ShopingExist(int itemId);
        bool Save();
    }
}
