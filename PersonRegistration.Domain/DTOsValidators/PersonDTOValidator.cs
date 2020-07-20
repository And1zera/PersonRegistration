using FluentValidation;
using PersonRegistration.Domain.DTOs;

namespace PersonRegistration.Domain.DTOsValidators
{
    public class PersonDTOValidator : AbstractValidator<PersonDTO>
    {
        public PersonDTOValidator()
        {
            RuleFor(r => r.Name).NotEmpty().MaximumLength(50).WithMessage("Nome é obrigatorio e deve conter até 50 caracteres");
            RuleFor(r => r.Email).NotEmpty().EmailAddress().WithMessage("Email é obrigatorio");
            RuleFor(r => r.Address).NotEmpty().WithMessage("Endereço obrigatorio");
            RuleFor(r => r.City).NotEmpty().WithMessage("Cidade é obrigatoria");
            RuleFor(r => r.State).NotEmpty().WithMessage("Estado é obrigatorio");
            RuleFor(r => r.Phone).NotEmpty().WithMessage("Telefone é obrigatorio");
            RuleFor(r => r.Password).NotEmpty().MaximumLength(6).WithMessage("Senha não pode conter mais de 6 caracteres e a Senha é obrigatoria");
        }
    }
}
