using Escola.Domain.Dtos.InputModels;
using Escola.Domain.Models;

namespace Escola.Domain.Dtos.ViewModels
{
    public class MateriaViewModel
    {
        public int CodMateria { get; set; }
        public string Descricao { get; set; }

        public static MateriaViewModel Map(Materia materia) =>new()
        {
            CodMateria = materia.CodMateria,
            Descricao = materia.Descricao,
        };
    }
}
