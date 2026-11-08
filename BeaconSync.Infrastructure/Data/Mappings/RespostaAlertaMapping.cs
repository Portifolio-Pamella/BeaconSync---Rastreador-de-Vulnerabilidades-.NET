using BeaconSync.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeaconSync.Infrastructure.Data.Mappings;

public class RespostaAlertaMapping : IEntityTypeConfiguration<RespostaAlerta>
{
    public void Configure(EntityTypeBuilder<RespostaAlerta> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.StatusSeguranca)
            .IsRequired()
            .HasMaxLength(50); // Ex: "Seguro", "Precisa de Ajuda"

        builder.Property(r => r.Observacao)
            .HasMaxLength(500); // Espaço maior para detalhes da emergência

        builder.Property(r => r.DataResposta)
            .IsRequired();
    }
}