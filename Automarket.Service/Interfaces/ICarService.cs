using Automarket.Domain.Models;
using Automarket.Domain.Response;
using Automarket.Domain.ViewModels;

namespace Automarket.Service.Interfaces
{
    public interface ICarService
    {
        Task<BaseResponse<IEnumerable<Car>>> GetCars();

        Task<BaseResponse<Car>> GetCarById(int id);

        Task<BaseResponse<bool>> Delete(int id);

        Task<BaseResponse<Car>> Create(CarViewModel model);

        Task<BaseResponse<Car>> Edit(CarViewModel model);
    }
}
