using Automarket.Domain.Models;

namespace Automarket.DAL.Interfaces
{
    public interface ICarRepository : IBaseRepository<Car>
    {
        Task<Car> Update(Car entity);
    }
}
