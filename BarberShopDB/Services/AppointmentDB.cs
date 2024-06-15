using BarberShopDB.EF.Contexts;
using BarberShopDB.EF.Models;
using BarberShopDB.Interfaces;
using BarberShopEntities;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<AppointmentDB> _logger;
        public AppointmentDB(BarberShopContext context,ILogger<AppointmentDB> logger)
        {
            _context = context;
            _logger = logger;
        }
        public List<AppointmentFullDetails> GetAllAppointments()
        {
            //var appointmentsWithUserNames = _context.Appointments
            //    .Join(_context.Users,
            //          appointment => appointment.UserId,
            //          user => user.Id,
            //          (appointment, user) => new AppointmentFullDetails
            //          (

            //                  appointment.Id,
            //                 (DateTime)appointment.SchedulingDate,
            //                   (DateTime)appointment.AppointmentTime ,
            //                (int)appointment.UserId,
            //                  user.UserName,
            //                  //(int)appointment.HaircutId


            //          ) 
            //          )
            //        .ToList();

            var appointmentsWithFullDetails = _context.Appointments
    .Join(_context.Users,
          appointment => appointment.UserId,
          user => user.Id,
          (appointment, user) => new { appointment, user })
    .Join(_context.Haircuts,
          combined => combined.appointment.HaircutId,
          haircut => haircut.Id,
          (combined, haircut) => new AppointmentFullDetails
          (
              combined.appointment.Id,
              (DateTime)combined.appointment.SchedulingDate,
              combined.appointment.AppointmentTime,
              (int)combined.appointment.UserId,
              combined.user.UserName,
              (int)combined.appointment.HaircutId,
              haircut.HaircutType // Assuming HaircutName is a property of the Haircut table
          ))
       .ToList();

            return appointmentsWithFullDetails;

          
            }

        
        public Appointment AddAppointment(Appointment appointment,int userId)
        {

            //User userFromDB = _context.Users.FirstOrDefault(user => user.Id == id);
            // appointment.User = userFromDB;
            Appointment appointmentFromDB = _context.Appointments.FirstOrDefault(a =>
            a.AppointmentTime == appointment.AppointmentTime );

            if(appointmentFromDB != null)
            {
                _logger.LogWarning("Preventing the scheduling of an appointment at " +
                    "a time that is already booked.appointment time :"+ appointment.AppointmentTime); 
                    
                   
                return null;
            }
        
            appointment.UserId = userId;
             _context.Appointments.Add(appointment);
            _context.SaveChanges();

            _logger.LogInformation($"Adding new appointment at :{appointment.SchedulingDate}.appointmentTime: {appointment.AppointmentTime} by" +
                $" user id:{appointment.UserId}");

              

            return appointment;
        }
        public Appointment UpdateAppointment(int id, Appointment appointment, int userId)
        {
            Appointment appointmentFromDB = _context.Appointments.FirstOrDefault( 
                appointment=> appointment.Id == id);
         
            if(appointmentFromDB == null) {
               _logger.LogWarning($"Preventing the update of a non-existent appointment");

                return null;
            }
          Appointment a= _context.Appointments.FirstOrDefault(
                app => app.AppointmentTime == appointment.AppointmentTime);
            if(a != null) {
                _logger.LogWarning("Preventing the scheduling of an appointment at " +
                   "a time that is already booked.appointment time :" + appointment.AppointmentTime);
                return null;
            }
            if (appointmentFromDB.UserId != userId)
            {
                _logger.LogWarning($"Preventing the update of an appointment by another owner");
                return null;
            }
            appointmentFromDB.AppointmentTime = appointment.AppointmentTime;
            appointmentFromDB.HaircutId = appointment.HaircutId;

            _context.SaveChanges();
            _logger.LogInformation($"Updating  appointment code:{appointment.Id}" +
             $"from {appointmentFromDB.AppointmentTime} to : {appointment.AppointmentTime}");
           
           
            return appointmentFromDB;
        }
        public Appointment DeleteAppointment(int id,int userId)
        {
            Appointment appointmentFromDB = _context.Appointments.FirstOrDefault(
                appointment => appointment.Id == id);


            if (appointmentFromDB == null)
            {
                _logger.LogWarning($"Preventing the delete of a non-existent appointment");
                return null;
            }
            if(appointmentFromDB.UserId != userId) {
                _logger.LogWarning($"Preventing the delete of an appointment by another owner");
                return null;
            }
            _context.Appointments.Remove(appointmentFromDB);
            _logger.LogInformation($"Deleting appointment code: {id}");

           _context.SaveChanges();
            return appointmentFromDB;
        }
    }
}
