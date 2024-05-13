using Escola.Domain.Dtos;
using Escola.Domain.Dtos.InputModels;
using Escola.Domain.Dtos.ViewModels;
using Escola.Domain.Interface.Repository;
using Escola.Domain.Interface.Service;
using Escola.Domain.Models;

namespace Escola.Application.Service
{
    public class MateriaService : IMateriaService
    {
        private readonly IMateriaRepository _materiaRepository;

        public MateriaService(IMateriaRepository materiaRepository)
        {
            _materiaRepository = materiaRepository;
        }

        public async Task<ResponseGeneric> Create(MateriaInputModel materiaInputModel)
        {
            try
            {
                var materia = Materia.Map(materiaInputModel);
                await _materiaRepository.Create(materia);
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
                await _materiaRepository.Delete(id);
                return ResponseGeneric.Successful();
            }
            catch (Exception ex)
            {
                return ResponseGeneric.Failure(ex);
            }
        }

        public async Task<MateriaViewModel> Get(int id)
        {
            var materia = await _materiaRepository.Get(id);
            if (materia is null) return null;
            return MateriaViewModel.Map(materia);
        }

        public async Task<List<MateriaViewModel>> GetAll()
        {
            var materias = await _materiaRepository.GetAll();
            List<MateriaViewModel>materiasResponse = [];
            foreach (var item in materias)
            {
                materiasResponse.Add(MateriaViewModel.Map(item));
            }
            return materiasResponse;
        }

        public async Task<ResponseGeneric> Update( MateriaInputModel materiaInputModel)
        {
            try
            {
                var aluno = Materia.Map(materiaInputModel);
                await _materiaRepository.Update(aluno);
                return ResponseGeneric.Successful();
            }
            catch (Exception ex)
            {
                return ResponseGeneric.Failure(ex);
            }
        }
    }
}
