using BeaconSync.Domain.Models;

namespace BeaconSync.Infrastructure.Repositories.Interfaces;

public interface IResponsavelRepository
{
    Task<IEnumerable<Responsavel>> GetAllAsync();
    Task<Responsavel?> GetByIdAsync(int id);
    Task<Responsavel> AddAsync(Responsavel responsavel);
    Task UpdateAsync(Responsavel responsavel);
    Task DeleteAsync(int id);
}