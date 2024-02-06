using Automarket.DAL.Interfaces;
using Automarket.Domain.Models;
using Automarket.Domain.Response;
using Automarket.Service.Interfaces;
using Automarket.Domain.Enum;
using Automarket.Domain.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Automarket.Service.Implementations
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;

        public CarService(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<BaseResponse<IEnumerable<Car>>> GetCars()
        {
            var baseResponse = new BaseResponse<IEnumerable<Car>>();

            try
            {
                var cars = await _carRepository.Get();
                if(await cars.CountAsync() == 0)
                {
                    baseResponse.Description = "No cars found";
                    baseResponse.StatusCode = StatusCode.IntenalServerError;
                }
                else
                {
                    baseResponse.Data = cars;
                    baseResponse.StatusCode = StatusCode.OK;
                }
                return baseResponse;

            }
            catch(Exception ex)
            {
                return new BaseResponse<IEnumerable<Car>>()
                {
                    Description = $"[Get Cars] {ex.Message}"
                };
            }
        }

        public async Task<BaseResponse<Car>> GetCarById(int id)
        {
            var baseResponse = new BaseResponse<Car>();

            try
            {
                var cars = await _carRepository.Get();
                var car = await cars.FirstOrDefaultAsync(x => x.Id == id);
                if (car != null)
                {
                    baseResponse.Data = car;
                    baseResponse.StatusCode = StatusCode.OK;

                }
                else
                {
                    baseResponse.StatusCode = StatusCode.IntenalServerError;
                    baseResponse.Description = "We have no cars with such id";
                }
                
            }
            catch (Exception ex)
            {
                baseResponse.Description = $"[Get Car By ID] {ex.Message}";
            }
            return baseResponse;
        }

        public async Task<BaseResponse<bool>> Delete(int id)
        {
            var baseResponse = new BaseResponse<bool>();

            try
            {
                var cars = await _carRepository.Get();
                var car = await cars.FirstOrDefaultAsync(x => x.Id == id);
                if (car != null)
                {
                    if (await _carRepository.Delete(car))
                    {
                        baseResponse.Data = true;
                        baseResponse.Description = "Car deleted";
                        baseResponse.StatusCode = StatusCode.OK;
                    }
                    else
                    {
                        baseResponse.Data = false;
                        baseResponse.Description = "Error, can not delete the car";
                        baseResponse.StatusCode= StatusCode.IntenalServerError;
                    }
                }
            }
            catch (Exception ex)
            {
                baseResponse.Data= false;
                baseResponse.Description = $"[Delete Car] {ex.Message}";
                baseResponse.StatusCode = StatusCode.IntenalServerError;
            }
            return baseResponse;
        }

        public async Task<BaseResponse<Car>> Create(CarViewModel model)
        {
            var baseResponse = new BaseResponse<Car>();

            var car = new Car()
            {
                Name = model.Name,
                Description = model.Description,
                Model = model.Model,
                Speed = model.Speed,
                Price = model.Price,
                DateCreate = model.DateCreate,
                Image = model.Image,
                TypeCar = model.TypeCar,
            };

            try
            {
                if (car != null)
                {
                    baseResponse.Data = car;
                    if (await _carRepository.Create(car))
                    {
                        baseResponse.Description = "Car created";
                        baseResponse.StatusCode = StatusCode.OK;
                    }
                    else
                    {
                        baseResponse.Description = "Error, can not create the car";
                        baseResponse.StatusCode = StatusCode.IntenalServerError;
                    }
                }
            }
            catch (Exception ex)
            {
                baseResponse.Description = $"[Create Car] {ex.Message}";
                baseResponse.StatusCode = StatusCode.IntenalServerError;
            }
            return baseResponse;
        }

        public async Task<BaseResponse<Car>> Edit(CarViewModel model)
        {
            var baseResponse = new BaseResponse<Car>();
            try
            {
                var cars = await _carRepository.Get();
                var car = await cars.FirstOrDefaultAsync(x => x.Id == model.Id);
                if (car != null)
                {
                    car.Description = model.Description;
                    car.Speed = model.Speed;
                    car.DateCreate = model.DateCreate;
                    car.Model = model.Model;
                    car.Price = model.Price;
                    car.TypeCar = model.TypeCar;
                    car.Image = model.Image;


                    await _carRepository.Update(car);
                }
                else
                {
                    baseResponse.StatusCode = StatusCode.CarNotFound;
                    baseResponse.Description = "Car not found";
                }

            }
            catch (Exception ex)
            {
                baseResponse.Description = $"[Edit Car] {ex.Message}";
                baseResponse.StatusCode = StatusCode.IntenalServerError;
            }
            return baseResponse;

        }

    }
}
