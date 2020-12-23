using Shopping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopping.Repo.IRepo
{
    public interface IUserRepo
    {
        ICollection<User> GetUsers();
        User GetUser(int userId);
        bool UserExist(string name);
        bool UserExist(int userId);
        bool CreateUser(User user);
        bool UpdateUser(User user);

        //void Update(User user, string password = null);
        User Update(int id, User item);
        bool DeleteUser(User user);
        bool Save();

    }
}
