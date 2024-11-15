using FIAP.Pos.Tech.Challenge.Micro.Servico.Pedido.Domain.Models;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.Micro.Servico.Pedido.Application.UseCases.PedidoItem.Commands
{
    internal class PedidoItemFindByIdCommand : IRequest<ModelResult>
    {
        public PedidoItemFindByIdCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}