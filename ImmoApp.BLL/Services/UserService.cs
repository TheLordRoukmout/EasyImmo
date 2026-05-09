using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImmoApp.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace ImmoApp.BLL.Services
{
    public class UserService
    {
        public UserProg CreateUser(string username, string password, string role, string? email, string? phone)
        {
            if (string.IsNullOrEmpty(username))
                throw new Exception("Le nom d'utilisateur est obligatoire");
            if (string.IsNullOrEmpty(password))
                throw new Exception("Le mot de passe est obligatoire");

            using (var context = new ImmoDbContext())
            {
                bool UserNameExists = context.UserProgs.Any(u => u.Username == username);
                {
                    if (UserNameExists)
                        throw new Exception("Ce nom d'utilisateur existe déja");

                    var user = new UserProg
                    {
                        Username = username,
                        PasswordHash = password,
                        Role = role,
                        Email = email,
                        Phone = phone
                    };

                    context.UserProgs.Add(user);
                    context.SaveChanges();
                    return user;
                }
            }
        }
    }

}
