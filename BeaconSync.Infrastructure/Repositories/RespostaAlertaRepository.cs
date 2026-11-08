using BeaconSync.Domain.Models;
using BeaconSync.Infrastructure.Data;
using BeaconSync.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BeaconSync.Infrastructure.Repositories;

public class RespostaAlertaRepository : IRespostaAlertaRepository
{
	private readonly AppDbContext _context;

	public RespostaAlertaRepository(AppDbContext context)
	{
		_context = context;
	}

	public async Task<IEnumerable<RespostaAlerta>> GetAllAsync()
	{
		return await _context.RespostasAlertas
			.Include(r => r.PessoaVulneravel)
			.ToListAsync();
	}

	public async Task<RespostaAlerta?> GetByIdAsync(int id)
	{
		return await _context.RespostasAlertas
			.Include(r => r.PessoaVulneravel)
			.FirstOrDefaultAsync(r => r.Id == id);
	}

	public async Task<RespostaAlerta> AddAsync(RespostaAlerta respostaAlerta)
	{
		_context.RespostasAlertas.Add(respostaAlerta);
		await _context.SaveChangesAsync();
		return respostaAlerta;
	}

	public async Task UpdateAsync(RespostaAlerta respostaAlerta)
	{
		_context.RespostasAlertas.Update(respostaAlerta);
		await _context.SaveChangesAsync();
	}

	public async Task DeleteAsync(int id)
	{
		var resposta = await _context.RespostasAlertas.FindAsync(id);
		if (resposta != null)
		{
			_context.RespostasAlertas.Remove(resposta);
			await _context.SaveChangesAsync();
		}
	}
}