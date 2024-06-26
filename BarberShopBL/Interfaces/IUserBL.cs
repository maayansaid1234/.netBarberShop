﻿using BarberShopDB.EF.Models;
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
        BaseResponse<UserInfo> AddUser(UserAddDTO user);
        BaseResponse<List<User>> GetAllUsers();
        BaseResponse<UserInfo> Login(UserLoginDTO user);
        BaseResponse<UserInfo> GetUserFromSession();
        void Logout();
    }

}

