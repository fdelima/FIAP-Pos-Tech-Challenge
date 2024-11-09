using FIAP.Pos.Tech.Challenge.Application.Controllers;
using FIAP.Pos.Tech.Challenge.Application.UseCases.Pedido.Commands;
using FIAP.Pos.Tech.Challenge.Domain;
using FIAP.Pos.Tech.Challenge.Domain.Entities;
using FIAP.Pos.Tech.Challenge.Domain.Models;
using FIAP.Pos.Tech.Challenge.Domain.Validator;
using FIAP.Pos.Tech.Challenge.Domain.ValuesObject;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using TestProject.MockData;

namespace TestProject.UnitTest.Aplication
{
    /// <summary>
    /// Classe de teste.
    /// </summary>
    public partial class PedidoControllerTest
    {
        private readonly IConfiguration _configuration;
        private readonly IMediator _mediator;
        private readonly IValidator<Pedido> _validator;

        /// <summary>
        /// Construtor da classe de teste.
        /// </summary>
        public PedidoControllerTest()
        {
            _configuration = Substitute.For<IConfiguration>();
            _mediator = Substitute.For<IMediator>();
            _validator = new PedidoValidator();            
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

            var aplicationController = new PedidoController(_configuration, _mediator, _validator);
            
            //Mockando retorno do mediator.
            _mediator.Send(Arg.Any<PedidoPostCommand>())
                .Returns(Task.FromResult(ModelResultFactory.SucessResult()));

            //Act
            var result = await aplicationController.PostAsync(pedido);

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

            var aplicationController = new PedidoController(_configuration, _mediator, _validator);
            
            //Act
            var result = await aplicationController.PostAsync(pedido);

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

            var aplicationController = new PedidoController(_configuration, _mediator, _validator);

            //Mockando retorno do mediator.
            _mediator.Send(Arg.Any<PedidoPutCommand>())
                .Returns(Task.FromResult(ModelResultFactory.SucessResult()));

            //Act
            var result = await aplicationController.PutAsync(idPedido, pedido);

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

            var aplicationController = new PedidoController(_configuration, _mediator, _validator);

            //Act
            var result = await aplicationController.PutAsync(idPedido, pedido);

            //Assert
            Assert.False(result.IsValid);
        }

        /// <summary>
        /// Testa a consulta por id
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Alteracao, true, 1)]
        public async void ConsultarPedidoPorId(Guid idPedido, Guid idDispositivo, ICollection<PedidoItem> items)
        {
            ///Arrange
            var pedido = new Pedido
            {
                IdPedido = idPedido,
                IdDispositivo = idDispositivo,
                PedidoItems = items
            };

            var aplicationController = new PedidoController(_configuration, _mediator, _validator);

            //Mockando retorno do mediator.
            _mediator.Send(Arg.Any<PedidoFindByIdCommand>())
                .Returns(Task.FromResult(ModelResultFactory.SucessResult(pedido)));

            //Act
            var result = await aplicationController.FindByIdAsync(idPedido);

            //Assert
            Assert.True(result.IsValid);
        }

        /// <summary>
        /// Testa a consulta Valida
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Consulta, true, 10)]
        public async void ConsultarPedidoComDadosValidos(List<Pedido> pedidos)
        {
            ///Arrange
            var aplicationController = new PedidoController(_configuration, _mediator, _validator);

            var param = new PagingQueryParam<Pedido>();

            //Mockando retorno do mediator.
            _mediator.Send(Arg.Any<PedidoGetListaCommand>())
                .Returns(Task.FromResult(new PagingQueryResult<Pedido>(pedidos,1,1)));

            //Act
            var result = await aplicationController.GetListaAsync(param);

            //Assert
            Assert.DoesNotContain(result.Content, x => x.Status.Equals(enmPedidoStatus.FINALIZADO.ToString()));
        }

        /// <summary>
        /// Testa a consulta Inválida
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Consulta, false, 10)]
        public async void ConsultarPedidoComDadosInvalidos(List<Pedido> pedidos)
        {
            ///Arrange
            var aplicationController = new PedidoController(_configuration, _mediator, _validator);

            var param = new PagingQueryParam<Pedido>();

            //Mockando retorno do mediator.
            _mediator.Send(Arg.Any<PedidoGetListaCommand>())
                .Returns(Task.FromResult(new PagingQueryResult<Pedido>(pedidos, 1, 1)));

            //Act
            var result = await aplicationController.GetListaAsync(param);

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
