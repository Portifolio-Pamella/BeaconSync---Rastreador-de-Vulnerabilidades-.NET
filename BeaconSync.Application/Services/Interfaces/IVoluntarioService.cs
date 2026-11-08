using BeaconSync.Application.DTOs;

namespace BeaconSync.Application.Services.Interfaces;

public interface IVoluntarioService
{
    Task<IEnumerable<VoluntarioDto>> GetAllAsync();
    Task<VoluntarioDto?> GetByIdAsync(int id);
    Task<VoluntarioDto> AddAsync(VoluntarioDto dto);
    Task UpdateAsync(int id, VoluntarioDto dto);
    Task DeleteAsync(int id);
}