//using FIAP.Pos.Tech.Challenge.Domain;
//using FIAP.Pos.Tech.Challenge.Domain.Entities;
//using FIAP.Pos.Tech.Challenge.Domain.Interfaces;
//using FIAP.Pos.Tech.Challenge.Domain.Services;
//using FIAP.Pos.Tech.Challenge.Domain.Validator;
//using FIAP.Pos.Tech.Challenge.Domain.ValuesObject;
//using FluentValidation;
//using NSubstitute;
//using System.Linq.Expressions;
//using TestProject.MockData;
//using FIAP.Pos.Tech.Challenge.Infra.Gateways;
//using FIAP.Pos.Tech.Challenge.Infra;
//using Microsoft.EntityFrameworkCore;

//namespace TestProject.IntegrationTest.External
//{
//    /// <summary>
//    /// Classe de teste.
//    /// </summary>
//    public partial class PedidoGatewayTest
//    {
//        private readonly IGateways<Pedido> _pedidoGateway;
//        private readonly Context _context;
//        private readonly DbContextOptions<Context> _options;
//        private const string conn = "Server=sqlserver; Database=tech-challenge-micro-servico-pedido-grupo-71; User ID=sa; Password=SqlServer2019!; MultipleActiveResultSets=true; TrustServerCertificate=True";

//        /// <summary>
//        /// Construtor da classe de teste.
//        /// </summary>
//        public PedidoGatewayTest()
//        {
//            var options = new DbContextOptionsBuilder<Context>()
//                            .UseSqlServer(conn).Options;

//            _context = new Context(options);

//            _pedidoGateway = new BaseGateway<Pedido>(_context);
//        }

//        /// <summary>
//        /// Testa a inserção com dados válidos
//        /// </summary>
//        [Theory]
//        [MemberData(nameof(ObterDados), enmTipo.Inclusao, true, 3)]
//        public async void InserirComDadosValidos(Guid idDispositivo, ICollection<PedidoItem> items)
//        {
//            ///Arrange
//            var pedido = new Pedido
//            {
//                IdDispositivo = idDispositivo,
//                PedidoItems = items
//            };

//            //Act
//            var result = await _pedidoGateway.InsertAsync(pedido);

//            //Assert
//            Assert.True(result != null);
//        }

//        /// <summary>
//        /// Testa a inserção com dados inválidos
//        /// </summary>
//        [Theory]
//        [MemberData(nameof(ObterDados), enmTipo.Inclusao, false, 3)]
//        public async void InserirComDadosInvalidos(Guid idDispositivo, ICollection<PedidoItem> items)
//        {
//            ///Arrange
//            var pedido = new Pedido
//            {
//                IdDispositivo = idDispositivo,
//                PedidoItems = items
//            };

//            //Act
//            var result = await _pedidoGateway.InsertAsync(pedido);

//            //Assert
//            Assert.True(result == null);

//        }

//        /// <summary>
//        /// Testa a alteração com dados válidos
//        /// </summary>
//        [Theory]
//        [MemberData(nameof(ObterDados), enmTipo.Alteracao, true, 3)]
//        public async void AlterarComDadosValidos(Guid idPedido, Guid idDispositivo, ICollection<PedidoItem> items)
//        {
//            ///Arrange
//            var pedido = new Pedido
//            {
//                IdPedido = idPedido,
//                IdDispositivo = idDispositivo,
//                PedidoItems = items
//            };

//            //Act
//            await _pedidoGateway.UpdateAsync(pedido);

//            //Assert
//            Assert.True(true);
//        }

//        /// <summary>
//        /// Testa a alteração com dados inválidos
//        /// </summary>
//        [Theory]
//        [MemberData(nameof(ObterDados), enmTipo.Alteracao, false, 3)]
//        public async void AlterarComDadosInvalidos(Guid idPedido, Guid idDispositivo, ICollection<PedidoItem> items)
//        {
//            ///Arrange
//            var pedido = new Pedido
//            {
//                IdPedido = idPedido,
//                IdDispositivo = idDispositivo,
//                PedidoItems = items
//            };

//            try
//            {
//                //Act
//                await _pedidoGateway.UpdateAsync(pedido);

//                //Assert
//                Assert.True(false);
//            }
//            catch (Exception)
//            {
//                //Assert
//                Assert.True(true);
//            }
//        }

//        /// <summary>
//        /// Testa a consulta por id
//        /// </summary>
//        [Theory]
//        [MemberData(nameof(ObterDados), enmTipo.Alteracao, true, 1)]
//        public async void ConsultarPedidoPorId(Guid idPedido, Guid idDispositivo, ICollection<PedidoItem> items)
//        {
//            ///Arrange
//            var pedido = new Pedido
//            {
//                IdPedido = idPedido,
//                IdDispositivo = idDispositivo,
//                PedidoItems = items
//            };

//            //Act
//            var result = await _pedidoGateway.FindByIdAsync(idPedido);

//            //Assert
//            Assert.True(result == null);
//        }

//        /// <summary>
//        /// Testa a consulta Valida
//        /// </summary>
//        [Theory]
//        [MemberData(nameof(ObterDados), enmTipo.Consulta, true, 10)]
//        public async void ConsultarPedidoComDadosValidos(IEnumerable<Pedido> pedidos)
//        {
//            ///Arrange
//            var domainService = new PedidoService(_gatewayPedidoMock, _validator, _notificacaoGatewayMock);

//            var param = new PagingQueryParam<Pedido>();

//            //Mockando retorno do metodo interno do GetItemsAsync
//            _gatewayPedidoMock.GetItemsAsync(Arg.Any<PagingQueryParam<Pedido>>(),
//                Arg.Any<Expression<Func<Pedido, bool>>>(),
//                Arg.Any<Expression<Func<Pedido, object>>>())
//                .Returns(new ValueTask<PagingQueryResult<Pedido>>(new PagingQueryResult<Pedido>(new List<Pedido>(pedidos))));


//            //Act
//            var result = await domainService.GetListaAsync(param);

//            //Assert
//            Assert.DoesNotContain(result.Content, x => x.Status.Equals(enmPedidoStatus.FINALIZADO.ToString()));
//        }

//        /// <summary>
//        /// Testa a consulta Inválida
//        /// </summary>
//        [Theory]
//        [MemberData(nameof(ObterDados), enmTipo.Consulta, false, 10)]
//        public async void ConsultarPedidoComDadosInvalidos(IEnumerable<Pedido> pedidos)
//        {
//            ///Arrange
//            var domainService = new PedidoService(_gatewayPedidoMock, _validator, _notificacaoGatewayMock);

//            var param = new PagingQueryParam<Pedido>();

//            //Mockando retorno do metodo interno do GetItemsAsync
//            _gatewayPedidoMock.GetItemsAsync(Arg.Any<PagingQueryParam<Pedido>>(),
//                Arg.Any<Expression<Func<Pedido, bool>>>(),
//                Arg.Any<Expression<Func<Pedido, object>>>())
//                .Returns(new ValueTask<PagingQueryResult<Pedido>>(new PagingQueryResult<Pedido>(new List<Pedido>(pedidos))));


//            //Act
//            var result = await domainService.GetListaAsync(param);

//            //Assert
//            Assert.Contains(result.Content, x => x.Status.Equals(enmPedidoStatus.FINALIZADO.ToString()));
//        }

//        #region [ Xunit MemberData ]

//        /// <summary>
//        /// Mock de dados
//        /// </summary>
//        public static IEnumerable<object[]> ObterDados(enmTipo tipo, bool dadosValidos, int quantidade)
//        {
//            switch (tipo)
//            {
//                case enmTipo.Inclusao:
//                    if (dadosValidos)
//                        return PedidoMock.ObterDadosValidos(quantidade);
//                    else
//                        return PedidoMock.ObterDadosInvalidos(quantidade);
//                case enmTipo.Alteracao:
//                    if (dadosValidos)
//                        return PedidoMock.ObterDadosValidos(quantidade)
//                            .Select(i => new object[] { Guid.NewGuid() }.Concat(i).ToArray());
//                    else
//                        return PedidoMock.ObterDadosInvalidos(quantidade)
//                            .Select(i => new object[] { Guid.NewGuid() }.Concat(i).ToArray());
//                case enmTipo.Consulta:
//                    if (dadosValidos)
//                        return PedidoMock.ObterDadosConsultaValidos(quantidade);
//                    else
//                        return PedidoMock.ObterDadosConsultaInValidos(quantidade);
//                default:
//                    return null;
//            }
//        }

//        #endregion

//    }
//}
