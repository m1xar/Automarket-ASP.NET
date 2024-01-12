using Automarket.DAL.Interfaces;
using Automarket.Domain.Models;
using Automarket.Domain.Response;
using Automarket.Service.Interfaces;
using Automarket.Domain.Enum;

namespace Automarket.Service.Implementations
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;

        public CarService(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<IBaseResponse<IEnumerable<Car>>> GetCars()
        {
            var baseResponse = new BaseResponse<IEnumerable<Car>>();

            try
            {
                var cars = await _carRepository.Select();
                if(cars.Count == 0)
                {
                    baseResponse.Description = "No cars found";
                    baseResponse.StatusCode = StatusCode.OK;
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

        public async Task<IBaseResponse<Car>> GetCarByName(string name)
        {
            var baseResponse = new BaseResponse<Car>();

            try
            {
                var car = await _carRepository.GetByName(name);
                if(car != null)
                {
                    baseResponse.Data = car;
                    baseResponse.StatusCode = StatusCode.OK;
                    
                }
                else
                {
                    baseResponse.StatusCode = StatusCode.IntenalServerError;
                    baseResponse.Description = "We have no cars with such name";
                }
                return baseResponse;
            }
            catch(Exception ex)
            {
                return new BaseResponse<Car>()
                {
                    Description = $"[Get Car By Name] {ex.Message}"
                };
            }
        }
    }
}
