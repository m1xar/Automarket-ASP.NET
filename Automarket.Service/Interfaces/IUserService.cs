using Automarket.Domain.Models;
using Automarket.Domain.Response;
using Automarket.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Automarket.Service.Interfaces
{
    public interface IUserService
    {
        Task<BaseResponse<User>> GetUserById(int id);

        Task<BaseResponse<User>> Create(UserViewModel model);

        Task<BaseResponse<User>> Verify(UserViewModel model);

        Task<BaseResponse<User>> GetUserByName(string name);

        Task<BaseResponse<User>> GetUserByEmail(string email);

        Task<BaseResponse<IEnumerable<User>>> GetUsers();

        Task<BaseResponse<bool>> Delete(int id);

        Task<BaseResponse<bool>> Edit(UserViewModel userModel);
    }
}
