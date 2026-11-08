using BeaconSync.Application.DTOs;

namespace BeaconSync.Application.Services.Interfaces;

public interface IResponsavelService
{
    Task<IEnumerable<ResponsavelDto>> GetAllAsync();
    Task<ResponsavelDto?> GetByIdAsync(int id);
    Task<ResponsavelDto> AddAsync(ResponsavelDto dto);
    Task UpdateAsync(int id, ResponsavelDto dto);
    Task DeleteAsync(int id);
}