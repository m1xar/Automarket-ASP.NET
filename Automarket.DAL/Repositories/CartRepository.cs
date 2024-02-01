using Automarket.DAL.Interfaces;
using Automarket.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automarket.DAL.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _db;

        public CartRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<bool> Create(CartItem entity)
        {
            await _db.Cart.AddAsync(entity);
            _db.SaveChanges();
            return true;
        }

        public async Task<bool> Delete(CartItem entity)
        {
            _db.Cart.Remove(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<IQueryable<CartItem>> Get()
        {
            return await Task.FromResult(_db.Cart);
        }
    }
}
