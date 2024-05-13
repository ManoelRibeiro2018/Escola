using Escola.Domain.Interface.Repository;
using Escola.Domain.Models;
using Escola.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Escola.Infrastructure.Repository
{
    [ExcludeFromCodeCoverage]
    public class MateriaRepository : IMateriaRepository
    {
        private readonly EscolaDbContext _dbContext;

        public MateriaRepository(EscolaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Create(Materia materia)
        {
            _dbContext.Materias.Add(materia);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var materia = await Get(id) ?? throw new Exception("Aluno não existe");
            _dbContext.Materias.Remove(materia);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Materia> Get(int id)
        {
            return await _dbContext.Materias.FirstOrDefaultAsync(e => e.CodMateria == id);
        }

        public async Task<List<Materia>> GetAll()
        {
            return await _dbContext.Materias.ToListAsync();
        }

        public async Task Update(Materia materia)
        {
            var materiaResponse = await Get(materia.CodMateria) ?? throw new Exception("Aluno não existe");
            materiaResponse.Update(materia);
            _dbContext.Materias.Update(materiaResponse);
            await _dbContext.SaveChangesAsync();
        }
    }
}
