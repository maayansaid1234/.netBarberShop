using Azure.Core;
using BarberShopDB.EF.Contexts;
using BarberShopDB.EF.Models;
using BarberShopDB.Interfaces;
using BarberShopEntities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShopDB.Services
{
    public class HaircutDB:IHaircutDB
    {
        private readonly BarberShopContext _context;
        private readonly ILogger<HaircutDB> _logger;
        public HaircutDB(BarberShopContext context,
            ILogger<HaircutDB> logger)
        {
            _context = context;
            _logger = logger;
        }
        public List<Haircut> GetAllHaircuts()
        {
            return _context.Haircuts.ToList();
        }

        public BaseResponse<Haircut> AddHaircut(Haircut haircut)
        {
            _context.Haircuts.Add(haircut);
            _context.SaveChanges();
            return new BaseResponse<Haircut>(){ StatusCode = 201,IsSuccess=true
                , Data = haircut };
        }
    }
}
