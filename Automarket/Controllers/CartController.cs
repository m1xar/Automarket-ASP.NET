using Microsoft.AspNetCore.Mvc;
using Automarket.Service.Interfaces;
using Automarket.Domain.Models;

namespace Automarket.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IUserService _userService;
        private readonly ICarService _carService;


        public CartController(ICartService cartService, IUserService userService, ICarService carService)
        {
            _cartService = cartService;
            _userService = userService;
            _carService = carService;
        }
        
        [HttpPost]
        public async Task<IActionResult> Add(int id)
        {
            if(User.Identity.Name != null)
            {
                var user = await _userService.GetUserByEmail(User.Identity.Name);
                var response = await _cartService.Create(id, user.Data.Id);
                if(response.StatusCode == Domain.Enum.StatusCode.OK) 
                {
                    return RedirectToAction("Index");
                }
                RedirectToAction("Error");
            }
            return RedirectToAction("AccessDenied", "Home");
            
        }

        public async Task<IActionResult> Index()
        {
            if (User.Identity.Name != null)
            {
                var user = await _userService.GetUserByEmail(User.Identity.Name);
                var items = await _cartService.GetItemsByUserId(user.Data.Id);
                var cars = new Dictionary<int, Car>();
                    foreach (var item in items.Data)
                    {
                        var car = await _carService.GetCarById(item.CarId);
                        cars.Add(item.Id, car.Data);
                    }

                return View(cars);
            }
            return RedirectToAction("AccessDenied", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> Remove(int id)
        {
            await _cartService.Delete(id);
            return RedirectToAction("Index");

        }
    }
}
