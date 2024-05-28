using BarberShopBL.Interfaces;
using BarberShopDB.EF.Models;
using Microsoft.AspNetCore.Mvc;

namespace BarberShopApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentBL _appointmentBL;
        public AppointmentController(IAppointmentBL appointmentDB)
        {
            _appointmentBL = appointmentDB;
        }


        // private readonly ILogger<UserController> _logger;


        [HttpPost]

        public IActionResult AddAppointment([FromBody] Appointment appointment)

        {
            try
            {


                return Ok(_appointmentBL.AddAppointment(appointment));

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An error occurred. The operation failed");
            }
        }
        [HttpPut("{id?}")]
        public IActionResult UpdateAppointment([FromRoute] int id,[FromBody] Appointment appointment)

        {
            try
            {


                return Ok(_appointmentBL.UpdateAppointment(id,appointment));

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An error occurred. The operation failed");
            }
        }
        [HttpDelete("{id?}")]
        public IActionResult DeleteAppointment([FromRoute] int id)

        {
            try
            {

                _appointmentBL.DeleteAppointment(id);
                return Ok("Deleted successfully");

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An error occurred. The operation failed");
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
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An error occurred. The operation failed");
            }
        }
    }
    
}
