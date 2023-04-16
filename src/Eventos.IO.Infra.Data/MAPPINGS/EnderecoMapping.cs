using Eventos.IO.Domain.Eventos;
using Eventos.IO.Infra.Data.EXTENSIONS;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eventos.IO.Infra.Data.MAPPINGS;

public class EnderecoMapping : EntityTypeConfiguration<Endereco>
{
    public override void Map(EntityTypeBuilder<Endereco> builder)
    {
        builder.Property(e => e.Logradouro)
            .IsRequired()
            .HasMaxLength(20)
            .HasColumnType("varchar(150)");

        builder.Property(e => e.Numero)
            .IsRequired()
            .HasMaxLength(20)
            .HasColumnType("varchar(20)");

        builder.Property(e => e.Bairro)
            .IsRequired()
            .HasMaxLength(50)
            .HasColumnType("varchar(50)");

        builder.Property(e => e.CEP)
            .IsRequired()
            .HasMaxLength(8)
            .HasColumnType("varchar(8)");

        builder.Property(e => e.Complemento)
            .HasMaxLength(100)
            .HasColumnType("varchar(100)");

        builder.Property(e => e.Cidade)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnType("varchar(100)");

        builder.Ignore(e => e.ValidationResult);

        builder.Ignore(e => e.CascadeMode);

        builder.HasOne(c => c.evento)
            .WithOne(c => c.Endereco)
            .HasForeignKey<Endereco>(c => c.EventoId)
            .IsRequired(false);

        builder.ToTable("Enderecos");
    }
}