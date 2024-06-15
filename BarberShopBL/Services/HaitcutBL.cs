using BarberShopBL.Interfaces;
using BarberShopDB.EF.Models;
using BarberShopDB.Interfaces;
using BarberShopEntities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShopBL.Services
{
    public class HaitcutBL : IHaircutBL
    {
        private readonly IHaircutDB _haircutDB;
        
        public HaitcutBL(IHaircutDB haircutDB)
        {
            _haircutDB = haircutDB;
         

        }
        public List<Haircut> GetAllHaircuts()
        {
            return _haircutDB.GetAllHaircuts();
        }

        public Haircut AddHaircut(Haircut haircut)
        {
            return _haircutDB.AddHaircut(haircut);
        }
    }
}
