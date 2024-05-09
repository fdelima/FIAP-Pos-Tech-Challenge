using FIAP.Pos.Tech.Challenge.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.Domain.ValuesObject;
using System.Linq.Expressions;
using System.Text.Json.Serialization;

namespace FIAP.Pos.Tech.Challenge.Domain.Entities;

public partial class Pedido : IDomainEntity
{
    /// <summary>
    /// Retorna a regra de validação a ser utilizada na inserção.
    /// </summary>
    public Expression<Func<IDomainEntity, bool>> InsertDuplicatedRule()
    {
        return x => ((Pedido)x).IdCliente.Equals(IdCliente) &&
                    ((Pedido)x).IdDispositivo.Equals(IdDispositivo) &&
                    ((Pedido)x).Data.ToString("yyyyMMddHHmm").Equals(Data.ToString("yyyyMMddHHmm"));
    }

    /// <summary>
    /// Retorna a regra de validação a ser utilizada na atualização.
    /// </summary>
    public Expression<Func<IDomainEntity, bool>> AlterDuplicatedRule()
    {
        return x => !((Pedido)x).IdPedido.Equals(IdPedido) &&
                    ((Pedido)x).IdCliente.Equals(IdCliente) &&
                    ((Pedido)x).IdDispositivo.Equals(IdDispositivo) &&
                    ((Pedido)x).Data.ToString("yyyyMMddHHmm").Equals(Data.ToString("yyyyMMddHHmm"));
    }

    public Guid IdPedido { get; set; }

    public DateTime Data { get; set; }

    public Guid IdDispositivo { get; set; }

    public Guid? IdCliente { get; set; }

    public enmPedidoStatus Status { get; set; }

    public DateTime DataStatusPedido { get; set; }

    [JsonIgnore]
    public virtual Cliente? IdClienteNavigation { get; set; }

    [JsonIgnore]
    public virtual Dispositivo IdDispositivoNavigation { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<PedidoItem> PedidoItems { get; set; } = new List<PedidoItem>();
}
