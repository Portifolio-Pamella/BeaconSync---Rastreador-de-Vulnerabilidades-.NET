using BeaconSync.Application.DTOs;
using BeaconSync.Application.Services.Interfaces;
using BeaconSync.Domain.Models;
using BeaconSync.Infrastructure.Repositories.Interfaces;

namespace BeaconSync.Application.Services;

public class EnderecoService : IEnderecoService
{
    private readonly IEnderecoRepository _repository;

    public EnderecoService(IEnderecoRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<EnderecoDto>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        return entities.Select(e => new EnderecoDto
        {
            Id = e.Id,
            PessoaVulneravelId = e.PessoaVulneravelId,
            Logradouro = e.Logradouro,
            Numero = e.Numero,
            Bairro = e.Bairro,
            Distrito = e.Distrito,
            TipoImovel = e.TipoImovel,
            TemLajeParaHelicoptero = e.TemLajeParaHelicoptero,
            Andar = e.Andar,
            EEnderecoAtual = e.EEnderecoAtual
        });
    }

    public async Task<EnderecoDto?> GetByIdAsync(int id)
    {
        var e = await _repository.GetByIdAsync(id);
        if (e == null) return null;

        return new EnderecoDto
        {
            Id = e.Id,
            PessoaVulneravelId = e.PessoaVulneravelId,
            Logradouro = e.Logradouro,
            Numero = e.Numero,
            Bairro = e.Bairro,
            Distrito = e.Distrito,
            TipoImovel = e.TipoImovel,
            TemLajeParaHelicoptero = e.TemLajeParaHelicoptero,
            Andar = e.Andar,
            EEnderecoAtual = e.EEnderecoAtual
        };
    }

    public async Task<EnderecoDto> AddAsync(EnderecoDto dto)
    {
        var entity = new Endereco
        {
            PessoaVulneravelId = dto.PessoaVulneravelId,
            Logradouro = dto.Logradouro,
            Numero = dto.Numero,
            Bairro = dto.Bairro,
            Distrito = dto.Distrito,
            TipoImovel = dto.TipoImovel,
            TemLajeParaHelicoptero = dto.TemLajeParaHelicoptero,
            Andar = dto.Andar,
            EEnderecoAtual = dto.EEnderecoAtual
        };

        var created = await _repository.AddAsync(entity);
        dto.Id = created.Id;
        return dto;
    }

    public async Task UpdateAsync(int id, EnderecoDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) throw new KeyNotFoundException("Endereço não encontrado.");

        entity.PessoaVulneravelId = dto.PessoaVulneravelId;
        entity.Logradouro = dto.Logradouro;
        entity.Numero = dto.Numero;
        entity.Bairro = dto.Bairro;
        entity.Distrito = dto.Distrito;
        entity.TipoImovel = dto.TipoImovel;
        entity.TemLajeParaHelicoptero = dto.TemLajeParaHelicoptero;
        entity.Andar = dto.Andar;
        entity.EEnderecoAtual = dto.EEnderecoAtual;

        await _repository.UpdateAsync(entity);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
}