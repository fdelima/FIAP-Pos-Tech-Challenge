using FIAP.Pos.Tech.Challenge.Domain.Entities;
using FIAP.Pos.Tech.Challenge.Domain.Messages;
using FluentValidation;

namespace FIAP.Pos.Tech.Challenge.Domain.Validator
{
    /// <summary>
    /// Regras de validação da model
    /// </summary>
    public class ProdutoValidator : AbstractValidator<Produto>
    {
        /// <summary>
        /// Contrutor das regras de validação da model
        /// </summary>
        public ProdutoValidator()
        {
            RuleFor(c => c.Nome).NotEmpty().WithMessage(ValidationMessages.RequiredField);
            RuleFor(c => c.Preco).NotEmpty().WithMessage(ValidationMessages.RequiredField);
            RuleFor(c => c.Descricao).NotEmpty().WithMessage(ValidationMessages.RequiredField);
            RuleFor(c => c.Categoria).NotEmpty().WithMessage(ValidationMessages.RequiredField);
        }
    }
}
