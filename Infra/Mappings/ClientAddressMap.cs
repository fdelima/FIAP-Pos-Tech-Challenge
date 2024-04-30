using FIAP.Pos.Tech.Challenge.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FIAP.Pos.Tech.Challenge.Infra.Mappings
{
    public class ClientAddressMap : IEntityTypeConfiguration<ClientAddress>
    {
        public void Configure(EntityTypeBuilder<ClientAddress> builder)
        {
            builder.HasKey(e => e.IdClientAddress);

            builder.ToTable("ClientAddress");

            builder.Property(e => e.IdClientAddress).HasDefaultValueSql("(newid())");
            builder.Property(e => e.AddressType)
                .HasMaxLength(50)
                .IsUnicode(false);
            builder.Property(e => e.City)
                .HasMaxLength(100)
                .IsUnicode(false);
            builder.Property(e => e.Neighborhood)
                .HasMaxLength(50)
                .IsUnicode(false);
            builder.Property(e => e.State)
                .HasMaxLength(2)
                .IsUnicode(false);
            builder.Property(e => e.Street)
                .HasMaxLength(200)
                .IsUnicode(false);
            builder.Property(e => e.StreetNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            builder.Property(e => e.ZipCode)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.HasOne(d => d.IdClientNavigation).WithMany(p => p.ClientAddresses)
                .HasForeignKey(d => d.IdClient)
                .HasConstraintName("FK_ClientAddress_Client");
        }
    }
}
