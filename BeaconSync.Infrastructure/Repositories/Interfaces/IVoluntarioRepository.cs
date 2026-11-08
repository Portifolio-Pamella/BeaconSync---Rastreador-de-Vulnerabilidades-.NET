using BeaconSync.Domain.Models;

namespace BeaconSync.Infrastructure.Repositories.Interfaces;

public interface IVoluntarioRepository
{
    Task<IEnumerable<Voluntario>> GetAllAsync();
    Task<Voluntario?> GetByIdAsync(int id);
    Task<Voluntario> AddAsync(Voluntario voluntario);
    Task UpdateAsync(Voluntario voluntario);
    Task DeleteAsync(int id);
}