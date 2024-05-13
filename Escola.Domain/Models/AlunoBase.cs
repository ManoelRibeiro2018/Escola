namespace Escola.Domain.Models
{
    public abstract class AlunoBase
    {
        public int CodAluno { get; set; }
        public string Nome { get; set; }
        public DateTime Nascimento { get; set; }
        public string CPF { get; set; }
        public string Endereco { get; set; }
        public string Celular { get; set; }
    }
}
