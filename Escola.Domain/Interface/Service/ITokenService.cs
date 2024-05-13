using Escola.Domain.Dtos.InputModels;

namespace Escola.Domain.Interface.Service
{
    public interface ITokenService
    {
        Task<string> GenereteToken(int codAluno);
        string GenereteTokenDefault();
    }
}
