using BeaconSync.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeaconSync.Infrastructure.Data.Mappings;

public class PessoaVulneravelMapping : IEntityTypeConfiguration<PessoaVulneravel>
{
    public void Configure(EntityTypeBuilder<PessoaVulneravel> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Nome)
            .IsRequired()
            .HasMaxLength(150);

        // Relacionamento 1:N com Responsáveis
        builder.HasMany(p => p.Responsaveis)
            .WithOne(r => r.PessoaVulneravel)
            .HasForeignKey(r => r.PessoaVulneravelId)
            .OnDelete(DeleteBehavior.Cascade);

        // Relacionamento 1:N com Endereços
        builder.HasMany(p => p.Enderecos)
            .WithOne(e => e.PessoaVulneravel)
            .HasForeignKey(e => e.PessoaVulneravelId)
            .OnDelete(DeleteBehavior.Cascade);

        // Relacionamento 1:N com Respostas de Alertas
        builder.HasMany(p => p.RespostasAlertas)
            .WithOne(r => r.PessoaVulneravel)
            .HasForeignKey(r => r.PessoaVulneravelId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}