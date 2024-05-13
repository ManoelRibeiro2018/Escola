using Escola.Domain.Models;

namespace Escola.Domain.Dtos.ViewModels
{
    public class AlunoViewModel : AlunoBase
    {
        public static AlunoViewModel Map(Aluno aluno) => new()
        {
            CodAluno = aluno.CodAluno,
            Nome = aluno.Nome,
            Nascimento = aluno.Nascimento,
            CPF = aluno.CPF,
            Endereco = aluno.Endereco,
            Celular = aluno.Celular,
        };
    }
}
