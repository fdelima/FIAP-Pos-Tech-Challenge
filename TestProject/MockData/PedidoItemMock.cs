using FIAP.Pos.Tech.Challenge.Micro.Servico.Pedido.Domain;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Pedido.Domain.Entities;

namespace TestProject.MockData;

public class PedidoItemMock
{
    /// <summary>
    /// Mock de dados válidos
    /// </summary>
    public static IEnumerable<object[]> ObterDadosValidos(int quantidade)
    {
        for (var index = 1; index <= quantidade; index++)
            yield return new object[]
            {
                DateTime.Now,
                Guid.NewGuid(),
                Guid.NewGuid(),
                1
            };
    }

    /// <summary>
    /// Mock de dados inválidos
    /// </summary>
    public static IEnumerable<object[]> ObterDadosInvalidos(int quantidade)
    {
        for (var index = 1; index <= quantidade; index++)
            yield return new object[]
            {
                DateTime.Now,
                Guid.Empty,
                Guid.Empty,
                0
            };
    }

    /// <summary>
    /// Mock de dados válidos
    /// </summary>
    public static IEnumerable<object[]> ObterDadosConsultaValidos(int quantidade)
    {
        for (var index = 1; index <= quantidade; index++)
        {
            var notificacoes = new List<PedidoItem>();
            for (var index2 = 1; index <= quantidade; index++)
            {
                notificacoes.Add(new PedidoItem
                {
                    IdPedidoItem = Guid.NewGuid(),
                    Data = DateTime.Now,
                    IdPedido = Guid.NewGuid(),
                    IdProduto = Guid.NewGuid(),
                    Quantidade = 1
                });
            }
            var param = new PagingQueryParam<PedidoItem>() { CurrentPage = 1, Take = 10 };
            yield return new object[]
            {
                param,
                notificacoes
            };
        }
    }

    /// <summary>
    /// Mock de dados inválidos
    /// </summary>
    public static IEnumerable<object[]> ObterDadosConsultaInValidos(int quantidade)
    {
        for (var index = 1; index <= quantidade; index++)
        {
            var notificacoes = new List<PedidoItem>();
            for (var index2 = 1; index2 <= quantidade; index2++)
            {
                notificacoes.Add(new PedidoItem
                {
                    IdPedidoItem = Guid.Empty,
                    Data = DateTime.Now,
                    IdPedido = Guid.Empty,
                    IdProduto = Guid.Empty,
                    Quantidade = 0
                });
            }
            var param = new PagingQueryParam<PedidoItem>() { CurrentPage = 1, Take = 10 };
            yield return new object[]
            {
                param,
                notificacoes
            };
        }
    }

    public static IEnumerable<object[]> ObterDadosConsultaPorIdValidos(int quantidade)
    {
        for (var index = 1; index <= quantidade; index++)
            yield return new object[]
            {
                Guid.NewGuid()
            };
    }

    public static IEnumerable<object[]> ObterDadosConsultaPorIdInvalidos(int quantidade)
    {
        for (var index = 1; index <= quantidade; index++)
            yield return new object[]
            {
                Guid.Empty
            };
    }
}