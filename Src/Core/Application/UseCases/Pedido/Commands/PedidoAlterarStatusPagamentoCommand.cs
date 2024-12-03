using FIAP.Pos.Tech.Challenge.Micro.Servico.Pedido.Domain.Models;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Pedido.Domain.ValuesObject;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.Micro.Servico.Pedido.Application.UseCases.Pedido.Commands
{
    public class PedidoAlterarStatusPagamentoCommand : IRequest<ModelResult>
    {
        public PedidoAlterarStatusPagamentoCommand(Guid id,
            enmPedidoStatusPagamento statusPagamento,
            string microServicoProducaoBaseAdress)
        {
            Id = id;
            StatusPagamento = statusPagamento;
            MicroServicoProducaoBaseAdress = microServicoProducaoBaseAdress;
        }

        public Guid Id { get; private set; }
        public enmPedidoStatusPagamento StatusPagamento { get; private set; }
        public string MicroServicoProducaoBaseAdress { get; private set; }
    }
}
