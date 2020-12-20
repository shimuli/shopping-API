using Shopping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopping.Repo.IRepo
{
    public interface IAuthRepo
    {
        bool IsUniqueUser(string email);

        User Authenticate(string email, string password);

        User Register(string name, string email, string password);

        User UpdatePassword(int id, string currentpassword, string newpassword);
    }
}
