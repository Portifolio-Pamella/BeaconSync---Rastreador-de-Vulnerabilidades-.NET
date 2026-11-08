using BeaconSync.Application.DTOs;
using BeaconSync.Application.Services.Interfaces;
using BeaconSync.Domain.Models;
using BeaconSync.Infrastructure.Repositories.Interfaces;

namespace BeaconSync.Application.Services;

public class RespostaAlertaService : IRespostaAlertaService
{
    private readonly IRespostaAlertaRepository _repository;

    public RespostaAlertaService(IRespostaAlertaRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<RespostaAlertaDto>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        return entities.Select(e => new RespostaAlertaDto
        {
            Id = e.Id,
            PessoaVulneravelId = e.PessoaVulneravelId,
            EnderecoOndeEstaId = e.EnderecoOndeEstaId,
            StatusSeguranca = e.StatusSeguranca,
            Observacao = e.Observacao,
            DataResposta = e.DataResposta
        });
    }

    public async Task<RespostaAlertaDto?> GetByIdAsync(int id)
    {
        var e = await _repository.GetByIdAsync(id);
        if (e == null) return null;

        return new RespostaAlertaDto
        {
            Id = e.Id,
            PessoaVulneravelId = e.PessoaVulneravelId,
            EnderecoOndeEstaId = e.EnderecoOndeEstaId,
            StatusSeguranca = e.StatusSeguranca,
            Observacao = e.Observacao,
            DataResposta = e.DataResposta
        };
    }

    public async Task<RespostaAlertaDto> AddAsync(RespostaAlertaDto dto)
    {
        var entity = new RespostaAlerta
        {
            PessoaVulneravelId = dto.PessoaVulneravelId,
            EnderecoOndeEstaId = dto.EnderecoOndeEstaId,
            StatusSeguranca = dto.StatusSeguranca,
            Observacao = dto.Observacao,
            DataResposta = dto.DataResposta == default ? DateTime.UtcNow : dto.DataResposta
        };

        var created = await _repository.AddAsync(entity);
        dto.Id = created.Id;
        return dto;
    }

    public async Task UpdateAsync(int id, RespostaAlertaDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) throw new KeyNotFoundException("Resposta de alerta não encontrada.");

        entity.PessoaVulneravelId = dto.PessoaVulneravelId;
        entity.EnderecoOndeEstaId = dto.EnderecoOndeEstaId;
        entity.StatusSeguranca = dto.StatusSeguranca;
        entity.Observacao = dto.Observacao;
        entity.DataResposta = dto.DataResposta;

        await _repository.UpdateAsync(entity);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
}