using BarberShopDB.EF.Contexts;
using BarberShopDB.EF.Models;
using BarberShopDB.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShopDB.Services
{
    public class UserDB:IUserDB
    {
        private readonly BarberShopContext _context;
        public UserDB(BarberShopContext context)
        {
            _context = context;
        }

        public User AddUser(User user)
        {
          User userFromDB = _context.Users.FirstOrDefault(u =>
          u.Password == user.Password &&
         u.UserName == user.UserName);
            if (userFromDB == null) 
            {  
            _context.Users.Add(user);
            _context.SaveChanges();
            }
            else
            {
                user = null;

            }
            return user;
        }
        public User LogIn (User user)
        {
            // if exists
            // return user with all his details
            //else
            //  return : not found - 404,user doesn't exist

           User userFromDB= _context.Users.FirstOrDefault(u=>u.Password==user.Password&&
           u.UserName == user.UserName);
            
            return userFromDB;
           
           
        }
        public List<User> GetAllUsers() => _context.Users.ToList();

        public List<Appointment> GetAllAppointments()
        {
            return _context.Appointments.ToList();
        }
    }
}

