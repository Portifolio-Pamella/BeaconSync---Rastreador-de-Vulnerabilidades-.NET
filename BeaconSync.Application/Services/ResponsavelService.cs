using BeaconSync.Application.DTOs;
using BeaconSync.Application.Services.Interfaces;
using BeaconSync.Domain.Models;
using BeaconSync.Infrastructure.Repositories.Interfaces;

namespace BeaconSync.Application.Services;

public class ResponsavelService : IResponsavelService
{
    private readonly IResponsavelRepository _repository;

    public ResponsavelService(IResponsavelRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ResponsavelDto>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        return entities.Select(e => new ResponsavelDto
        {
            Id = e.Id,
            Nome = e.Nome,
            Telefone = e.Telefone,
            Email = e.Email,
            MoraComVulneravel = e.MoraComVulneravel,
            PessoaVulneravelId = e.PessoaVulneravelId
        });
    }

    public async Task<ResponsavelDto?> GetByIdAsync(int id)
    {
        var e = await _repository.GetByIdAsync(id);
        if (e == null) return null;

        return new ResponsavelDto
        {
            Id = e.Id,
            Nome = e.Nome,
            Telefone = e.Telefone,
            Email = e.Email,
            MoraComVulneravel = e.MoraComVulneravel,
            PessoaVulneravelId = e.PessoaVulneravelId
        };
    }

    public async Task<ResponsavelDto> AddAsync(ResponsavelDto dto)
    {
        var entity = new Responsavel
        {
            Nome = dto.Nome,
            Telefone = dto.Telefone,
            Email = dto.Email,
            MoraComVulneravel = dto.MoraComVulneravel,
            PessoaVulneravelId = dto.PessoaVulneravelId
        };

        var created = await _repository.AddAsync(entity);
        dto.Id = created.Id;
        return dto;
    }

    public async Task UpdateAsync(int id, ResponsavelDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) throw new KeyNotFoundException("Responsável não encontrado.");

        entity.Nome = dto.Nome;
        entity.Telefone = dto.Telefone;
        entity.Email = dto.Email;
        entity.MoraComVulneravel = dto.MoraComVulneravel;
        entity.PessoaVulneravelId = dto.PessoaVulneravelId;

        await _repository.UpdateAsync(entity);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
}