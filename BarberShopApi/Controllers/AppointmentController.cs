using BarberShopBL.Interfaces;
using BarberShopDB.EF.Models;
using BarberShopEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BarberShopApi.Controllers
{
    [Authorize]
    
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentBL _appointmentBL;
        private readonly ILogger<AppointmentController> _logger;
        public AppointmentController(IAppointmentBL appointmentDB,
            ILogger<AppointmentController> logger)
        {
            _appointmentBL = appointmentDB;
            _logger=logger;
        }


        [HttpPost]
        public IActionResult AddAppointment([FromBody]
        AppointmentAddAndUpdateDTO appointment)

        {
            try
            {
               
                Appointment a= _appointmentBL.AddAppointment(appointment);
                if(a == null)
                {
                    return StatusCode(409, "There is an appointmnet already at this time");

                }

                return Ok(a);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error on AddAppointment, Message: {ex.Message}," +
                    $" InnerException: {ex.InnerException}, StackTrace: {ex.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPut("{id?}")]
        public IActionResult UpdateAppointment([FromRoute] int id,[FromBody] AppointmentAddAndUpdateDTO appointment)

        {
            try
            {

              
                Appointment appointment1 = _appointmentBL.UpdateAppointment(id,
                    appointment);
                    if(appointment1 == null)
                    {
                    return StatusCode(404, "Appointment doesn't exist");
                    }
                    return Ok(appointment1);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error on UpdateAppointment, Message: {ex.Message}," +
                    $" InnerException: {ex.InnerException}, StackTrace: {ex.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpDelete("{id?}")]
        public IActionResult DeleteAppointment([FromRoute] int id)

        {
            try { 
            
                Appointment appointment= _appointmentBL.DeleteAppointment(id);
                
                if (appointment == null)
                {
                    return StatusCode(404, "Appointment doesn't exist");
                }
                return Ok(appointment);
             

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error on DeleteAppointment, Message: {ex.Message}," +
                    $" InnerException: {ex.InnerException}, StackTrace: {ex.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        
        public IActionResult GetAllAppointments()
        {
            try
            {
                return Ok(_appointmentBL.GetAllAppointments());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error on GetAllAppointments, Message: {ex.Message}," +
                    $" InnerException: {ex.InnerException}, StackTrace: {ex.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
    
}
