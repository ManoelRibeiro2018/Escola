using Escola.Application.ExtensionMethod;
using Escola.Domain.Dtos.InputModels;
using FluentValidation;

namespace Escola.Application.Validator
{
    public class AlunoValidator : AbstractValidator<AlunoInputModel>
    {
        public AlunoValidator()
        {
            RuleFor(e => e.CodAluno)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0)
                .WithMessage("Informe o código do aluno");

            RuleFor(e => e.Nome)
                .MinimumLength(4)
                .NotEmpty()
                .WithMessage("Informe o nome maior que 4 caracteres");

            RuleFor(e => e.CPF)
                .Must(ValidCPF)
                .WithMessage("CPF invalido");

            RuleFor(e => e.Nascimento)
                .Must(ValidData)
                .WithMessage("Data de nascimento inválida");
            
            RuleFor(e => e.Endereco)
                .NotNull()
                .NotEmpty()
                .WithMessage("Informe o endereço");

            RuleFor(e => e.Celular)
                .NotNull()
                .Must(ValidPhone)
                .WithMessage("Informe corretamente o celular");
        }

        public static bool ValidCPF(string cpf)
        {
            return cpf.CpfIsValid();
        }

        public static bool ValidData(DateTime data)
        {
            return !(data >= DateTime.Now || data == default || data.Year == DateTime.Now.Year);
        }

        public static bool ValidPhone(string phone)
        {
            if (!phone.JustDigits()) return false;
            else if (phone.Length >= 8 && phone.Length <= 9) return true;
            else return false;
        }
    }
}
