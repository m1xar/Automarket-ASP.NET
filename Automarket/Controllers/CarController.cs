using Automarket.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Automarket.Domain.Models;
using Automarket.Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using Microsoft.Extensions.Hosting.Internal;
using Automarket.Domain.Enum;


namespace Automarket.Controllers
{
    public class CarController : Controller
    {

        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }
        [HttpGet]
        public async Task<IActionResult> GetCars()
        {
            var response = await _carService.GetCars();
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data.ToList<Car>());
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetCarById(int id)
        {
            var response = await _carService.GetCarById(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return RedirectToAction("Error");
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (User.IsInRole("Admin"))
            {
                var response = await _carService.Delete(id);
                if (response.Data)
                {
                    return RedirectToAction("GetCars");
                }
                return RedirectToAction("Error");
            }
            return RedirectToAction("AccessDenied", "Home");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (User.IsInRole("Admin"))
            {
                var response = await _carService.GetCarById(id);
                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    return View(new CarViewModel(response.Data));
                }
                return RedirectToAction("Error");
            }
            return RedirectToAction("AccessDenied", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CarViewModel carModel)
        {
            
            if(carModel.Image == null)
            {
                carModel.Image = carModel.ImageView.FileName;
            }
            _carService.Edit(carModel);
            return RedirectToAction("GetCars");
        }
        public async Task<IActionResult> Create()
        {
            if (User.IsInRole("Admin"))
            {
                string basePath = HttpContext.Request.PathBase;
                Console.WriteLine(basePath);
                var car = new CarViewModel();
                return View(car);
            }
            return RedirectToAction("AccessDenied", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Create(CarViewModel carModel)
        {
            carModel.DateCreate = DateTime.Now;
            carModel.Image = Path.GetFileName(carModel.ImageView.FileName);
            string filePath = "wwwroot/images/" + carModel.Image;
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await carModel.ImageView.CopyToAsync(stream);
            }
            _carService.Create(carModel);
            return RedirectToAction("GetCars");
        }
    }
}
