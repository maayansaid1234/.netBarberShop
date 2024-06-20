using BarberShopDB.EF.Models;
using BarberShopDB.Services;
using BarberShopEntities;
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
        BaseResponse<User>  AddUser(User user);
        BaseResponse <List<User>> GetAllUsers();
        BaseResponse<User>  Login(User user);
     

    }
}
