using Escola.Domain.Dtos.InputModels;

namespace Escola.Domain.Models
{
    public class Materia
    {
        public int CodMateria { get; set; }
        public string Descricao { get; set; }

        public static Materia Map(MateriaInputModel materiaInputModel) => new()
        {
            CodMateria = materiaInputModel.CodMateria,
            Descricao = materiaInputModel.Descricao,
        };

        public void Update(Materia materia)
        {
            CodMateria = materia.CodMateria;
            Descricao = materia.Descricao;
        }
    }
}
