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
        List<AppointmentFullDetails> GetAllAppointments();
        Appointment AddAppointment(Appointment appointment, int userId);
        Appointment UpdateAppointment(int id,Appointment appointment, int userId);
        Appointment DeleteAppointment(int id, int userId);
    }
}
