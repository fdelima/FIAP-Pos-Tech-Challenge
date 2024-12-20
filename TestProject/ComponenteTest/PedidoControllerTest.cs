﻿using FIAP.Pos.Tech.Challenge.Micro.Servico.Pedido.Domain;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Pedido.Domain.Entities;
using Newtonsoft.Json;
using System.Net.Http.Json;
using TestProject.Infra;
using Xunit.Gherkin.Quick;

namespace TestProject.ComponenteTest
{
    /// <summary>
    /// Classe de teste.
    /// </summary>
    [FeatureFile("./BDD/Features/ControlarPedidos.feature")]
    public class PedidoControllerTest : Feature, IClassFixture<ComponentTestsBase>
    {
        private readonly ApiTestFixture _apiTest;
        Pedido _pedido;

        /// <summary>
        /// Construtor da classe de teste.
        /// </summary>
        public PedidoControllerTest(ComponentTestsBase data)
        {
            _apiTest = data._apiTest;
        }
        private class ActionResult
        {
            public List<string> Messages { get; set; }
            public List<string> Errors { get; set; }
            public Pedido Model { get; set; }
            public bool IsValid { get; set; }
        }

        [Given(@"Recebendo um pedido na lanchonete vamos preparar o pedido")]
        public void PrepararPedido()
        {
            _pedido = new Pedido();
            _pedido.IdDispositivo = Guid.NewGuid();
            _pedido.PedidoItems.Add(new PedidoItem()
            {
                IdProduto = Guid.Parse("f724910b-ed6d-41a2-ab52-da4cd26ba413"),
                Quantidade = 1
            });
            _pedido.PedidoItems.Add(new PedidoItem
            {
                IdProduto = Guid.Parse("802be132-64ef-4737-9de7-c83298c70a73"),
                Quantidade = 1
            });
            _pedido.PedidoItems.Add(new PedidoItem
            {
                IdProduto = Guid.Parse("f44b20ab-a453-4579-accf-d94d7075f508"),
                Quantidade = 1
            });
        }

        [And(@"Adicionar o pedido")]
        public async Task AdicionarPedido()
        {
            var client = _apiTest.GetClient();
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "api/pedido/checkout", _pedido);

            var responseContent = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<ActionResult>(responseContent);

            _pedido = actualResult.Model;

            Assert.True(actualResult.IsValid);
        }

        [And(@"Encontrar o pedido")]
        public async Task EncontrarPedido()
        {
            var client = _apiTest.GetClient();
            HttpResponseMessage response = await client.GetAsync(
                $"api/pedido/{_pedido.IdPedido}");

            var responseContent = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<ActionResult>(responseContent);
            _pedido = actualResult.Model;

            Assert.True(actualResult.IsValid);
        }

        [And(@"Alterar o pedido")]
        public async Task AlterarPedido()
        {
            var client = _apiTest.GetClient();
            HttpResponseMessage response = await client.PutAsJsonAsync(
                $"api/pedido/{_pedido.IdPedido}", _pedido);

            var responseContent = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<ActionResult>(responseContent);
            _pedido = actualResult.Model;

            Assert.True(actualResult.IsValid);
        }

        [And(@"Consultar o pedido")]
        public async Task ConsultarPedido()
        {
            var client = _apiTest.GetClient();
            HttpResponseMessage response = await client.PostAsJsonAsync(
                $"api/pedido/consult", new PagingQueryParam<Pedido> { ObjFilter = _pedido });

            var responseContent = await response.Content.ReadAsStringAsync();
            dynamic actualResult = JsonConvert.DeserializeObject(responseContent);

            Assert.True(actualResult.content != null);
        }

        [When(@"Listar o pedido")]
        public async Task ListarPardido()
        {
            var client = _apiTest.GetClient();
            HttpResponseMessage response = await client.GetAsync(
                $"api/pedido/Lista");

            var responseContent = await response.Content.ReadAsStringAsync();
            dynamic actualResult = JsonConvert.DeserializeObject(responseContent);

            Assert.True(actualResult.content != null);
        }

        [Then(@"posso deletar o pedido")]
        public async Task DeletarPedido()
        {
            var client = _apiTest.GetClient();
            HttpResponseMessage response = await client.DeleteAsync(
                $"api/pedido/{_pedido.IdPedido}");

            var responseContent = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<ActionResult>(responseContent);
            _pedido = null;

            Assert.True(actualResult.IsValid);
        }
    }
}
