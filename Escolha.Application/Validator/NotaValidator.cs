using Escola.Domain.Dtos.InputModels;
using FluentValidation;

namespace Escola.Application.Validator
{
    public class NotaValidator : AbstractValidator<NotaInputModel>
    {
        public NotaValidator() {

            RuleFor(e => e.CodNota)
               .NotEmpty()
               .NotNull()
               .WithMessage("Informe código da nota");

            RuleFor(e => e.CodAluno)
               .NotEmpty()
               .NotNull();

            RuleFor(e => e.CodMateria)
               .NotEmpty()
               .NotNull();

            RuleFor(e => e.Nota)
               .NotEmpty()
               .NotNull();
        }
    }
}
