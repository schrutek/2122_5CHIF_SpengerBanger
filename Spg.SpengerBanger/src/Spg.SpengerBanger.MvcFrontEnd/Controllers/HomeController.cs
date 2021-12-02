using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Spg.SpengerBanger.Business.Domain.Interfaces;
using Spg.SpengerBanger.Business.Domain.Model;
using Spg.SpengerBanger.Business.Services.UserService;
using Spg.SpengerBanger.MvcFrontEnd.Models;
using Spg.SpengerBanger.MvcFrontEnd.Services;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Spg.SpengerBanger.MvcFrontEnd.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAuthService _httpAuthService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(
            IUserService userService,
            IAuthService httpAuthService, 
            ILogger<HomeController> logger
            )
        {
            _userService = userService;
            _httpAuthService = httpAuthService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<string> emails = _userService
                .ListTop50()
                .Select(u => u.EMail)
                .ToList();

            UserDto dto = new UserDto() { EMails = emails, SelectedEMail = emails[0] };

            ViewData["EMails"] = new SelectList(emails);
            
            return View(dto);
        }

        [HttpPost()]
        public async Task<IActionResult> Index(UserDto model)
        {
            if (ModelState.IsValid)
            {
                await _httpAuthService.Login(model.SelectedEMail);
                return RedirectToAction(nameof(Welcome), "Home");
            }
            return View();
        }

        public IActionResult Welcome()
        {
            string username = _httpAuthService.Username();

            return View(model: username);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
