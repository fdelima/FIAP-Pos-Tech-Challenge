using FIAP.Pos.Tech.Challenge.Micro.Servico.Pedido.Domain;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Pedido.Domain.Interfaces;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.Micro.Servico.Pedido.Application.UseCases.Pedido.Commands
{
    public class PedidoGetListaCommand : IRequest<PagingQueryResult<Domain.Entities.Pedido>>
    {
        public PedidoGetListaCommand(IPagingQueryParam filter)
        {
            Filter = filter;
        }

        public IPagingQueryParam Filter { get; }
    }
}