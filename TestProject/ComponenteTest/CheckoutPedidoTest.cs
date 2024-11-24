using FIAP.Pos.Tech.Challenge.Micro.Servico.Pedido.Domain.Entities;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Pedido.Domain.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using TestProject.IntegrationTest.Infra;
using Xunit.Gherkin.Quick;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TestProject.ComponenteTest
{
    [FeatureFile("./BDD/Features/CheckoutPedido.feature")]
    public class CheckoutPedidoTest : Feature, IClassFixture<TestsBase>
    {
        private readonly SqlServerTestFixture _sqlserverTest;
        private readonly ApiTestFixture _apiTest;

        private ModelResult expectedResult;
        Pedido _pedido;

        /// <summary>
        /// Construtor da classe de teste.
        /// </summary>
        public CheckoutPedidoTest(TestsBase data)
        {
            _sqlserverTest = data._sqlserverTest;
            _apiTest = data._apiTest;
        }

        [Given(@"Recebendo um pedido com um hamburguer")]
        public void RecebendoPedidoComHamburguer()
        {
            _pedido = new Pedido();
            _pedido.IdDispositivo = Guid.NewGuid();
            _pedido.PedidoItems.Add(new PedidoItem()
            {
                IdProduto = Guid.Parse("f724910b-ed6d-41a2-ab52-da4cd26ba413"),
                Quantidade = 1
            });
        }

        [And(@"e uma coca-cola")]
        public void EuAdicionandoCocaCola()
        {
            _pedido.PedidoItems.Add(new PedidoItem
            {
                IdProduto = Guid.Parse("802be132-64ef-4737-9de7-c83298c70a73"),
                Quantidade = 1
            });
        }

        [And(@"e uma batata frita")]
        public void EuAdicionandoBatataFrita()
        {
            _pedido.PedidoItems.Add(new PedidoItem
            {
                IdProduto = Guid.Parse("f44b20ab-a453-4579-accf-d94d7075f508"),
                Quantidade = 1
            });
        }

        [When(@"quando eu fizer o checkout")]
        public void EuFazendoCheckout()
        {
            Console.WriteLine("checkout do pedido na api");
        }

        [Then(@"o resultado esperado é o cadastro do pedido com sucesso")]
        public async Task OResultadoDoCheckout()
        {
            expectedResult = ModelResultFactory.InsertSucessResult<Pedido>(_pedido);

            var client = _apiTest.GetClient();
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "api/pedido/checkout", _pedido);

            var responseContent = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<ModelResult>(responseContent);


            Assert.Equal(expectedResult.IsValid, actualResult.IsValid);
            Assert.Equal(expectedResult.Messages, actualResult.Messages);
            Assert.Equal(expectedResult.Errors, actualResult.Errors);
        }
    }
}
