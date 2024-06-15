using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShopEntities
{
    public class AppointmentFullDetails
    {
       

        public int Id { get; set; }

        public DateTime SchedulingDate { get; set; }

        public DateTime AppointmentTime { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; }

        public int HaircutId { get; set; }

        public string HaircutType { get; set; }

        public AppointmentFullDetails(int id, DateTime schedulingDate,
            DateTime appointmentTime, int userId, 
            string userName,int haircutId,string haircutType)
        {
            Id = id;
            SchedulingDate = schedulingDate;
            AppointmentTime = appointmentTime;
            UserId = userId;
            UserName = userName;
            HaircutId = haircutId;
            HaircutType = haircutType;
        }
    }
}
