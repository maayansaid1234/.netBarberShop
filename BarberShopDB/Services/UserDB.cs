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
using System.Text;
using System.Threading.Tasks;




namespace BarberShopDB.Services
{
    public class UserDB : IUserDB
    {
        private readonly BarberShopContext _context;
        private readonly ILogger<UserDB> _logger;
        public UserDB(BarberShopContext context,
            ILogger<UserDB> logger)
        {
            _context = context;
            _logger = logger;
        }

        public User AddUser(User user)
        {
            User userFromDB = _context.Users.FirstOrDefault(u =>
           u.UserName == user.UserName && u.Password == user.Password);
            if (userFromDB == null)
            {
                string hashedPassword = BC.EnhancedHashPassword(
                    user.Password,13);
                user.Password = hashedPassword;
                _context.Users.Add(user);
                _context.SaveChanges();
                _logger.LogInformation($"Adding a new User successfully. userName: {user.UserName}" +
                    $" Id:{user.Id}");
                return user;
            }
            else
            {
                _logger.LogWarning($"" +
                   $"Preventing the creation of a user with an existing username {user.UserName}");
                return null;

            }

        }
        public User Login(User user)
        {

            User userFromDB = _context.Users.FirstOrDefault(u => u.UserName == user.UserName);

            if (userFromDB != null)
            {


                bool isPasswordValid = BC.EnhancedVerify(user.Password, userFromDB.Password);
                if (isPasswordValid)
                {
                    _logger.LogInformation($"User id: {userFromDB.Id} logged in successfully.");
                    
                    return userFromDB;


                }
            }


            _logger.LogWarning("Preventing login with wrong details password or userName ");
            return null;



        }
        public List<User> GetAllUsers() => _context.Users.ToList();


    }
}

