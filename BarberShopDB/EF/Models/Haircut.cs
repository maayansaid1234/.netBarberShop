using System;
using System.Collections.Generic;

namespace BarberShopDB.EF.Models;

public partial class Haircut
{
    public int Id { get; set; }

    public string? HaircutType { get; set; }

    public int? Price { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
