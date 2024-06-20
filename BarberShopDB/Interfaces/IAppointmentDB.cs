using BarberShopDB.EF.Models;
using BarberShopDB.Services;
using BarberShopEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShopDB.Interfaces
{
    public interface IAppointmentDB
    {
        BaseResponse<List<AppointmentFullDetails>> GetAllAppointments();
        BaseResponse<Appointment> AddAppointment(Appointment appointment, int userId);
        BaseResponse<Appointment> UpdateAppointment(int id,Appointment appointment);
        BaseResponse<Appointment > DeleteAppointment(int id);
        Appointment GetAppointmentById(int id);
    }
}
