using Automarket.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Automarket.Domain.Models;
using Automarket.Domain.ViewModels;


namespace Automarket.Controllers
{
    public class CarController : Controller
    {

        private readonly ICarService _carService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CarController(ICarService carService, IWebHostEnvironment webHostEnvironment)
        {
            _carService = carService;
            _webHostEnvironment = webHostEnvironment;

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
                var car = await _carService.GetCarById(id);
                if (car.Data != null)
                {
                    string webRootPath = _webHostEnvironment.WebRootPath;
                    string path = Path.Combine(webRootPath, "images", car.Data.Image);
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }
                }
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
                string filePath = "wwwroot/images/" + carModel.Image;
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await carModel.ImageView.CopyToAsync(stream);
                }
            }
            await _carService.Edit(carModel);
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
            if (ModelState.IsValid)
            {
                carModel.DateCreate = DateTime.Now;
                carModel.Image = Path.GetFileName(carModel.ImageView.FileName);
                string filePath = "wwwroot/images/" + carModel.Image;
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await carModel.ImageView.CopyToAsync(stream);
                }
                await _carService.Create(carModel);
                return RedirectToAction("GetCars");
            }
            return View(carModel);
        }
    }
}
