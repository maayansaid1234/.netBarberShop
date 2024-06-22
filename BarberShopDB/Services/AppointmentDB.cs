using BarberShopDB.EF.Contexts;
using BarberShopDB.EF.Models;
using BarberShopDB.Interfaces;
using BarberShopEntities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BarberShopDB.Services
{
    public class AppointmentDB : IAppointmentDB
    {
        private readonly BarberShopContext _context;
        private readonly ILogger<AppointmentDB> _logger;

        public AppointmentDB(BarberShopContext context, ILogger<AppointmentDB> logger)
        {
            _context = context;
            _logger = logger;
        }

        public BaseResponse<List<AppointmentFullDetails>> GetAllAppointments()
        {
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
                          haircut.HaircutType
                      )).ToList();

            return new BaseResponse<List<AppointmentFullDetails>>()
            {
                StatusCode = 200, 
                IsSuccess = true,
                Data = appointmentsWithFullDetails
            };
        }

        public BaseResponse<Appointment> AddAppointment(Appointment appointment, int userId)
        {


                Appointment appointmentFromDB = _context.Appointments.FirstOrDefault(a =>
                a.AppointmentTime == appointment.AppointmentTime);


                if(appointmentFromDB != null)
                {

              _logger.LogWarning("Preventing the scheduling of an appointment at " +
                        "a time that is already booked.appointment time :"+ appointment.AppointmentTime); 
                   return new BaseResponse<Appointment>()
                   {
                     StatusCode = 409,
                     IsSuccess=false,
                     ErrorMessage = "Threre is already an appointment at this time"
                   };
    
                }

               appointment.UserId = userId;
                 _context.Appointments.Add(appointment);
                _context.SaveChanges();

                _logger.LogInformation($"Adding new appointment at :{appointment.SchedulingDate}.appointmentTime: {appointment.AppointmentTime} by" +
                    $" user id:{appointment.UserId}");



             return new BaseResponse<Appointment >()
                   {
                           StatusCode = 201,
                           IsSuccess = true,
                           Data=appointment
                    }; 
             }
public BaseResponse<Appointment> UpdateAppointment(int id, Appointment appointment)
            {
    Appointment appointmentFromDB = _context.Appointments.FirstOrDefault(
        appointment => appointment.Id == id);


    Appointment a = _context.Appointments.FirstOrDefault(
          app => app.AppointmentTime == appointment.AppointmentTime&&app.Id!=id);
    if (a != null)
    {
        _logger.LogWarning("Preventing the scheduling of an appointment at " +
                         "a time that is already booked.appointment time :" + appointment.AppointmentTime);
        return new BaseResponse<Appointment>()
        {
            StatusCode = 409,
            IsSuccess = false,
            ErrorMessage = "Threre is already appointment at this time"
        };

    }

    appointmentFromDB.AppointmentTime = appointment.AppointmentTime;
    appointmentFromDB.HaircutId = appointment.HaircutId;

    _context.SaveChanges();
    _logger.LogInformation($"Updating  appointment code:{appointment.Id}" +
     $"from {appointmentFromDB.AppointmentTime} to : {appointment.AppointmentTime}");


    return new BaseResponse<Appointment>()
    {
        StatusCode = 200,
        IsSuccess=true,
        Data = appointmentFromDB
     }; 
             
}
public BaseResponse<Appointment> DeleteAppointment(int id)
{
                Appointment appointmentFromDB = _context.Appointments.FirstOrDefault(
                    appointment => appointment.Id == id);

                   _context.Appointments.Remove(appointmentFromDB);
              _logger.LogInformation($"Deleting appointment code: {id}");

                   _context.SaveChanges();
        return new BaseResponse<Appointment>() 
        {
            StatusCode =204
         };
 }

public Appointment GetAppointmentById(int id)
{
    return _context.Appointments.FirstOrDefault(ap => ap.Id == id);
}

}

}