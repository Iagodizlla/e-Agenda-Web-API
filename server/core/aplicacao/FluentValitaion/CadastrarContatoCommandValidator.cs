using eAgenda.Core.Aplicacao.ModuloContato.Commands;
using FluentValidation;

namespace eAgenda.Core.Aplicacao.FluentValidation;

public class CadastrarContatoCommandValidator : AbstractValidator<CadastrarContatoCommand>
{
    public CadastrarContatoCommandValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("O nome é obrigatório.")
            .MinimumLength(2).WithMessage("O nome deve ter pelo menos {MinLength} caracteres.")
            .MaximumLength(100).WithMessage("O nome deve conter no máximo {MaxLength} caracteres.");

        RuleFor(x => x.Telefone)
           .NotEmpty().WithMessage("O telefone é obrigatório.")
           .Matches(@"^\(\d{2}\) \d{4,5}-\d{4}$").WithMessage("O telefone deve seguir o formato (XX) XXXXX-XXXX ou (XX) XXXX-XXXX.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("O email é obrigatório.")
            .EmailAddress().WithMessage("O email deve seguir o formato nome@provedor.");

        RuleFor(x => x.Empresa)
            .MinimumLength(2).WithMessage("O nome da empresa deve ter pelo menos {MinLength} caracteres.")
            .MaximumLength(100).WithMessage("O nome da empresa deve conter no máximo {MaxLength} caracteres.")
            .When(x => !string.IsNullOrWhiteSpace(x.Empresa));

        RuleFor(x => x.Cargo)
            .MinimumLength(2).WithMessage("O cargo deve ter pelo menos {MinLength} caracteres.")
            .MaximumLength(100).WithMessage("O cargo deve conter no máximo {MaxLength} caracteres.")
            .When(x => !string.IsNullOrWhiteSpace(x.Cargo));
    }
}