using Escola.Domain.Models;

namespace Escola.Domain.Dtos.ViewModels
{
    public class NotaViewModel
    {
        public int CodAluno { get; set; }
        public int CodMateria { get; set; }
        public decimal Nota { get; set; }

        public static NotaViewModel Map(Notas nota) => new()
        {
            CodMateria = nota.CodMateria,
            CodAluno = nota.CodAluno,
            Nota = nota.Nota

        };
    }
}
