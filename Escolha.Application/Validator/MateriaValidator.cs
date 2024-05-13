using Escola.Domain.Dtos.InputModels;
using FluentValidation;

namespace Escola.Application.Validator
{
    public class MateriaValidator : AbstractValidator<MateriaInputModel>
    {
        public MateriaValidator()
        {
            RuleFor(e => e.CodMateria)
                .NotEmpty()
                .NotNull()
                .WithMessage("Informe código da máteria");

            RuleFor(e => e.Descricao)
                .NotEmpty()
                .NotNull()
                .WithMessage("Informe a descrição");
        }
    }
}
