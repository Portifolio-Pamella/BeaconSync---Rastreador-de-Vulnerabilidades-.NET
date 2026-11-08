using BeaconSync.Domain.Models;

namespace BeaconSync.Infrastructure.Repositories.Interfaces;

public interface IPessoaVulneravelRepository
{
    Task<IEnumerable<PessoaVulneravel>> GetAllAsync();
    Task<PessoaVulneravel?> GetByIdAsync(int id);
    Task AddAsync(PessoaVulneravel pessoa);
    Task UpdateAsync(PessoaVulneravel pessoa);
    Task DeleteAsync(int id);
}