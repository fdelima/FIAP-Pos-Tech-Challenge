using FIAP.Pos.Tech.Challenge.Micro.Servico.Pedido.Application.UseCases.Pedido.Commands;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Pedido.Domain;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Pedido.Domain.Entities;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Pedido.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Pedido.Domain.Models;
using MediatR;
using System.Net.Http.Json;

namespace FIAP.Pos.Tech.Challenge.Micro.Servico.Pedido.Application.UseCases.Pedido.Handlers
{
    public class PedidoPostHandler : IRequestHandler<PedidoPostCommand, ModelResult>
    {
        private readonly IPedidoService _service;

        public PedidoPostHandler(IPedidoService service)
        {
            _service = service;
        }

        public async Task<ModelResult> Handle(PedidoPostCommand command, CancellationToken cancellationToken = default)
        {
            var warnings = new List<string>();
            try
            {
                var cadastroClient = Util.GetClient(command.MicroServicoPagamentoBaseAdress);

                HttpResponseMessage response =
                    await cadastroClient.GetAsync($"api/cadastro/Cliente/{command.Entity.IdCliente}");

                if (!response.IsSuccessStatusCode)
                    warnings.Add("Não foi possível validar cliente.");

                response = await cadastroClient.GetAsync($"api/cadastro/Dispositivo/{command.Entity.IdDispositivo}");

                if (!response.IsSuccessStatusCode)
                    warnings.Add("Não foi possível validar dispositivo.");

                foreach (var produto in command.Entity.PedidoItems)
                {
                    response = await cadastroClient.GetAsync($"api/cadastro/Produto/{produto.IdProduto}");

                    if (!response.IsSuccessStatusCode)
                        warnings.Add($"Não foi possível validar produto {produto.IdProduto}.");
                }
            }
            catch (Exception)
            {
                warnings.Add("Falha ao conectar ao cadastro.");
            }

            var result = await _service.InsertAsync(command.Entity, command.BusinessRules);

            if (result.IsValid)
            {
                try
                {
                    var pagamentoClient = Util.GetClient(command.MicroServicoPagamentoBaseAdress);

                    HttpResponseMessage response =
                     await pagamentoClient.PostAsJsonAsync("api/Pagamento/Pedido", result.Model);

                    if (!response.IsSuccessStatusCode)
                        result.AddMessage("Não foi possível enviar pedido para o pagamento.");
                }
                catch (Exception)
                {
                    result.AddMessage("Falha ao conectar ao pagamento.");
                }
            }

            foreach (var item in warnings)
                result.AddMessage(item);

            return result;
        }
    }
}
