﻿using Automarket.Domain.Models;

namespace Automarket.DAL.Interfaces
{
     public interface IUserRepository : IBaseRepository<User>
    {
        Task<bool> Update(User entity);
    }
}
