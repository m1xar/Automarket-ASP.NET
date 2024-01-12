﻿using Automarket.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
            return View(response.Data);
        }

        public async Task<IActionResult> GetCar()
        {
            var response = await _carService.GetCarByName("BMW X5");
            return View(response.Data);
        }
    }
}
