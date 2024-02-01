using Automarket.DAL.Interfaces;
using Automarket.Domain.Enum;
using Automarket.Domain.Models;
using Automarket.Domain.Response;
using Automarket.Domain.ViewModels;
using Automarket.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Automarket.Service.Implementations
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICarRepository _carRepository;

        public CartService(ICartRepository cartRepository, IUserRepository userRepository, ICarRepository carRepository)
        {
            _cartRepository = cartRepository;
            _userRepository = userRepository;
            _carRepository = carRepository;
        }

        public async Task<BaseResponse<List<CartItem>>> GetItemsByUserId(int id)
        {
            var baseResponse = new BaseResponse<List<CartItem>>();

            try
            {
                var items = await _cartRepository.Get();
                var itemsList = items.Where(x => x.UserId == id).ToList();
                baseResponse.Data = itemsList;
                baseResponse.StatusCode = StatusCode.OK;
            }
            catch (Exception ex)
            {
                baseResponse.Description = $"[Get Item By ID] {ex.Message}";
            }
            return baseResponse;
        }

        public async Task<BaseResponse<bool>> Delete(int id)
        {
            var baseResponse = new BaseResponse<bool>();

            try
            {
                var items = await _cartRepository.Get();
                var item = await items.FirstOrDefaultAsync(x => x.Id == id);
                if (item != null)
                {
                    if (await _cartRepository.Delete(item))
                    {
                        baseResponse.Data = true;
                        baseResponse.Description = "Item deleted";
                        baseResponse.StatusCode = StatusCode.OK;
                    }
                    else
                    {
                        baseResponse.Data = false;
                        baseResponse.Description = "Error, can not delete the item";
                        baseResponse.StatusCode = StatusCode.IntenalServerError;
                    }
                }
            }
            catch (Exception ex)
            {
                baseResponse.Data = false;
                baseResponse.Description = $"[Delete item] {ex.Message}";
                baseResponse.StatusCode = StatusCode.IntenalServerError;
            }
            return baseResponse;
        }

        public async Task<BaseResponse<CartItem>> Create(int carId, int userId)
        {
            var baseResponse = new BaseResponse<CartItem>();
            var users = await _userRepository.Get();
            var user = users.FirstOrDefault(x => x.Id == userId);
            var cars = await _carRepository.Get();
            var car = cars.FirstOrDefault(x => x.Id == carId);
            var item = new CartItem()
            {
                CarId = carId,
                UserId = userId,
                Car = car,
                User = user
            };

            try
            {
                if (item != null && car != null && user !=null)
                {
                    baseResponse.Data = item;
                    if (await _cartRepository.Create(item))
                    {
                        baseResponse.Description = "Item added";
                        baseResponse.StatusCode = StatusCode.OK;
                    }
                    else
                    {
                        baseResponse.Description = "Error, can not add the item";
                        baseResponse.StatusCode = StatusCode.IntenalServerError;
                    }
                }
            }
            catch (Exception ex)
            {
                baseResponse.Description = $"[Create item] {ex.Message}";
                baseResponse.StatusCode = StatusCode.IntenalServerError;
            }
            return baseResponse;
        }
    }
}
