
namespace Automarket.DAL.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task<bool> Create(T entity);

        Task<IQueryable<T>> Get();

        Task<bool> Delete(T entity);
    }
}
