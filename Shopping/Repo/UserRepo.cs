using Shopping.Repo.IRepo;
using Shopping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BC = BCrypt.Net.BCrypt;

namespace Shopping.Repo
{
    public class UserRepo : IUserRepo
    {
        private readonly MyShoppingDBContext database;

        public UserRepo(MyShoppingDBContext dBContext)
        {
            database = dBContext;
        }
        public bool CreateUser(User user)
        {
            database.Users.Add(user);
            return Save();
        }

        public ICollection<User> GetUsers()
        {
            return database.Users.OrderBy(a => a.Name).ToList();
        }

        public User GetUser(int userId)
        {
            return database.Users.FirstOrDefault(a => a.UserId == userId);
        }

        public bool UpdateUser(User user)
        {
            database.Users.Update(user);
            return Save();
        }

        public bool UserExist(string name)
        {
            bool value = database.Users.Any(a => a.Name.ToLower().Trim() == name.ToLower().ToLower());
            return value;
        }
        public bool UserExist(int userId)
        {
            return database.Users.Any(a => a.UserId == userId);
        }
        public bool Save()
        {
            return database.SaveChanges() >= 0 ? true : false;
        }

        public bool DeleteUser(User user)
        {
            database.Users.Remove(user);
            return Save();
        }

        public User Update(int id, User item)
        {
            database.Users.Update(item);
            return item;
        }
    }
}
