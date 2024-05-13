using Escola.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace Escola.Infrastructure.Configuration
{
    [ExcludeFromCodeCoverage]
    internal class Notaconfiguration : IEntityTypeConfiguration<Notas>
    {
        public void Configure(EntityTypeBuilder<Notas> builder)
        {
            builder.HasKey(e => e.CodNota);
        }
    }
}
