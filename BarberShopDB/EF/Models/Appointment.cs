using System;
using System.Collections.Generic;

namespace BarberShopDB.EF.Models;

public partial class Appointment
{
    public int Id { get; set; }

    public DateTime? SchedulingDate { get; set; }

    public DateTime? AppointmentTime { get; set; }

    public int? UserId { get; set; }

    public virtual User? User { get; set; }
}
