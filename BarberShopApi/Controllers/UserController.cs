using BarberShopBL.Interfaces;
using BarberShopDB.EF.Models;
using Microsoft.AspNetCore.Mvc;

namespace BarberShopApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private  readonly IUserBL _userBL ;
        public UserController(IUserBL userBL)
        {
            _userBL=userBL;
        }
       

       // private readonly ILogger<UserController> _logger;


        [HttpPost]
       
        public  IActionResult AddUser( [FromBody] User user)

        {
            try
            {
               
                User user1 = _userBL.AddUser(user);
                return Ok(user1);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An error occurred. The operation failed");
            }
        }
        [HttpGet]
        public IActionResult GetAllUsers()

        {
            try
            {

               
                return Ok(_userBL.GetAllUsers());

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An error occurred. The operation failed");
            }
        }
        [HttpPost]
        public IActionResult LogIn(User user)

        {
            try
            {

                User newUser = _userBL.LogIn(user);
                if (newUser != null) 
                return Ok(_userBL.LogIn(user));
                return NotFound("There is/are mistake/s in user's details");

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An error occurred. The operation failed");
            }
        }

    }
}
