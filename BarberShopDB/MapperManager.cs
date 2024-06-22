using AutoMapper;
using BarberShopDB.EF.Models;
using BarberShopEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShopDB
{
    public class MapperManager:Profile
    {
        public MapperManager()
        {
            CreateMap<UserLoginDTO, User>();
            CreateMap<UserAddDTO, User>();
            CreateMap<AppointmentAddAndUpdateDTO, Appointment>();
            CreateMap<User, UserInfo>();
        }
    }
}
