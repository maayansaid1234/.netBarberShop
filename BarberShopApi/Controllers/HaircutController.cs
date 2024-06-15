using BarberShopBL.Interfaces;
using BarberShopDB.EF.Models;
using BarberShopEntities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace BarberShopApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class HaircutController : ControllerBase
    {
        private readonly IHaircutBL _haircutBL;
        private readonly ILogger<HaircutController> _logger;
        private readonly IMemoryCache _memoryCache;
        public HaircutController(IHaircutBL haircutBL,
             ILogger<HaircutController>logger, IMemoryCache memoryCache)
        {
            _logger = logger;
            _haircutBL = haircutBL;
            _memoryCache = memoryCache;
        }

        [HttpGet]
        public IActionResult GetAllHaircuts()
        {
            try
            {
                List<Haircut> haircuts=new ();
                if (!_memoryCache.TryGetValue(CacheKeys.Haircuts, out haircuts))
                {
                    haircuts = _haircutBL.GetAllHaircuts();
                    _memoryCache.Set(CacheKeys.Haircuts, haircuts
                        ,new MemoryCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1)
                    }) ;
                }

                return Ok(haircuts);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error on GetAllHaircuts, Message: {ex.Message}," +
                   $" InnerException: {ex.InnerException}, StackTrace: {ex.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
           
        }
        [HttpPost]
        public IActionResult AddHaircut(Haircut haircut)
        {
            try
            {
                return Ok(_haircutBL.AddHaircut(haircut));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error on Add Haircut, Message: {ex.Message}," +
                   $" InnerException: {ex.InnerException}, StackTrace: {ex.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

    }
}
