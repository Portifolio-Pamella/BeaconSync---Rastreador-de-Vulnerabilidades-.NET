using BeaconSync.Application.DTOs;
using BeaconSync.Application.Services.Interfaces;
using BeaconSync.Domain.Models;
using BeaconSync.Infrastructure.Repositories.Interfaces;

namespace BeaconSync.Application.Services;

public class PessoaVulneravelService : IPessoaVulneravelService
{
    private readonly IPessoaVulneravelRepository _repository;

    public PessoaVulneravelService(IPessoaVulneravelRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<PessoaVulneravelDto>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        return entities.Select(e => new PessoaVulneravelDto
        {
            Id = e.Id,
            Nome = e.Nome,
            DataNascimento = e.DataNascimento,
            TipoVulnerabilidade = e.TipoVulnerabilidade,
            NecessidadeEspecial = e.NecessidadeEspecial
        });
    }

    public async Task<PessoaVulneravelDto?> GetByIdAsync(int id)
    {
        var e = await _repository.GetByIdAsync(id);
        if (e == null) return null;

        return new PessoaVulneravelDto
        {
            Id = e.Id,
            Nome = e.Nome,
            DataNascimento = e.DataNascimento,
            TipoVulnerabilidade = e.TipoVulnerabilidade,
            NecessidadeEspecial = e.NecessidadeEspecial
        };
    }

    public async Task<PessoaVulneravelDto> AddAsync(PessoaVulneravelDto dto)
    {
        var entity = new PessoaVulneravel
        {
            Nome = dto.Nome,
            DataNascimento = dto.DataNascimento,
            TipoVulnerabilidade = dto.TipoVulnerabilidade,
            NecessidadeEspecial = dto.NecessidadeEspecial
        };

        await _repository.AddAsync(entity);
        dto.Id = entity.Id;
        return dto;
    }

    public async Task UpdateAsync(int id, PessoaVulneravelDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) throw new KeyNotFoundException("Pessoa vulnerável não encontrada.");

        entity.Nome = dto.Nome;
        entity.DataNascimento = dto.DataNascimento;
        entity.TipoVulnerabilidade = dto.TipoVulnerabilidade;
        entity.NecessidadeEspecial = dto.NecessidadeEspecial;

        await _repository.UpdateAsync(entity);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
}