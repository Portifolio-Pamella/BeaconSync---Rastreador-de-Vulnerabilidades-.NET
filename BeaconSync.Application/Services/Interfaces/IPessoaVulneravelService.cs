using BeaconSync.Application.DTOs;

namespace BeaconSync.Application.Services.Interfaces;

public interface IPessoaVulneravelService
{
    Task<IEnumerable<PessoaVulneravelDto>> GetAllAsync();
    Task<PessoaVulneravelDto?> GetByIdAsync(int id);
    Task<PessoaVulneravelDto> AddAsync(PessoaVulneravelDto dto);
    Task UpdateAsync(int id, PessoaVulneravelDto dto);
    Task DeleteAsync(int id);
}