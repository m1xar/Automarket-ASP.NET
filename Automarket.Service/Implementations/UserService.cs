﻿using Automarket.DAL.Interfaces;
using Automarket.Domain.Enum;
using Automarket.Domain.Models;
using Automarket.Domain.Response;
using Automarket.Domain.ViewModels;
using Automarket.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Automarket.Domain.Helpers;
using System.Drawing;

namespace Automarket.Service.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<BaseResponse<IEnumerable<User>>> GetUsers()
        {
            var baseResponse = new BaseResponse<IEnumerable<User>>();

            try
            {
                var users = await _userRepository.Get();
                if (await users.CountAsync() == 0)
                {
                    baseResponse.Description = "No users found";
                    baseResponse.StatusCode = StatusCode.IntenalServerError;
                }
                else
                {
                    baseResponse.Data = users;
                    baseResponse.StatusCode = StatusCode.OK;
                }
                return baseResponse;

            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<User>>()
                {
                    Description = $"[Get users] {ex.Message}"
                };
            }
        }

        public async Task<BaseResponse<User>> GetUserById(int id)
        {
            var baseResponse = new BaseResponse<User>();

            try
            {
                var users = await _userRepository.Get();
                var user = await users.FirstOrDefaultAsync(x => x.Id == id);
                if (user != null)
                {
                    baseResponse.Data = user;
                    baseResponse.StatusCode = StatusCode.OK;

                }
                else
                {
                    baseResponse.StatusCode = StatusCode.IntenalServerError;
                    baseResponse.Description = "We have no users with such id";
                }

            }
            catch (Exception ex)
            {
                baseResponse.Description = $"[Get user By ID] {ex.Message}";
            }
            return baseResponse;
        }

        public async Task<BaseResponse<User>> GetUserByName(string name)
        {
            var baseResponse = new BaseResponse<User>();

            try
            {
                var users = await _userRepository.Get();
                var user = await users.FirstOrDefaultAsync(x => x.Name == name);
                if (user != null)
                {
                    baseResponse.Data = user;
                    baseResponse.StatusCode = StatusCode.OK;

                }
                else
                {
                    baseResponse.StatusCode = StatusCode.IntenalServerError;
                    baseResponse.Description = "We have no users with such name";
                }

            }
            catch (Exception ex)
            {
                baseResponse.Description = $"[Get user By Name] {ex.Message}";
            }
            return baseResponse;
        }

        public async Task<BaseResponse<User>> GetUserByEmail(string email)
        {
            var baseResponse = new BaseResponse<User>();

            try
            {
                var users = await _userRepository.Get();
                var user = await users.FirstOrDefaultAsync(x => x.Email == email);
                if (user != null)
                {
                    baseResponse.Data = user;
                    baseResponse.StatusCode = StatusCode.OK;

                }
                else
                {
                    baseResponse.StatusCode = StatusCode.IntenalServerError;
                    baseResponse.Description = "We have no users with such email";
                }

            }
            catch (Exception ex)
            {
                baseResponse.Description = $"[Get user By Email] {ex.Message}";
            }
            return baseResponse;
        }



        public async Task<BaseResponse<User>> Create(UserViewModel model)
        {
            var baseResponse = new BaseResponse<User>();

            var user = new User()
            {
                Name = model.Name,
                Email = model.Email,
                Password = HashPasswordHelper.HashPassword(model.Password),
                Role = model.Role,
                CartItems = null
            };

            try
            {
                if (user != null)
                {
                    baseResponse.Data = user;
                    if (await _userRepository.Create(user))
                    {
                        baseResponse.Description = "User created";
                        baseResponse.StatusCode = StatusCode.OK;
                    }
                    else
                    {
                        baseResponse.Description = "Error, can not create the user";
                        baseResponse.StatusCode = StatusCode.IntenalServerError;
                    }
                }
            }
            catch (Exception ex)
            {
                baseResponse.Description = $"[Create user] {ex.Message}";
                baseResponse.StatusCode = StatusCode.IntenalServerError;
            }
            return baseResponse;
        }


        public async Task<BaseResponse<User>> Verify(UserViewModel user)
        {
            var baseResponse = new BaseResponse<User>();
            user.Password = HashPasswordHelper.HashPassword(user.Password);
            try
            {
                var users = await _userRepository.Get();
                if(await users.FirstOrDefaultAsync(x => x.Name == user.Name && x.Password == user.Password) == null)
                {
                    baseResponse.Data = null;
                    baseResponse.StatusCode = StatusCode.NoUserFound;
                    baseResponse.Description = "There are no user with such name or password";
                }
                else
                {
                    baseResponse.Data = await users.FirstOrDefaultAsync(x => x.Name == user.Name && x.Password == user.Password);
                    baseResponse.StatusCode = StatusCode.OK;
                    baseResponse.Description = "User found";
                }

            }
            catch (Exception ex)
            {
                baseResponse.Description = $"[Verify User] {ex.Message}";
            }
            return baseResponse;
        }

        public async Task<BaseResponse<bool>> Delete(int id)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                var users = await _userRepository.Get();
                    if(await _userRepository.Delete(users.FirstOrDefault(x => x.Id == id)))
                    {
                        baseResponse.Data = true;
                        baseResponse.StatusCode = StatusCode.OK;
                    }
                    else
                    {
                        baseResponse.Data = false;
                        baseResponse.StatusCode = StatusCode.IntenalServerError;
                    }
                
            }
            catch(Exception ex)
            {
                baseResponse.Description = $"[Delete User] {ex.Message}";
            }
            return baseResponse;
        }
        public async Task<BaseResponse<bool>> Edit(UserViewModel userModel)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                var users = await _userRepository.Get();
                var user = users.FirstOrDefault(x=>x.Id == userModel.Id);
                user.Name = userModel.Name;
                user.Email = userModel.Email;
                user.Password = HashPasswordHelper.HashPassword(userModel.Password);
                user.Role = userModel.Role;

                    if (await _userRepository.Update(user))
                    {
                        baseResponse.Data = true;
                        baseResponse.StatusCode = StatusCode.OK;
                    }
                    else
                    {
                        baseResponse.Data = false;
                        baseResponse.StatusCode = StatusCode.IntenalServerError;
                    }

            }
            catch (Exception ex)
            {
                baseResponse.Description = $"[Edit User] {ex.Message}";
            }
            return baseResponse;
        }
    }
}
