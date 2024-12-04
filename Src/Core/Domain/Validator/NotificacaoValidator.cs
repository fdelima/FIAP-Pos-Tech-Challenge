using FIAP.Pos.Tech.Challenge.Micro.Servico.Pedido.Domain.Entities;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Pedido.Domain.Messages;
using FluentValidation;

namespace FIAP.Pos.Tech.Challenge.Micro.Servico.Pedido.Domain.Validator
{
    /// <summary>
    /// Regras de validação da model
    /// </summary>
    public class NotificacaoValidator : AbstractValidator<Notificacao>
    {
        /// <summary>
        /// Contrutor das regras de validação da model
        /// </summary>
        public NotificacaoValidator()
        {
            RuleFor(c => c.Mensagem).NotEmpty().WithMessage(ValidationMessages.RequiredField);
        }
    }
}
