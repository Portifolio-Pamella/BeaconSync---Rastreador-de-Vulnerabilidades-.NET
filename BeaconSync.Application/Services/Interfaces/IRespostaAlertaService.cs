using BeaconSync.Application.DTOs;

namespace BeaconSync.Application.Services.Interfaces;

public interface IRespostaAlertaService
{
    Task<IEnumerable<RespostaAlertaDto>> GetAllAsync();
    Task<RespostaAlertaDto?> GetByIdAsync(int id);
    Task<RespostaAlertaDto> AddAsync(RespostaAlertaDto dto);
    Task UpdateAsync(int id, RespostaAlertaDto dto);
    Task DeleteAsync(int id);
}