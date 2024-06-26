﻿using FIAP.Pos.Tech.Challenge.Domain.Interfaces;
using System.Linq.Expressions;
using System.Text.Json.Serialization;

namespace FIAP.Pos.Tech.Challenge.Domain.Entities;

public partial class Cliente : IDomainEntity
{
    /// <summary>
    /// Retorna a regra de validação a ser utilizada na inserção.
    /// </summary>
    public Expression<Func<IDomainEntity, bool>> InsertDuplicatedRule()
    {
        return x => ((Cliente)x).Cpf.Equals(Cpf);
    }

    /// <summary>
    /// Retorna a regra de validação a ser utilizada na atualização.
    /// </summary>
    public Expression<Func<IDomainEntity, bool>> AlterDuplicatedRule()
    {
        return x => !((Cliente)x).IdCliente.Equals(IdCliente) &&
                    ((Cliente)x).Cpf.Equals(Cpf);
    }

    public Guid IdCliente { get; set; }

    public string Nome { get; set; } = null!;

    public string Email { get; set; } = null!;

    public long Cpf { get; set; }

    [JsonIgnore]
    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
}
