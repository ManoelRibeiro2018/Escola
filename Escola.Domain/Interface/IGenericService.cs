using Escola.Domain.Dtos;
using Escola.Domain.Dtos.ViewModels;

namespace Escola.Domain.Interface
{
    public interface IGenericService<T, D>
    {
        Task<ResponseGeneric> Create(T entity);
        Task<ResponseGeneric> Update(T entity);
        Task<ResponseGeneric> Delete(int id);
        Task<D> Get(int id);
        Task<List<D>> GetAll();
    }
}
