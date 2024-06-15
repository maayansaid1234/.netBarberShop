using BarberShopBL.Interfaces;
using BarberShopBL.Services;
using BarberShopDB.EF.Models;
using BarberShopEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarberShopApi.Controllers
{
    
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly IUserBL _userBL ;
        private readonly ILogger<UserController> _logger;
        public UserController(IUserBL userBL, ILogger<UserController> logger)
        {
            _userBL=userBL;
            _logger= logger;
        }
       

        


        [HttpPost]
       
        public  IActionResult AddUser( [FromBody] UserAddDTO user)

        {
            try
            {
               
                User user1 = _userBL.AddUser(user);
                if(user1 == null)
                {
                    return StatusCode(409,"user already exists.choose a different user name");

                }
                return Ok(user1);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error on AddUser, Message: {ex.Message}," +
                    $" InnerException: {ex.InnerException}, StackTrace: {ex.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError);
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
                _logger.LogError($"Error on GetAllUsers, Message: {ex.Message}," +
                    $" InnerException: {ex.InnerException}, StackTrace: {ex.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public IActionResult Login(UserLoginDTO user)

        {
            try
            {

                User newUser = _userBL.Login(user);
                if (newUser != null)
                    return Ok(newUser);
                return NotFound("user name or password are wrong");

            }
            catch (Exception ex)
            {

                _logger.LogError($"Error on Login, Message: {ex.Message}," +
                    $" InnerException: {ex.InnerException}, StackTrace: {ex.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError);

            }
        }

        [HttpGet]
            public IActionResult Logout()

            {
                try
                {
                 _userBL.Logout();
                //Response.Cookies.Delete(CookiesKeys.AccessToken);
                return Ok();
                }
                catch (Exception ex)
                {

                    _logger.LogError($"Error on Logout, Message: {ex.Message}," +
                        $" InnerException: {ex.InnerException}, StackTrace: {ex.StackTrace}");
                    return StatusCode(StatusCodes.Status500InternalServerError);

                }

            }


        [HttpGet]
    public IActionResult GetUserName()

    {
        try
        {
                


            return Ok(_userBL.GetUserName());

        }
        catch (Exception ex)
        {
            _logger.LogError($"Error on GetUserName, Message: {ex.Message}," +
                $" InnerException: {ex.InnerException}, StackTrace: {ex.StackTrace}");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}

}
