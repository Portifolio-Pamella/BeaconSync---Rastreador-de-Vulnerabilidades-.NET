using BeaconSync.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeaconSync.Infrastructure.Data.Mappings;

public class ResponsavelMapping : IEntityTypeConfiguration<Responsavel>
{
    public void Configure(EntityTypeBuilder<Responsavel> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Nome)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(r => r.Telefone)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(r => r.Email)
            .HasMaxLength(150);

        builder.Property(r => r.LogradouroRes)
            .HasMaxLength(200);

        builder.Property(r => r.NumeroRes)
            .HasMaxLength(20);

        builder.Property(r => r.BairroRes)
            .HasMaxLength(100);

        builder.Property(r => r.CidadeRes)
            .HasMaxLength(100);

        // Compatibilidade com Oracle para campos booleanos
        builder.Property(r => r.MoraComVulneravel)
            .HasColumnType("NUMBER(1)");
    }
}