
using Escola.Domain.Dtos.InputModels;

namespace Escola.Domain.Models
{
    public class Aluno : AlunoBase
    {
        public static Aluno Map(AlunoInputModel alunoInputModel) => new()
        {
            CodAluno = alunoInputModel.CodAluno,
            Nome = alunoInputModel.Nome,
            Nascimento = alunoInputModel.Nascimento,
            CPF = alunoInputModel.CPF,
            Endereco = alunoInputModel.Endereco,
            Celular = alunoInputModel.Celular,
        };

        public void Update(Aluno aluno)
        {
            CodAluno = aluno.CodAluno;
            Nome = aluno.Nome;
            Nascimento = aluno.Nascimento;
            CPF = aluno.CPF;
            Endereco = aluno.Endereco;
            Celular = aluno.Celular;

        }
    }
}
