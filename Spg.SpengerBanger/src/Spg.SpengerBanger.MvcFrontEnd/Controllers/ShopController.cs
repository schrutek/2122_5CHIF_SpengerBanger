using Microsoft.AspNetCore.Mvc;
using Spg.SpengerBanger.Services.ShopService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spg.SpengerBanger.MvcFrontEnd.Controllers
{
    public class ShopController : Controller
    {
        private IShopService _shopService;

        public ShopController(IShopService shopService)
        {
            _shopService = shopService;
        }

        public IActionResult Index()
        {
            var model = _shopService.ListAllShops();
            return View(model);
        }
    }
}
