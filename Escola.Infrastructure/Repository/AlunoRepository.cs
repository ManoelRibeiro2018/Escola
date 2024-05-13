using Escola.Domain.Interface.Repository;
using Escola.Domain.Models;
using Escola.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Escola.Infrastructure.Repository
{
    [ExcludeFromCodeCoverage]
    public class AlunoRepository : IAlunoRepository
    {
        private readonly EscolaDbContext _dbContext;

        public AlunoRepository(EscolaDbContext context)
        {
            _dbContext = context;
        }

        public async Task Create(Aluno aluno)
        {
            _dbContext.Alunos.Add(aluno);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var aluno = await Get(id) ?? throw new Exception("Aluno não existe");
            _dbContext.Alunos.Remove(aluno);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Aluno> Get(int id)
        {
            return await _dbContext.Alunos.FirstOrDefaultAsync(e => e.CodAluno == id);
        }

        public async Task<List<Aluno>> GetAll()
        {
            return await _dbContext.Alunos.ToListAsync();
        }   

        public async Task Update(Aluno aluno)
        {
            var alunoResponse = await Get(aluno.CodAluno) ?? throw new Exception("Aluno não existe");
            alunoResponse.Update(aluno);
            _dbContext.Alunos.Update(alunoResponse);
            await _dbContext.SaveChangesAsync();
        }
    }
}
