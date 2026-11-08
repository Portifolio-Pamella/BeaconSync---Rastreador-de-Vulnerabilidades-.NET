using BeaconSync.Application.DTOs;
using BeaconSync.Application.Services.Interfaces;
using BeaconSync.Domain.Models;
using BeaconSync.Infrastructure.Repositories.Interfaces;

namespace BeaconSync.Application.Services;

public class VoluntarioService : IVoluntarioService
{
    private readonly IVoluntarioRepository _repository;

    public VoluntarioService(IVoluntarioRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<VoluntarioDto>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        return entities.Select(e => new VoluntarioDto
        {
            Id = e.Id,
            Nome = e.Nome,
            Telefone = e.Telefone,
            Email = e.Email,
            Especialidade = e.Especialidade,
            DisponivelParaAjuda = e.DisponivelParaAjuda
        });
    }

    public async Task<VoluntarioDto?> GetByIdAsync(int id)
    {
        var e = await _repository.GetByIdAsync(id);
        if (e == null) return null;

        return new VoluntarioDto
        {
            Id = e.Id,
            Nome = e.Nome,
            Telefone = e.Telefone,
            Email = e.Email,
            Especialidade = e.Especialidade,
            DisponivelParaAjuda = e.DisponivelParaAjuda
        };
    }

    public async Task<VoluntarioDto> AddAsync(VoluntarioDto dto)
    {
        var entity = new Voluntario
        {
            Nome = dto.Nome,
            Telefone = dto.Telefone,
            Email = dto.Email,
            Especialidade = dto.Especialidade,
            DisponivelParaAjuda = dto.DisponivelParaAjuda
        };

        var created = await _repository.AddAsync(entity);
        dto.Id = created.Id;
        return dto;
    }

    public async Task UpdateAsync(int id, VoluntarioDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) throw new KeyNotFoundException("Voluntário não encontrado.");

        entity.Nome = dto.Nome;
        entity.Telefone = dto.Telefone;
        entity.Email = dto.Email;
        entity.Especialidade = dto.Especialidade;
        entity.DisponivelParaAjuda = dto.DisponivelParaAjuda;

        await _repository.UpdateAsync(entity);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
}