using Escola.Domain.Dtos;
using Escola.Domain.Dtos.InputModels;
using Escola.Domain.Dtos.ViewModels;

namespace Escola.Domain.Interface.Service
{
    public interface INotaService : IGenericService<NotaInputModel, NotaViewModel>
    {
        Task<List<NotaReponse>> GetAllWithMateria(int codAluno);
    }
}
