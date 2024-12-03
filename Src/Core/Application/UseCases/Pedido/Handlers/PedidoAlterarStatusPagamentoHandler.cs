using FIAP.Pos.Tech.Challenge.Micro.Servico.Pedido.Application.UseCases.Pedido.Commands;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Pedido.Domain;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Pedido.Domain.Entities;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Pedido.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Pedido.Domain.Models;
using MediatR;
using System.Net.Http.Json;

namespace FIAP.Pos.Tech.Challenge.Micro.Servico.Pedido.Application.UseCases.Pedido.Handlers
{
    public class PedidoAlterarStatusPagamentoHandler : IRequestHandler<PedidoAlterarStatusPagamentoCommand, ModelResult>
    {
        private readonly IPedidoService _service;

        public PedidoAlterarStatusPagamentoHandler(IPedidoService service)
        {
            _service = service;
        }

        public async Task<ModelResult> Handle(PedidoAlterarStatusPagamentoCommand command, CancellationToken cancellationToken = default)
        {
            var result = await _service.FindByIdAsync(command.Id);

            if (result.IsValid)
            {
                var pedido = (Domain.Entities.Pedido)result.Model;
                pedido.StatusPagamento = command.StatusPagamento.ToString();
                result = await _service.UpdateAsync(pedido);

                if (result.IsValid)
                {
                    try
                    {
                        var producaoClient = Util.GetClient(command.MicroServicoProducaoBaseAdress);

                        HttpResponseMessage response = await producaoClient.PostAsJsonAsync(
                            "api/producao/pedido/InserirRecebido", pedido);

                        if (!response.IsSuccessStatusCode)
                            result.AddMessage("Não foi possível enviar pedido para produção.");
                    }
                    catch (Exception)
                    {
                        result.AddMessage("Falha ao conectar a produção.");
                    }
                }
            }

            return result;
        }
    }
}
