﻿using FIAP.Pos.Tech.Challenge.Micro.Servico.Pedido.Domain.Entities;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Pedido.Domain.Messages;
using FluentValidation;

namespace FIAP.Pos.Tech.Challenge.Micro.Servico.Pedido.Domain.Validator
{
    /// <summary>
    /// Regras de validação da model
    /// </summary>
    public class PedidoItemValidator : AbstractValidator<PedidoItem>
    {
        /// <summary>
        /// Contrutor das regras de validação da model
        /// </summary>
        public PedidoItemValidator()
        {
            RuleFor(c => c.IdPedido).NotNull().WithMessage(ValidationMessages.RequiredField);
            RuleFor(c => c.IdProduto).NotEmpty().WithMessage(ValidationMessages.RequiredField);
            RuleFor(c => c.Quantidade).NotEmpty().WithMessage(ValidationMessages.RequiredField);
        }
    }
}
