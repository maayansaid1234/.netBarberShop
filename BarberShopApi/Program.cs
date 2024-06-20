
using BarberShopBL.Interfaces;
using BarberShopBL.Services;
using BarberShopDB.Interfaces;
using BarberShopDB.Services;
using BarberShopEntities;
using Microsoft.EntityFrameworkCore;
using BarberShopDB.EF.Contexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Builder;
using BarberShopDB;
using Microsoft.AspNetCore.DataProtection;


namespace BarberShopApi
{
    public class Program
    {

        public static void Main(string[] args)
        {
            var builder = WebApplication
             .CreateBuilder(args);


            builder.UseSerilog();

            builder.Services.Configure<AppSettings>(builder.Configuration);
            AppSettings appSettings = builder.Configuration.Get<AppSettings>();


            

            // Add services to the container.

            builder.Services.AddScoped<IUserBL, UserBL>();
            builder.Services.AddScoped<IUserDB, UserDB>();
            builder.Services.AddScoped<IHaircutBL,HaitcutBL>();
            builder.Services.AddScoped<IHaircutDB,HaircutDB>();
            builder.Services.AddScoped<IAppointmentBL, AppointmentBL>();
            builder.Services.AddScoped<IAppointmentDB, AppointmentDB>();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddHttpClient();
           // builder.Services.AddSession();
            builder.Services.AddSession(options =>
            {

                
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.SameSite = SameSiteMode.None;
                
            });
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddMemoryCache();
            builder.Services.AddAutoMapper(typeof(MapperManager));


            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
              .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = appSettings.Jwt.Issuer,
                    ValidAudience = appSettings.Jwt.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.Jwt.SecretKey)),
                    ClockSkew = TimeSpan.Zero
                };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies[CookiesKeys.AccessToken];
                        return Task.CompletedTask;
                    }
                };
            });

            builder.Services.AddCors(options =>
           {
               options.AddPolicy("CorsPolicy",
                      builder => builder.WithOrigins(appSettings.Origins[0])
                      .AllowAnyMethod()
                      .AllowAnyHeader()
                      .AllowCredentials()
                      );
           });

            builder.Services.AddDbContext<BarberShopContext>(options =>
            {
                options.UseSqlServer(appSettings.ConnectionStrings.BarberShop);
            });


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("CorsPolicy");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession();
            app.MapControllers();

            app.Run();
        }

    }
}

