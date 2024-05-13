using Escola.Domain.Interface.Repository;
using Escola.Domain.Models;
using Escola.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Escola.Infrastructure.Repository
{
    [ExcludeFromCodeCoverage]
    public class NotaRepository : INotaRepository
    {
        private readonly EscolaDbContext _dbContext;

        public NotaRepository(EscolaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Create(Notas entity)
        {
            _dbContext.Notas.Add(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var nota = await Get(id) ?? throw new Exception("Nota não existe");
            _dbContext.Notas.Remove(nota);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Notas> Get(int id)
        {
            return await _dbContext.Notas.FirstOrDefaultAsync(e => e.CodNota == id);
        }

        public async Task<List<Notas>> GetAll()
        {
            return await _dbContext.Notas.ToListAsync();
        }

        public async Task<List<Notas>> GetByAluno(int codAluno)
        {
            return await _dbContext.Notas.Where(e => e.CodAluno == codAluno).ToListAsync();
        }

        public async Task Update(Notas entity)
        {
            var notaResponse = await Get(entity.CodNota) ?? throw new Exception("Aluno não existe");
            notaResponse.Update(entity);
            _dbContext.Notas.Update(notaResponse);
            await _dbContext.SaveChangesAsync();
        }
    }
}
