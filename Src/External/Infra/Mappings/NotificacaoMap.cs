﻿using FIAP.Pos.Tech.Challenge.Micro.Servico.Pedido.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FIAP.Pos.Tech.Challenge.Micro.Servico.Pedido.Infra.Mappings;

internal class NotificacaoMap : IEntityTypeConfiguration<Notificacao>
{
    public void Configure(EntityTypeBuilder<Notificacao> builder)
    {
        builder.HasKey(e => e.IdNotificacao).HasName("PK_notificacao_1");

        builder.ToTable("notificacao");

        builder.Property(e => e.IdNotificacao)
            .ValueGeneratedNever()
            .HasColumnName("id_notificacao");
        builder.Property(e => e.Data)
            .HasColumnType("datetime")
            .HasColumnName("data");
        builder.Property(e => e.IdDispositivo).HasColumnName("id_dispositivo");
        builder.Property(e => e.Mensagem)
            .HasMaxLength(50)
            .HasColumnName("mensagem");
    }
}
