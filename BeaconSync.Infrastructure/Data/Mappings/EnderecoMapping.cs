using BeaconSync.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeaconSync.Infrastructure.Data.Mappings;

public class EnderecoMapping : IEntityTypeConfiguration<Endereco>
{
    public void Configure(EntityTypeBuilder<Endereco> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Logradouro)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(e => e.Numero)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(e => e.Bairro)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.Distrito)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.TipoImovel)
            .HasMaxLength(50);

        // Força booleano a ser tratado de forma compatível no Oracle
        builder.Property(e => e.TemLajeParaHelicoptero)
            .HasColumnType("NUMBER(1)");

        builder.Property(e => e.EEnderecoAtual)
            .HasColumnType("NUMBER(1)");
    }
}