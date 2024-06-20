using AutoMapper;
using BarberShopBL.Interfaces;
using BarberShopDB.EF.Models;
using BarberShopDB.Interfaces;
using BarberShopDB.Services;
using BarberShopEntities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BarberShopBL.Services
{
    public class UserBL : IUserBL
    {
        private readonly IUserDB _userDB;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AppSettings _appSettings;
        private readonly IMapper _mapper;

        public UserBL(IUserDB userDB, IHttpContextAccessor httpContextAccessor,
            IOptions<AppSettings> options,IMapper mapper)
        {
            _userDB = userDB;
            _httpContextAccessor = httpContextAccessor;
            _appSettings = options.Value;
            _mapper= mapper;

        }

        public BaseResponse<List<User>> GetAllUsers()
        {
            return _userDB.GetAllUsers();
        }
        public BaseResponse<User> AddUser(UserAddDTO user)
        {
            User userMapped = _mapper.Map<User>(user);
           BaseResponse<User> baseResponse = _userDB.AddUser(userMapped);
            if (baseResponse.IsSuccess  )
            {
                CreateUserToken(baseResponse.Data.Id);
                byte[] bytearray = Encoding.ASCII.GetBytes(baseResponse.Data.UserName);
                _httpContextAccessor.HttpContext.Session.Set(SessionKeys.UserName, bytearray);
            } 
           
            return baseResponse;
        }
        public BaseResponse<User> Login(UserLoginDTO user)
        {
            User userMapped = _mapper.Map<User>(user);
            BaseResponse < User> baseResponse = _userDB.Login(userMapped);
            if (baseResponse.IsSuccess)
            {
                CreateUserToken(baseResponse.Data.Id);
                byte[] bytearray = Encoding.ASCII.GetBytes(baseResponse.Data.UserName);
                _httpContextAccessor.HttpContext.Session.Set(SessionKeys.UserName, bytearray);
            }

            return baseResponse;
        }
        public void Logout()
        {

            _httpContextAccessor.HttpContext.Response.Cookies.Delete(CookiesKeys.AccessToken);
            _httpContextAccessor.HttpContext.Session.Remove(SessionKeys.UserName);
         
        }

        public string GetUserNameFromSession()
        {
            byte[] byteArray;
            _httpContextAccessor.HttpContext.Session.TryGetValue(SessionKeys.UserName, out byteArray);
            string userName = Encoding.ASCII.GetString(byteArray);
            return userName;
        }


        private void CreateUserToken(int id)
        {
            string newAccessToken = GenerateAccessToken(id);
            CookieOptions cookieTokenOptions = new CookieOptions()
            {
                HttpOnly = true,
                Secure = true,
                //SameSite = SameSiteMode.Strict,
                SameSite = SameSiteMode.None,
                Expires = DateTime.Now.AddMinutes(_appSettings.Jwt.ExpireMinutes)
            };
            _httpContextAccessor.HttpContext.Response.Cookies.Append(CookiesKeys.AccessToken, newAccessToken, cookieTokenOptions);
        }

        private string GenerateAccessToken(int id)
        {
            var jwtSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes
                (_appSettings.Jwt.SecretKey));
            var credentials = new SigningCredentials(jwtSecurityKey, 
                SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.Name,Convert.ToString(id))
            };
            var token = new JwtSecurityToken(
            _appSettings.Jwt.Issuer,
                _appSettings.Jwt.Audience,
                claims,
                expires: DateTime.Now.AddMinutes(_appSettings.Jwt.ExpireMinutes),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
