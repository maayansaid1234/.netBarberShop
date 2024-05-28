
using BarberShopBL.Interfaces;
using BarberShopBL.Services;
using BarberShopDB.Interfaces;
using BarberShopDB.Services;
using BarberShopEntities;
using Microsoft.EntityFrameworkCore;
using BarberShopDB.EF.Contexts;


namespace BarberShopApi
{
    public class Program
    {
       
            public static void Main(string[] args)
            {
                var builder = WebApplication.CreateBuilder(args);

                builder.Services.Configure<AppSettings>(builder.Configuration);
                AppSettings appSettings = builder.Configuration.Get<AppSettings>();

                // Add services to the container.

                builder.Services.AddScoped<IUserBL, UserBL>();
                builder.Services.AddScoped<IUserDB, UserDB>();

                builder.Services.AddScoped<IAppointmentBL, AppointmentBL>();
                builder.Services.AddScoped<IAppointmentDB, AppointmentDB>();

                builder.Services.AddDbContext<BarberShopContext>(options =>
                {
                    options.UseSqlServer(appSettings.ConnectionStrings.BarberShop);
                });

                // Add CORS configuration
                builder.Services.AddCors(options =>
                {
                    options.AddPolicy("corsPolicy",
                        builder =>
                        {
                            builder.AllowAnyMethod()
                                .AllowAnyHeader().WithOrigins("http://localhost:3000").
                                AllowCredentials();
                              
                        });
                });

                // Add services to the container.

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

                app.UseHttpsRedirection();

                app.UseAuthorization();

                // Enable CORS
                app.UseCors("corsPolicy");

                app.MapControllers();

                app.Run();
            }

        }
    }

