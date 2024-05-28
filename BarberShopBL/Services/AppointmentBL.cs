using BarberShopBL.Interfaces;
using BarberShopDB.EF.Models;
using BarberShopDB.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShopBL.Services
{
    public class AppointmentBL:IAppointmentBL
    {
        private readonly IAppointmentDB _appointmentDB;
        public AppointmentBL(IAppointmentDB appointmentDB)
        {
            _appointmentDB = appointmentDB;
        }

        public List<Appointment> GetAllAppointments()
        {
            return _appointmentDB.GetAllAppointments();
        }
        public Appointment  AddAppointment(Appointment appointment)
        {
            return _appointmentDB.AddAppointment(appointment);
        }
        public Appointment UpdateAppointment(int id,Appointment appointment)
        {
            return _appointmentDB.UpdateAppointment( id ,appointment);
        }
        public Appointment DeleteAppointment(int id)
        {
            return _appointmentDB.DeleteAppointment(id);
        }
    }
}
