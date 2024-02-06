using Automarket.DAL.Interfaces;
using Automarket.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automarket.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;

        public UserRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<bool> Create(User entity)
        {
            await _db.User.AddAsync(entity);
            _db.SaveChanges();
            return true;
        }

        public async Task<bool> Delete(User entity)
        {
            _db.User.Remove(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<IQueryable<User>> Get()
        {
            var lst = _db.User.ToList();
            return await Task.FromResult(_db.User);
        }

        public async Task<bool> Update(User entity)
        {
            _db.User.Update(entity);
            await _db.SaveChangesAsync();
            return true;

        }

    }
}
