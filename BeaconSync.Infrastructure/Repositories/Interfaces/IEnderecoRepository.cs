using BeaconSync.Domain.Models;

namespace BeaconSync.Infrastructure.Repositories.Interfaces;

public interface IEnderecoRepository
{
    Task<IEnumerable<Endereco>> GetAllAsync();
    Task<Endereco?> GetByIdAsync(int id);
    Task<Endereco> AddAsync(Endereco endereco);
    Task UpdateAsync(Endereco endereco);
    Task DeleteAsync(int id);
}