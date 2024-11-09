using FIAP.Pos.Tech.Challenge.Domain;
using FIAP.Pos.Tech.Challenge.Domain.Entities;
using FIAP.Pos.Tech.Challenge.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.Domain.Services;
using FIAP.Pos.Tech.Challenge.Domain.Validator;
using FIAP.Pos.Tech.Challenge.Domain.ValuesObject;
using FluentValidation;
using NSubstitute;
using System.Linq.Expressions;
using TestProject.MockData;

namespace TestProject.UnitTest.Domain
{
    /// <summary>
    /// Classe de teste.
    /// </summary>
    public partial class PedidoServiceTest
    {
        private readonly IGateways<Pedido> _gatewayPedidoMock;
        private readonly IValidator<Pedido> _validator;
        private readonly IGateways<Notificacao> _notificacaoGatewayMock;

        /// <summary>
        /// Construtor da classe de teste.
        /// </summary>
        public PedidoServiceTest()
        {
            _validator = new PedidoValidator();
            _gatewayPedidoMock = Substitute.For<IGateways<Pedido>>();
            _notificacaoGatewayMock = Substitute.For<IGateways<Notificacao>>();
        }

        /// <summary>
        /// Testa a inserção com dados válidos
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Inclusao, true, 3)]
        public async void InserirComDadosValidos(Guid idDispositivo, ICollection<PedidoItem> items)
        {
            ///Arrange
            var pedido = new Pedido
            {
                IdDispositivo = idDispositivo,
                PedidoItems = items
            };

            var domainService = new PedidoService(_gatewayPedidoMock, _validator, _notificacaoGatewayMock);


            //Act
            var result = await domainService.InsertAsync(pedido);

            //Assert
            Assert.True(result.IsValid);
        }

        /// <summary>
        /// Testa a inserção com dados inválidos
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Inclusao, false, 3)]
        public async void InserirComDadosInvalidos(Guid idDispositivo, ICollection<PedidoItem> items)
        {
            ///Arrange
            var pedido = new Pedido
            {
                IdDispositivo = idDispositivo,
                PedidoItems = items
            };

            var domainService = new PedidoService(_gatewayPedidoMock, _validator, _notificacaoGatewayMock);

            //Act
            var result = await domainService.InsertAsync(pedido);

            //Assert
            Assert.False(result.IsValid);

        }

        /// <summary>
        /// Testa a alteração com dados válidos
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Alteracao, true, 3)]
        public async void AlterarComDadosValidos(Guid idPedido, Guid idDispositivo, ICollection<PedidoItem> items)
        {
            ///Arrange
            var pedido = new Pedido
            {
                IdPedido = idPedido,
                IdDispositivo = idDispositivo,
                PedidoItems = items
            };

            var domainService = new PedidoService(_gatewayPedidoMock, _validator, _notificacaoGatewayMock);

            //Mockando retorno do metodo interno do UpdateAsync
            _gatewayPedidoMock.FirstOrDefaultWithIncludeAsync(Arg.Any<Expression<Func<Pedido, ICollection<PedidoItem>>>>(), Arg.Any<Expression<Func<Pedido, bool>>>())
                .Returns(new ValueTask<Pedido>(pedido));

            //Act
            var result = await domainService.UpdateAsync(pedido);

            //Assert
            Assert.True(result.IsValid);
        }

        /// <summary>
        /// Testa a alteração com dados inválidos
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Alteracao, false, 3)]
        public async void AlterarComDadosInvalidos(Guid idPedido, Guid idDispositivo, ICollection<PedidoItem> items)
        {
            ///Arrange
            var pedido = new Pedido
            {
                IdPedido = idPedido,
                IdDispositivo = idDispositivo,
                PedidoItems = items
            };

            var domainService = new PedidoService(_gatewayPedidoMock, _validator, _notificacaoGatewayMock);

            //Mockando retorno do metodo interno do UpdateAsync
            _gatewayPedidoMock.FirstOrDefaultWithIncludeAsync(Arg.Any<Expression<Func<Pedido, ICollection<PedidoItem>>>>(), Arg.Any<Expression<Func<Pedido, bool>>>())
                .Returns(new ValueTask<Pedido>(pedido));

            //Act
            var result = await domainService.UpdateAsync(pedido);

            //Assert
            Assert.False(result.IsValid);
        }

        /// <summary>
        /// Testa a consulta por id
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Inclusao, true, 1)]
        public async void ConsultarPedidoPorId(Guid idDispositivo, ICollection<PedidoItem> items)
        {
            ///Arrange
            var pedido = new Pedido
            {
                IdDispositivo = idDispositivo,
                PedidoItems = items
            };

            var domainService = new PedidoService(_gatewayPedidoMock, _validator, _notificacaoGatewayMock);
            var insertResult = await domainService.InsertAsync(pedido);

            //Mockando retorno do metodo interno do FindByIdAsync
            _gatewayPedidoMock.FirstOrDefaultWithIncludeAsync(Arg.Any<Expression<Func<Pedido, ICollection<PedidoItem>>>>(), Arg.Any<Expression<Func<Pedido, bool>>>())
                .Returns(new ValueTask<Pedido>(pedido));

            //Act
            var result = await domainService.FindByIdAsync(((Pedido)insertResult.Model).IdPedido);

            //Assert
            Assert.True(result.IsValid);
        }

        /// <summary>
        /// Testa a consulta Valida
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Consulta, true, 10)]
        public async void ConsultarPedidoComDadosValidos(IEnumerable<Pedido> pedidos)
        {
            ///Arrange
            var domainService = new PedidoService(_gatewayPedidoMock, _validator, _notificacaoGatewayMock);

            var param = new PagingQueryParam<Pedido>();

            //Mockando retorno do metodo interno do GetItemsAsync
            _gatewayPedidoMock.GetItemsAsync(Arg.Any<PagingQueryParam<Pedido>>(),
                Arg.Any<Expression<Func<Pedido, bool>>>(),
                Arg.Any<Expression<Func<Pedido, object>>>())
                .Returns(new ValueTask<PagingQueryResult<Pedido>>(new PagingQueryResult<Pedido>(new List<Pedido>(pedidos))));


            //Act
            var result = await domainService.GetListaAsync(param);

            //Assert
            Assert.DoesNotContain(result.Content, x => x.Status.Equals(enmPedidoStatus.FINALIZADO.ToString()));
        }

        /// <summary>
        /// Testa a consulta Inválida
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Consulta, false, 10)]
        public async void ConsultarPedidoComDadosInvalidos(IEnumerable<Pedido> pedidos)
        {
            ///Arrange
            var domainService = new PedidoService(_gatewayPedidoMock, _validator, _notificacaoGatewayMock);

            var param = new PagingQueryParam<Pedido>();

            //Mockando retorno do metodo interno do GetItemsAsync
            _gatewayPedidoMock.GetItemsAsync(Arg.Any<PagingQueryParam<Pedido>>(),
                Arg.Any<Expression<Func<Pedido, bool>>>(),
                Arg.Any<Expression<Func<Pedido, object>>>())
                .Returns(new ValueTask<PagingQueryResult<Pedido>>(new PagingQueryResult<Pedido>(new List<Pedido>(pedidos))));


            //Act
            var result = await domainService.GetListaAsync(param);

            //Assert
            Assert.Contains(result.Content, x => x.Status.Equals(enmPedidoStatus.FINALIZADO.ToString()));
        }

        #region [ Xunit MemberData ]

        /// <summary>
        /// Mock de dados
        /// </summary>
        public static IEnumerable<object[]> ObterDados(enmTipo tipo, bool dadosValidos, int quantidade)
        {
            switch (tipo)
            {
                case enmTipo.Inclusao:
                    if (dadosValidos)
                        return PedidoMock.ObterDadosValidos(quantidade);
                    else
                        return PedidoMock.ObterDadosInvalidos(quantidade);
                case enmTipo.Alteracao:
                    if (dadosValidos)
                        return PedidoMock.ObterDadosValidos(quantidade)
                            .Select(i => new object[] { Guid.NewGuid() }.Concat(i).ToArray());
                    else
                        return PedidoMock.ObterDadosInvalidos(quantidade)
                            .Select(i => new object[] { Guid.NewGuid() }.Concat(i).ToArray());
                case enmTipo.Consulta:
                    if (dadosValidos)
                        return PedidoMock.ObterDadosConsultaValidos(quantidade);
                    else
                        return PedidoMock.ObterDadosConsultaInValidos(quantidade);
                default:
                    return null;
            }
        }

        #endregion

    }
}
