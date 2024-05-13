using Escola.Domain.Dtos.InputModels;

namespace Escola.Domain.Models
{
    public class Notas
    {
        public int CodNota { get; set; }
        public int CodAluno { get; set; }
        public int CodMateria { get; set; }
        public decimal Nota { get; set; }

        public static Notas Map(NotaInputModel entity) => new()
        {
            CodNota = entity.CodNota,
            CodAluno = entity.CodAluno,
            CodMateria = entity.CodMateria,
            Nota = entity.Nota
        };

        public void Update(Notas entity)
        {
            CodNota = entity.CodNota;
            CodAluno = entity.CodAluno;
            CodMateria = entity.CodMateria;
            Nota = entity.Nota;
        }
    }
}
