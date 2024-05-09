using FIAP.Pos.Tech.Challenge.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.Domain.ValuesObject;
using System.Linq.Expressions;
using System.Text.Json.Serialization;

namespace FIAP.Pos.Tech.Challenge.Domain.Entities;

public partial class Produto : IDomainEntity
{
    /// <summary>
    /// Retorna a regra de validação a ser utilizada na inserção.
    /// </summary>
    public Expression<Func<IDomainEntity, bool>> InsertDuplicatedRule()
    {
        return x => ((Produto)x).Nome.Equals(Nome);
    }

    /// <summary>
    /// Retorna a regra de validação a ser utilizada na atualização.
    /// </summary>
    public Expression<Func<IDomainEntity, bool>> AlterDuplicatedRule()
    {
        return x => !((Produto)x).IdProduto.Equals(IdProduto) &&
                     ((Produto)x).Nome.Equals(Nome);
    }
    public Guid IdProduto { get; set; }

    public string Nome { get; set; } = null!;

    public decimal Preco { get; set; }

    public string Descricao { get; set; } = null!;

    public enmProdutoCategoria Categoria { get; set; }

    [JsonIgnore]
    public virtual ICollection<PedidoItem> PedidoItems { get; set; } = new List<PedidoItem>();
}
