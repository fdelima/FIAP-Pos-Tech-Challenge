using FIAP.Pos.Tech.Challenge.Domain.Entities;
using FIAP.Pos.Tech.Challenge.Domain.Messages;
using FIAP.Pos.Tech.Challenge.Domain.ValuesObject;
using FluentValidation;

namespace FIAP.Pos.Tech.Challenge.Domain.Validator
{
    /// <summary>
    /// Regras de validação da model
    /// </summary>
    public class PedidoValidator : AbstractValidator<Pedido>
    {
        /// <summary>
        /// Contrutor das regras de validação da model
        /// </summary>
        public PedidoValidator()
        {
            RuleFor(c => c.IdDispositivo).NotEmpty().WithMessage(ValidationMessages.RequiredField);
            RuleFor(c => c.PedidoItems).Must(x => x.Count() > 0).WithMessage(ValidationMessages.OneMandatoryItem);
            RuleForEach(c => c.PedidoItems).SetValidator(x => new PedidoItemValidator());
        }
    }
}
