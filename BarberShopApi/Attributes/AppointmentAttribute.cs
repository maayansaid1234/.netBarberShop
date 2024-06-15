using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Azure.Core;
using BarberShopEntities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BarberShopApi.Attributes
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]

    public class AppointmentAttribute : ActionFilterAttribute
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AppointmentAttribute(IHttpContextAccessor httpContextAccessor) {
            _httpContextAccessor = httpContextAccessor;
        }
        public override async Task OnActionExecutionAsync(ActionExecutingContext _context,ActionExecutionDelegate _next)
        {
           int userId=getUserIdByUserToken();
            
           
                await _next();
      
           
                _context.Result = new BadRequestObjectResult("error!");
          
        }
    
    private int getUserIdByUserToken()
    {
        var token = _httpContextAccessor.HttpContext.Request.Cookies[CookiesKeys.AccessToken];
        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadJwtToken(token);
        string userIdString = jsonToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
        int userId = Convert.ToInt32(userIdString);
        return userId;
    }
}
}

