using BarberShopDB.EF.Models;
using BarberShopEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShopDB.Interfaces
{
    public interface IHaircutDB
    {
        public List<Haircut> GetAllHaircuts();
        public BaseResponse<Haircut> AddHaircut(Haircut haircut);
    }
}
