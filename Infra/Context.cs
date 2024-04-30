using FIAP.Pos.Tech.Challenge.Domain.Entities;
using FIAP.Pos.Tech.Challenge.Infra.Mappings;
using Microsoft.EntityFrameworkCore;

namespace FIAP.Pos.Tech.Challenge.Infra
{
    public partial class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
            : base(options) { }

        #region [ DbSets ]

        public virtual DbSet<Client> Clients { get; set; }

        public virtual DbSet<ClientAddress> ClientAddresses { get; set; }

        public virtual DbSet<ClientTelephone> ClientTelephones { get; set; }

        #endregion DbSets

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //TODO: Map :: 2 - Adicione sua configuração aqui
            modelBuilder.ApplyConfiguration(new ClientMap());
            modelBuilder.ApplyConfiguration(new ClientAddressMap());
            modelBuilder.ApplyConfiguration(new ClientTelephoneMap());

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}