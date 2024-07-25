using FIAP.Pos.Tech.Challenge.Domain.Models;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.Application.UseCases.PedidoItem.Commands
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