using BeaconSync.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeaconSync.Infrastructure.Data.Mappings;

public class VoluntarioMapping : IEntityTypeConfiguration<Voluntario>
{
    public void Configure(EntityTypeBuilder<Voluntario> builder)
    {
        builder.HasKey(v => v.Id);

        builder.Property(v => v.Nome)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(v => v.Telefone)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(v => v.Email)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(v => v.Especialidade)
            .HasMaxLength(100);

        // Compatibilidade com Oracle para campos booleanos
        builder.Property(v => v.DisponivelParaAjuda)
            .HasColumnType("NUMBER(1)");
    }
}