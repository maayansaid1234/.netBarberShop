using BarberShopDB.EF.Models;
using BarberShopEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShopBL.Interfaces
{
    public interface IUserBL
    {
        User AddUser(UserAddDTO user);
        List<User> GetAllUsers();
        User Login(UserLoginDTO user);
        string GetUserName();
        void Logout();
    }

}

