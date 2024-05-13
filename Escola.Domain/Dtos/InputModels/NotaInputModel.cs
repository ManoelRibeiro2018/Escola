namespace Escola.Domain.Dtos.InputModels
{
    public class NotaInputModel
    {
        public int CodNota { get; set; }
        public int CodAluno { get; set; }
        public int CodMateria { get; set; }
        public decimal Nota { get; set; }
    }
}
