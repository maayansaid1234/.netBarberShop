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

        public List<AppointmentFullDetails> GetAllAppointments()
        {
            return _appointmentDB.GetAllAppointments();
        }
        public Appointment  AddAppointment(AppointmentAddAndUpdateDTO appointment)
        {
           Appointment appointmentMapped= _mapper.Map<Appointment>(appointment);
            int userId=getUserIdByUserToken();
            return _appointmentDB.AddAppointment(appointmentMapped, userId);
        }
        public Appointment UpdateAppointment(int id,AppointmentAddAndUpdateDTO appointment)
        {
            Appointment appointmentMapped = _mapper.Map<Appointment>(appointment);
            int userId = getUserIdByUserToken();
            return _appointmentDB.UpdateAppointment( id ,appointmentMapped,userId);
        }
        public Appointment DeleteAppointment(int id)
        {
            int userId = getUserIdByUserToken();
            return _appointmentDB.DeleteAppointment(id,userId);
        }
        private int getUserIdByUserToken()
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
