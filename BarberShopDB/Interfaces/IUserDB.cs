using BarberShopDB.EF.Models;
using BarberShopDB.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShopDB.Interfaces
{
    public interface IUserDB
    {
         User AddUser(User user);
        List<User> GetAllUsers();
        User Login(User user);
     

    }
}
