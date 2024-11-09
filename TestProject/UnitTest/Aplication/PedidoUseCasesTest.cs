using FIAP.Pos.Tech.Challenge.Application.Controllers;
using FIAP.Pos.Tech.Challenge.Application.UseCases.Pedido.Commands;
using FIAP.Pos.Tech.Challenge.Application.UseCases.Pedido.Handlers;
using FIAP.Pos.Tech.Challenge.Domain;
using FIAP.Pos.Tech.Challenge.Domain.Entities;
using FIAP.Pos.Tech.Challenge.Domain.Interfaces;
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
    public partial class PedidoUseCasesTest
    {
        private readonly IPedidoService _service;

        /// <summary>
        /// Construtor da classe de teste.
        /// </summary>
        public PedidoUseCasesTest()
        {
            _service = Substitute.For<IPedidoService>();
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

            var command = new PedidoPostCommand(pedido);

            //Mockando retorno do mediator.
            _service.InsertAsync(pedido)
                .Returns(Task.FromResult(ModelResultFactory.SucessResult(pedido)));

            //Act
            var handler = new PedidoPostHandler(_service);
            var result = await handler.Handle(command, CancellationToken.None);

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

            var command = new PedidoPostCommand(pedido);

            //Mockando retorno do mediator.
            _service.InsertAsync(pedido)
                .Returns(Task.FromResult(ModelResultFactory.NotFoundResult<Pedido>()));

            //Act
            var handler = new PedidoPostHandler(_service);
            var result = await handler.Handle(command, CancellationToken.None);

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

            var command = new PedidoPutCommand(idPedido, pedido);

            //Mockando retorno do mediator.
            _service.UpdateAsync(pedido)
                .Returns(Task.FromResult(ModelResultFactory.SucessResult()));

            //Act
            var handler = new PedidoPutHandler(_service);
            var result = await handler.Handle(command, CancellationToken.None);

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

            var command = new PedidoPutCommand(idPedido, pedido);

            //Mockando retorno do mediator.
            _service.UpdateAsync(pedido)
                .Returns(Task.FromResult(ModelResultFactory.NotFoundResult<Pedido>()));

            //Act
            var handler = new PedidoPutHandler(_service);
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.False(result.IsValid);
        }

        /// <summary>
        /// Testa a consulta por id
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.ConsultaPorId, true, 1)]
        public async void ConsultarPedidoPorId(Guid idPedido)
        {
            ///Arrange
            var command = new PedidoFindByIdCommand(idPedido);

            //Mockando retorno do mediator.
            _service.FindByIdAsync(idPedido)
                .Returns(Task.FromResult(ModelResultFactory.SucessResult()));

            //Act
            var handler = new PedidoFindByIdHandler(_service);
            var result = await handler.Handle(command, CancellationToken.None);

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
            var param = new PagingQueryParam<Pedido>();
            var command = new PedidoGetListaCommand(param);

            //Mockando retorno do mediator.
            _service.GetListaAsync(param)
                .Returns(new ValueTask<PagingQueryResult<Pedido>>(new PagingQueryResult<Pedido>(new List<Pedido>(pedidos))));

            //Act
            var handler = new PedidoGetIListaHandler(_service);
            var result = await handler.Handle(command, CancellationToken.None);

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
            var param = new PagingQueryParam<Pedido>();
            var command = new PedidoGetListaCommand(param);

            //Mockando retorno do mediator.
            _service.GetListaAsync(param)
                .Returns(new ValueTask<PagingQueryResult<Pedido>>(new PagingQueryResult<Pedido>(new List<Pedido>(pedidos))));

            //Act
            var handler = new PedidoGetIListaHandler(_service);
            var result = await handler.Handle(command, CancellationToken.None);

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
                case enmTipo.ConsultaPorId:
                    if (dadosValidos)
                        return PedidoMock.ObterDadosConsultaPorIdValidos(quantidade);
                    else
                        return PedidoMock.ObterDadosConsultaPorIdInvalidos(quantidade);
                default:
                    return null;
            }
        }

        #endregion

    }
}
