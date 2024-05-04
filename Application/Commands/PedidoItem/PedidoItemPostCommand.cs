using FIAP.Pos.Tech.Challenge.Domain.Models;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.Application.Commands.PedidoItem
{
    public class PedidoItemPostCommand: IRequest<ModelResult>
    {
        public PedidoItemPostCommand(Domain.Entities.PedidoItem entity,
            string[]? businessRules = null)
        {
            Entity = entity;
            BusinessRules = businessRules;
        }

        public Domain.Entities.PedidoItem Entity { get; private set; }
        public string[]? BusinessRules { get; private set; }
    }
}