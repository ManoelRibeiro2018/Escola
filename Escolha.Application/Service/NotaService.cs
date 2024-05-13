using Escola.Domain.Dtos;
using Escola.Domain.Dtos.InputModels;
using Escola.Domain.Dtos.ViewModels;
using Escola.Domain.Interface.Repository;
using Escola.Domain.Interface.Service;
using Escola.Domain.Models;

namespace Escola.Application.Service
{
    public class NotaService : INotaService
    {
        private readonly INotaRepository _notaRepository;
        private readonly IAlunoRepository _alunoRepository;
        private readonly IMateriaRepository _materiaRepository;

        public NotaService(INotaRepository notaRepository, IAlunoRepository alunoRepository, IMateriaRepository materiaRepository)
        {
            _notaRepository = notaRepository;
            _alunoRepository = alunoRepository;
            _materiaRepository = materiaRepository;
        }

        public async Task<ResponseGeneric> Create(NotaInputModel entity)
        {
            try
            {
                await ValidExistMateria(entity.CodMateria);

                await ValidExistAluno(entity.CodAluno);

                var nota = Notas.Map(entity);
                await _notaRepository.Create(nota);

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
                await _notaRepository.Delete(id);
                return ResponseGeneric.Successful();
            }
            catch (Exception ex)
            {
                return ResponseGeneric.Failure(ex);
            }
        }

        public async Task<NotaViewModel> Get(int id)
        {
            var nota = await _notaRepository.Get(id);
            if (nota is null) return null;
            return NotaViewModel.Map(nota);
        }

        public async Task<List<NotaViewModel>> GetAll()
        {
            var notas = await _notaRepository.GetAll();
            List<NotaViewModel> notaViewModels = [];
            foreach (var item in notas)
            {
                notaViewModels.Add(NotaViewModel.Map(item));
            }
            return notaViewModels;
        }

        public async Task<List<NotaReponse>> GetAllWithMateria(int codAluno)
        {
            await ValidExistAluno(codAluno);

            var notas = await _notaRepository.GetByAluno(codAluno);
            List<NotaReponse> notaReponses = [];
          
            foreach (var item in notas)
            {
                var materia = await _materiaRepository.Get(item.CodMateria);
                notaReponses.Add(NotaReponse.Map(materia, item));
            }
            return notaReponses;
        }

        public async Task<ResponseGeneric> Update(NotaInputModel entity)
        {
            try
            {
                var nota = Notas.Map(entity);
                await _notaRepository.Update(nota);
                return ResponseGeneric.Successful();
            }
            catch (Exception ex)
            {
                return ResponseGeneric.Failure(ex);
            }
        }

        private async Task ValidExistAluno(int codAluno)
        {
            var result = await _alunoRepository.Get(codAluno);
            if (result is null)
                throw new Exception("Aluno não existe");
        }

        private async Task ValidExistMateria(int codMateria)
        {
            var result = await _materiaRepository.Get(codMateria);
            if (result is null)
                throw new Exception("Materia não existe");
        }

    }
}
