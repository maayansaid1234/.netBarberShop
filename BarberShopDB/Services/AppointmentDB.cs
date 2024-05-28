using BarberShopDB.EF.Contexts;
using BarberShopDB.EF.Models;
using BarberShopDB.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShopDB.Services
{
    public class AppointmentDB:IAppointmentDB
    {
        private readonly BarberShopContext _context;
        public AppointmentDB(BarberShopContext context)
        {
            _context = context;
        }
        public List<Appointment> GetAllAppointments()
        {
            return _context.Appointments.ToList();
        }
         public Appointment AddAppointment(Appointment appointment)
        {
             _context.Appointments.Add(appointment);
            _context.SaveChanges(); 
            return appointment;
        }
        public Appointment UpdateAppointment(int id, Appointment appointment)
        {
            Appointment appointmentFromDB = _context.Appointments.FirstOrDefault( 
                appointment=> appointment.Id == id);
            // enable to set only AppointmentTime
            appointmentFromDB.AppointmentTime=appointment.AppointmentTime;
            _context.SaveChanges();
            return appointmentFromDB;
        }
        public Appointment DeleteAppointment(int id)
        {
            Appointment appointmentFromDB = _context.Appointments.FirstOrDefault(
                appointment => appointment.Id == id);
            // enable to set only AppointmentTime
           _context.Appointments.Remove(appointmentFromDB);
            _context.SaveChanges();
            return appointmentFromDB;
        }
    }
}
