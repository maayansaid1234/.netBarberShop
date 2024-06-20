using BarberShopApi.Attributes;
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
               
               BaseResponse<Appointment> baseResponse= _appointmentBL.AddAppointment(appointment);

                return StatusCode(baseResponse.StatusCode, baseResponse.IsSuccess ? baseResponse.Data : baseResponse.ErrorMessage);



            }
            catch (Exception ex)
            {
                _logger.LogError($"Error on AddAppointment, Message: {ex.Message}," +
                    $" InnerException: {ex.InnerException}, StackTrace: {ex.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPut("{id?}")]
        [TypeFilter(typeof(ValidateOwnerAttribute))]
        public IActionResult UpdateAppointment([FromRoute] int id,[FromBody] AppointmentAddAndUpdateDTO appointment)

        {
            try
            {

              
                BaseResponse<Appointment> baseResponse = _appointmentBL.UpdateAppointment(id,
                    appointment);
                 
                  
                  return StatusCode(baseResponse.StatusCode, 
                      baseResponse.IsSuccess?baseResponse.Data: baseResponse.ErrorMessage);
                    
                  

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error on UpdateAppointment, Message: {ex.Message}," +
                    $" InnerException: {ex.InnerException}, StackTrace: {ex.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpDelete("{id?}")]
        [TypeFilter(typeof(ValidateOwnerAttribute))]
        public IActionResult DeleteAppointment([FromRoute] int id)

        {
            try {

                BaseResponse<Appointment> baseResponse = _appointmentBL.DeleteAppointment(id);
                
              
                return StatusCode(baseResponse.StatusCode);
             

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
                BaseResponse < List<AppointmentFullDetails> >  baseResponse= _appointmentBL.GetAllAppointments();
                return StatusCode(baseResponse.StatusCode, baseResponse.Data);
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
