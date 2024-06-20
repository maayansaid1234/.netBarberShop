using AutoMapper;
using BarberShopBL.Interfaces;
using BarberShopDB.EF.Models;
using BarberShopDB.Interfaces;
using BarberShopEntities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BarberShopBL.Services
{
    public class AppointmentBL:IAppointmentBL
    {
        private readonly IAppointmentDB _appointmentDB;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AppointmentBL(IAppointmentDB appointmentDB,IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _appointmentDB = appointmentDB;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor; 
        }

        public BaseResponse<List<AppointmentFullDetails>> GetAllAppointments()
        {
            return _appointmentDB.GetAllAppointments();
        }
        public BaseResponse<Appointment>  AddAppointment(AppointmentAddAndUpdateDTO appointment)
        {
           Appointment appointmentMapped= _mapper.Map<Appointment>(appointment);
            int userId= GetUserIdByUserToken();
            return _appointmentDB.AddAppointment(appointmentMapped, userId);
        }
        public BaseResponse<Appointment> UpdateAppointment(int id,AppointmentAddAndUpdateDTO appointment)
        {
            Appointment appointmentMapped = _mapper.Map<Appointment>(appointment);
       
            return _appointmentDB.UpdateAppointment( id ,appointmentMapped);
        }
        public BaseResponse<Appointment> DeleteAppointment(int id)
        {
           
            return _appointmentDB.DeleteAppointment(id);
        }
        public Appointment GetAppointmentById(int id)
        {
            return _appointmentDB.GetAppointmentById( id);
        }
        public int GetUserIdByUserToken()
        {
            var token = _httpContextAccessor.HttpContext.Request.Cookies[CookiesKeys.AccessToken];
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadJwtToken(token);
            string userIdString = jsonToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            int userId = Convert.ToInt32(userIdString);
            return userId;
        }
    }
}
