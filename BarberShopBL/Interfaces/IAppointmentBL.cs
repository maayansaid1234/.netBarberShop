using BarberShopDB.EF.Models;
using BarberShopEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShopBL.Interfaces
{
    public interface IAppointmentBL
    {
       BaseResponse< List<AppointmentFullDetails>> GetAllAppointments();
        BaseResponse<Appointment> AddAppointment(AppointmentAddAndUpdateDTO appointment);
           BaseResponse<Appointment> UpdateAppointment( int id, AppointmentAddAndUpdateDTO appointment);
          BaseResponse<Appointment>  DeleteAppointment (int id);
         Appointment GetAppointmentById(int id);
        int GetUserIdByUserToken();
    }
}
