using Eventos.IO.Domain.Organizadores;
using Eventos.IO.Infra.Data.EXTENSIONS;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eventos.IO.Infra.Data.MAPPINGS;

public class OrganizadorMapping : EntityTypeConfiguration<Organizador>
{
    public override void Map(EntityTypeBuilder<Organizador> builder)
    {
        builder.Property(e => e.Nome)
            .HasColumnType("varchar(150)")
            .IsRequired();

        builder.Property(e => e.Email)
            .HasColumnType("varchar(100)")
            .IsRequired();

        builder.Property(e => e.CPF)
            .HasColumnType("varchar(11)")
            .HasMaxLength(11)
            .IsRequired();

        builder
            .Ignore(e => e.ValidationResult);

        builder
            .Ignore(e => e.CascadeMode);

        builder
            .ToTable("Organizadores");
    }
}