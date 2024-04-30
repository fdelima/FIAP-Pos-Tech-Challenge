using FIAP.Pos.Tech.Challenge.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FIAP.Pos.Tech.Challenge.Infra.Mappings
{
    public class ClientMap : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasKey(e => e.IdClient);

            builder.ToTable("Client");

            builder.Property(e => e.IdClient).HasDefaultValueSql("(newid())");
            builder.Property(e => e.BirthDate).HasColumnType("date");
            builder.Property(e => e.Email)
                .HasMaxLength(200)
                .IsUnicode(false);
            builder.Property(e => e.Gender)
                .HasMaxLength(50)
                .IsUnicode(false);
            builder.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            builder.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            builder.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
        }
    }
}
