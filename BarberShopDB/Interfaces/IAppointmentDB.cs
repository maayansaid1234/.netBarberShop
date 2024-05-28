using BarberShopDB.EF.Models;
using BarberShopDB.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShopDB.Interfaces
{
    public interface IAppointmentDB
    {
        List<Appointment> GetAllAppointments();
        Appointment AddAppointment(Appointment appointment);
        Appointment UpdateAppointment(int id,Appointment appointment);
        Appointment DeleteAppointment(int id );
    }
}
