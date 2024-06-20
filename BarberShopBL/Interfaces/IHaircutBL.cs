using BarberShopDB.EF.Models;
using BarberShopEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShopBL.Interfaces
{
    public interface IHaircutBL
    {
        public List<Haircut> GetAllHaircuts();
        public BaseResponse<Haircut> AddHaircut(Haircut haircut);
    }
}
