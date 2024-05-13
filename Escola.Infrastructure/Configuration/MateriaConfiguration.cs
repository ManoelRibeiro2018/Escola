using Escola.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace Escola.Infrastructure.Configuration
{
    [ExcludeFromCodeCoverage]
    internal class MateriaConfiguration : IEntityTypeConfiguration<Materia>
    {
        public void Configure(EntityTypeBuilder<Materia> builder)
        {
            builder.HasKey(e => e.CodMateria);
        }
    }
}
