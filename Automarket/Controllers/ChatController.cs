using Microsoft.AspNetCore.Mvc;
using Automarket.Service.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace Automarket.Controllers
{
    public class ChatController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IUserService _userService;
        private readonly ICarService _carService;


        public ChatController(ICartService cartService, IUserService userService, ICarService carService)
        {
            _cartService = cartService;
            _userService = userService;
            _carService = carService;
        }

        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("Admin") || User.IsInRole("Moderator")) { return  RedirectToAction("Admin"); }
            if (User.IsInRole("Customer")) {  return View(); }
           
            return RedirectToAction("Login", "Account");
        }

        public async Task<IActionResult> Admin()
        {
            if (User.IsInRole("Admin")) { return View(); }
            return RedirectToAction("AccessDenied", "Home");
        }
    }
}
