using BeaconSync.Application.DTOs;

namespace BeaconSync.Application.Services.Interfaces;

public interface IEnderecoService
{
    Task<IEnumerable<EnderecoDto>> GetAllAsync();
    Task<EnderecoDto?> GetByIdAsync(int id);
    Task<EnderecoDto> AddAsync(EnderecoDto dto);
    Task UpdateAsync(int id, EnderecoDto dto);
    Task DeleteAsync(int id);
}