using Escola.Domain.Models;

namespace Escola.Domain.Interface
{
    public interface IGenericRepository<T>
    {
        Task Create(T entity);
        Task Update(T entity);
        Task Delete(int id);
        Task<T> Get(int id);
        Task<List<T>> GetAll();
    }
}
