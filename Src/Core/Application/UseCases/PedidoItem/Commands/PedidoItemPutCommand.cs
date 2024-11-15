using FIAP.Pos.Tech.Challenge.Micro.Servico.Pedido.Domain.Models;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.Micro.Servico.Pedido.Application.UseCases.PedidoItem.Commands
{
    internal class PedidoItemPutCommand : IRequest<ModelResult>
    {
        public PedidoItemPutCommand(Guid id, Domain.Entities.PedidoItem entity,
            string[]? businessRules = null)
        {
            Id = id;
            Entity = entity;
            BusinessRules = businessRules;
        }

        public Guid Id { get; private set; }
        public Domain.Entities.PedidoItem Entity { get; private set; }
        public string[]? BusinessRules { get; private set; }
    }
}