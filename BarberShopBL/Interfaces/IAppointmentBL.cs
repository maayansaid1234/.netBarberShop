using BarberShopDB.EF.Models;
using BarberShopEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShopBL.Interfaces
{
    public interface IAppointmentBL
    {
        List<AppointmentFullDetails> GetAllAppointments();
        Appointment AddAppointment(AppointmentAddAndUpdateDTO appointment);
        Appointment UpdateAppointment( int id, AppointmentAddAndUpdateDTO appointment);
       Appointment  DeleteAppointment (int id);
    }
}
