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
               
                BaseResponse<User> baseResponse = _userBL.AddUser(user);
              
                return StatusCode(baseResponse.StatusCode, baseResponse.ErrorMessage == null ? baseResponse.Data : baseResponse.ErrorMessage);


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

                BaseResponse<List<User>> baseResponse = _userBL.GetAllUsers();
                return StatusCode(baseResponse.StatusCode,baseResponse.Data);

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

               BaseResponse<User> baseResponse = _userBL.Login(user);
              
                return StatusCode(baseResponse.StatusCode, baseResponse.ErrorMessage == null ? baseResponse.Data : baseResponse.ErrorMessage);

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
    public IActionResult GetUserNameFromSession()

    {
        try
        {               
            return Ok(_userBL.GetUserNameFromSession());

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
