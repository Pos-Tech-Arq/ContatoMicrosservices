using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RegiaoEntities = Regiao.Domain.Entities;

namespace Regiao.Infra.Contexts.Mappings
{
    public class RegiaoMapping : IEntityTypeConfiguration<RegiaoEntities.Regiao>
    {
        public void Configure(EntityTypeBuilder<RegiaoEntities.Regiao> builder)
        {
            builder.ToTable("Regioes");

            builder
                .Property(c => c.Id)
                .IsRequired();

            builder
                .HasKey(c => c.Id);

            builder.Property(c => c.Ddd)
                .HasColumnType("varchar(3)")
                .IsRequired();

            builder.Property(c => c.Estado)
                .HasColumnType("varchar(100)")
                .IsRequired();
        }
    }
}
