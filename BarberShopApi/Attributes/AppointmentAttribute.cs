using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Azure.Core;
using BarberShopEntities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BarberShopDB.Interfaces;
using BarberShopBL.Interfaces;
using BarberShopDB.EF.Models;
using BarberShopDB.Services;

namespace BarberShopApi.Attributes
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]

    public class ValidateOwnerAttribute : ActionFilterAttribute
    {
        
        private readonly IAppointmentBL _appointmentBL;
      
        public ValidateOwnerAttribute(
             IAppointmentBL appointmentBL)
            
        {
            
            _appointmentBL = appointmentBL;
            
        }
        public override async Task OnActionExecutionAsync(ActionExecutingContext _context, ActionExecutionDelegate _next)
        {
           
           int userId = _appointmentBL.GetUserIdByUserToken();
           if ( _context.ActionArguments.TryGetValue("Id", out var appointmentId))
            {
                Appointment ap = _appointmentBL.GetAppointmentById(Convert.ToInt32(appointmentId));
                if ( ap == null)
                {
                  _context.Result = new ObjectResult("Not Found!")
                   {
                        StatusCode = 404
                   };
                    return;
                }
                   
                if (userId != ap.UserId)
                {
            
                    _context.Result = new ObjectResult("You are not allowed to do this action!")
                    {
                        StatusCode = 403
                    };
                    return;
                }
               else
               {
                  await _next();
               }
            }
           
        }

   
    }
}

