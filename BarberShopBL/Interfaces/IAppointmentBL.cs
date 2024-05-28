using BarberShopDB.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShopBL.Interfaces
{
    public interface IAppointmentBL
    {
        List<Appointment> GetAllAppointments();
        Appointment AddAppointment(Appointment appointment);
        Appointment UpdateAppointment( int id,Appointment appointment);
       Appointment  DeleteAppointment (int id);
    }
}
