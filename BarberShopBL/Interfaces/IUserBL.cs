using BarberShopDB.EF.Models;
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
        User AddUser(User user);
        List<User> GetAllUsers();
        User LogIn(User user);
        
    }
}
