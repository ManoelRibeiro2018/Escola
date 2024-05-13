using Escola.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Escola.Infrastructure.Context
{
    [ExcludeFromCodeCoverage]
    public class EscolaDbContext: DbContext
    {
        public EscolaDbContext(DbContextOptions<EscolaDbContext> options): base(options) {}

        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Materia> Materias { get; set; }
        public DbSet<Notas> Notas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(assembly: Assembly.GetExecutingAssembly());
        }
    }
}
