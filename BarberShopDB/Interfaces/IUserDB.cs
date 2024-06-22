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
        BaseResponse<UserInfo>  AddUser(User user);
        BaseResponse <List<User>> GetAllUsers();
        BaseResponse<UserInfo>  Login(User user);
     

    }
}
