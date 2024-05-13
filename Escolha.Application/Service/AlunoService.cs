using Escola.Domain.Dtos;
using Escola.Domain.Dtos.InputModels;
using Escola.Domain.Dtos.ViewModels;
using Escola.Domain.Interface.Repository;
using Escola.Domain.Interface.Service;
using Escola.Domain.Models;

namespace Escola.Application.Service
{
    public class AlunoService : IAlunoService
    {
        private readonly IAlunoRepository _alunoRepository;

        public AlunoService(IAlunoRepository alunoRepository)
        {
            _alunoRepository = alunoRepository;
        }

        public async Task<ResponseGeneric> Create(AlunoInputModel alunoInputModel)
        {
            try
            {
                var aluno = Aluno.Map(alunoInputModel);
                await _alunoRepository.Create(aluno);

                return ResponseGeneric.Successful();
            }
            catch (Exception ex)
            {
                return ResponseGeneric.Failure(ex);
            }
        }

        public async Task<ResponseGeneric> Delete(int id)
        {
            try
            {
                await _alunoRepository.Delete(id);
                return ResponseGeneric.Successful();
            }
            catch (Exception ex)
            {
                return ResponseGeneric.Failure(ex);
            }
        }

        public async Task<AlunoViewModel?> Get(int id)
        {
            var aluno = await _alunoRepository.Get(id);
            if (aluno is null) return null;
            return AlunoViewModel.Map(aluno);
        }

        public async Task<List<AlunoViewModel>> GetAll()
        {
            var alunos = await _alunoRepository.GetAll();
            List<AlunoViewModel> alunosResponse = [];
            foreach (var item in alunos)
            {
                alunosResponse.Add(AlunoViewModel.Map(item));
            }
            return alunosResponse;
        }

        public async Task<ResponseGeneric> Update(AlunoInputModel alunoInputModel)
        {
            try
            {
                var aluno = Aluno.Map(alunoInputModel);
                await _alunoRepository.Update(aluno);
                return ResponseGeneric.Successful();
            }
            catch (Exception ex)
            {
                return ResponseGeneric.Failure(ex);
            }
        }
    }
}
