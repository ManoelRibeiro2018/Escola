using Escola.Domain.Models;

namespace Escola.Domain.Interface.Repository
{
    public interface INotaRepository : IGenericRepository<Notas>
    {
        Task<List<Notas>> GetByAluno(int codAluno);
    }
}
