using Automarket.Domain.Models;
using Automarket.Domain.Response;
using Automarket.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automarket.Service.Interfaces
{
    public interface ICartService
    {

        Task<BaseResponse<List<CartItem>>> GetItemsByUserId(int id);

        Task<BaseResponse<bool>> Delete(int id);

        Task<BaseResponse<CartItem>> Create(int carId, int userId);
    }
}
