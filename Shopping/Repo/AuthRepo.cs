using Shopping.Repo.IRepo;
using Shopping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using passwordHash = BCrypt.Net.BCrypt;

namespace Shopping.Repo
{
    public class AuthRepo : IAuthRepo
    {
        private readonly MyShoppingDBContext database;
        private readonly AppSettings appSettings;

        public AuthRepo(MyShoppingDBContext dBContext, IOptions<AppSettings> _appSettings)
        {
            database = dBContext;
            appSettings = _appSettings.Value;
        }

        public User Authenticate(string email, string password)
        {
            var user = database.Users.SingleOrDefault(x => x.Email == email);
           //User not found
            if(user == null || !passwordHash.Verify(password, user.Password)) // 
            {
                return null;
            }

            //if found generate token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] { 
                new Claim(ClaimTypes.Name, user.UserId.ToString()),
                new Claim(ClaimTypes.Role,user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials
                (new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescription);
            user.Token = tokenHandler.WriteToken(token);
            user.Password = "";
            return user;
        }

        public bool IsUniqueUser(string email)
        {
            var user = database.Users.SingleOrDefault(x => x.Email == email);

            if(user == null)
            {
                return true;
            }
            return false;
        }
       
        public User Register(string name, string email, string password)
        {
            User userObject = new User()
            {
                Name = name,
                Email = email,
                Password = passwordHash.HashPassword(password),
                Role = "User"
            };

            database.Users.Add(userObject);
            database.SaveChanges();
            userObject.Password = "";
            return userObject;
              
        }


        public User UpdatePassword(int id, string currentpassword, string newpassword)
        {
            throw new NotImplementedException();
        }
    }
}
