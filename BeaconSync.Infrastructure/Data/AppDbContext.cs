using BeaconSync.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BeaconSync.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<PessoaVulneravel> PessoasVulneraveis { get; set; }
    public DbSet<Responsavel> Responsaveis { get; set; }
    public DbSet<Endereco> Enderecos { get; set; }
    public DbSet<Voluntario> Voluntarios { get; set; }
    public DbSet<RespostaAlerta> RespostasAlertas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Mantém as configurações de assembly caso existam
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        // Mapeamento explícito em MAIÚSCULAS para garantir compatibilidade total com o Oracle
        modelBuilder.Entity<PessoaVulneravel>().ToTable("PESSOAS_VULNERAVEIS");
        modelBuilder.Entity<Responsavel>().ToTable("RESPONSAVEIS");
        modelBuilder.Entity<Endereco>().ToTable("ENDERECOS");
        modelBuilder.Entity<Voluntario>().ToTable("VOLUNTARIOS");
        modelBuilder.Entity<RespostaAlerta>().ToTable("RESPOSTAS_ALERTAS");

        base.OnModelCreating(modelBuilder);
    }
}