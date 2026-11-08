using BeaconSync.Domain.Models;

namespace BeaconSync.Infrastructure.Repositories.Interfaces;

public interface IRespostaAlertaRepository
{
    Task<IEnumerable<RespostaAlerta>> GetAllAsync();
    Task<RespostaAlerta?> GetByIdAsync(int id);
    Task<RespostaAlerta> AddAsync(RespostaAlerta respostaAlerta);
    Task UpdateAsync(RespostaAlerta respostaAlerta);
    Task DeleteAsync(int id);
}