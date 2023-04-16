using Eventos.IO.Domain.Eventos;
using Eventos.IO.Infra.Data.EXTENSIONS;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eventos.IO.Infra.Data.MAPPINGS;

public class CategoriaMapping : EntityTypeConfiguration<Categoria>
{
    public override void Map(EntityTypeBuilder<Categoria> builder)
    {
        builder
            .Property(e => e.Nome)
            .HasColumnType("varchar(150)")
            .IsRequired();

        builder
            .Ignore(e => e.ValidationResult);

        builder
            .Ignore(e => e.CascadeMode);

        builder
            .ToTable("Categorias");
    }
}