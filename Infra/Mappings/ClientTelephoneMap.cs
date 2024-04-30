using FIAP.Pos.Tech.Challenge.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FIAP.Pos.Tech.Challenge.Infra.Mappings
{
    public class ClientTelephoneMap : IEntityTypeConfiguration<ClientTelephone>
    {
        public void Configure(EntityTypeBuilder<ClientTelephone> builder)
        {
            builder.HasKey(e => e.IdClientTelephone);

            builder.ToTable("ClientTelephone");

            builder.Property(e => e.IdClientTelephone).HasDefaultValueSql("(newid())");
            builder.Property(e => e.Number)
                .HasMaxLength(50)
                .IsUnicode(false);
            builder.Property(e => e.TelephoneType)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.HasOne(d => d.IdClientNavigation).WithMany(p => p.ClientTelephones)
                .HasForeignKey(d => d.IdClient)
                .HasConstraintName("FK_ClientTelephone_Client");
        }
    }
}
