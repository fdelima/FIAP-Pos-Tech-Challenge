﻿using FIAP.Pos.Tech.Challenge.Domain.Entities;
using FIAP.Pos.Tech.Challenge.Infra.Mappings;
using Microsoft.EntityFrameworkCore;

namespace FIAP.Pos.Tech.Challenge.Infra
{
    public partial class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
            : base(options) { }

        #region [ DbSets ]

        public virtual DbSet<Cliente> Clientes { get; set; }

        public virtual DbSet<Dispositivo> Dispositivos { get; set; }

        public virtual DbSet<Notificacao> Notificacaos { get; set; }

        public virtual DbSet<Pedido> Pedidos { get; set; }

        public virtual DbSet<PedidoItem> PedidoItems { get; set; }

        public virtual DbSet<Produto> Produtos { get; set; }

        public virtual DbSet<ProdutoImagens> ProdutoImagens { get; set; }


        #endregion DbSets

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //TODO: Map :: 2 - Adicione sua configuração aqui
            modelBuilder.ApplyConfiguration(new ClienteMap());
            modelBuilder.ApplyConfiguration(new DispositivoMap());
            modelBuilder.ApplyConfiguration(new NotificacaoMap());
            modelBuilder.ApplyConfiguration(new PedidoItemMap());
            modelBuilder.ApplyConfiguration(new PedidoMap());
            modelBuilder.ApplyConfiguration(new ProdutoMap());
            modelBuilder.ApplyConfiguration(new ProdutoImagensMap());

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}