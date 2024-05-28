using BarberShopBL.Interfaces;
using BarberShopDB.EF.Models;
using BarberShopDB.Interfaces;
using BarberShopDB.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShopBL.Services
{
    public class UserBL : IUserBL
    {
        private readonly IUserDB _userDB;
      public UserBL(IUserDB userDB)
        {
            _userDB = userDB;
        }

        public List<User> GetAllUsers()
        {
            return _userDB.GetAllUsers();
        }
        public User AddUser(User user)
        {
            return _userDB.AddUser(user);
        }
        public User LogIn(User user)
        {
            return _userDB.LogIn(user);
        }
     
    }
}
