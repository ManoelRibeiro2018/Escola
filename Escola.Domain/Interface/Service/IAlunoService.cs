using Escola.Domain.Dtos.InputModels;
using Escola.Domain.Dtos.ViewModels;

namespace Escola.Domain.Interface.Service
{
    public interface IAlunoService : IGenericService<AlunoInputModel, AlunoViewModel>
    {
    }
}
