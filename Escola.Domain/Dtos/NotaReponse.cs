using Escola.Domain.Models;

namespace Escola.Domain.Dtos
{
    public class NotaReponse
    {
        public Materia Materia { get; set; }
        public Notas Nota { get; set; }

        public static NotaReponse Map(Materia materia, Notas nota) => new() { Materia = materia, Nota = nota };

    }
}
