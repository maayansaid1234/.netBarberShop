using AutoMapper;
using Azure;
using BarberShopDB.EF.Contexts;
using BarberShopDB.EF.Models;
using BarberShopDB.Interfaces;
using BarberShopEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;




namespace BarberShopDB.Services
{
    public class UserDB : IUserDB
    {
        private readonly BarberShopContext _context;
        private readonly ILogger<UserDB> _logger;
        private readonly IMapper _mapper;
        public UserDB(BarberShopContext context,
            ILogger<UserDB> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public BaseResponse<UserInfo> AddUser(User user)
        {
            User userFromDB = _context.Users.FirstOrDefault(u =>
           u.UserName == user.UserName );
            if (userFromDB == null)
            {
                string hashedPassword = BC.EnhancedHashPassword(
                    user.Password,13);
                user.Password = hashedPassword;
                _context.Users.Add(user);
                _context.SaveChanges();
                _logger.LogInformation($"Adding a new User successfully. userName: {user.UserName}" +
                    $" Id:{user.Id}");
                UserInfo userinfo = _mapper.Map<UserInfo>(user);
                return new BaseResponse<UserInfo>() {StatusCode = 201,
                    Data = userinfo,
                    IsSuccess=true };
            }
            else
            {
                _logger.LogWarning($"" +
                   $"Preventing the creation of a user with an existing username {user.UserName}");
               return new BaseResponse<UserInfo>() {
                   IsSuccess=false,    
                   StatusCode=409,ErrorMessage="There is  already a user with such user name",};

            }

        }
        public BaseResponse<UserInfo> Login(User user)
        {

            User userFromDB = _context.Users.FirstOrDefault(u => u.UserName == user.UserName);

            if (userFromDB != null)
            {
                bool isPasswordValid = BC.EnhancedVerify(user.Password, userFromDB.Password);
                if (isPasswordValid)
                {
                    _logger.LogInformation($"User id: {userFromDB.Id} logged in successfully.");
                    UserInfo userinfo = _mapper.Map<UserInfo>(userFromDB);
                    return new BaseResponse<UserInfo>() { StatusCode = 200, 
                        Data = userinfo,
                        IsSuccess=true };
                }
            }


            _logger.LogWarning("Preventing login with wrong details password or userName ");
            return new BaseResponse<UserInfo>() { StatusCode = 401,IsSuccess=false,
                ErrorMessage = "One or more of the details (password or username) is incorrect."
            };



        }
        public BaseResponse<List<User>> GetAllUsers()
        {
            return new BaseResponse<List<User>>()
             {
                StatusCode = 200,
               Data=_context.Users.ToList(),
               IsSuccess=true
              }
           ;
        }

    }
}

