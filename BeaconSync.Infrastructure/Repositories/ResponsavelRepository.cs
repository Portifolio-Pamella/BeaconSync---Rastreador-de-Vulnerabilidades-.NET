using BeaconSync.Domain.Models;
using BeaconSync.Infrastructure.Data;
using BeaconSync.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BeaconSync.Infrastructure.Repositories;

public class ResponsavelRepository : IResponsavelRepository
{
    private readonly AppDbContext _context;

    public ResponsavelRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Responsavel>> GetAllAsync()
    {
        return await _context.Responsaveis
            .Include(r => r.PessoaVulneravel)
            .ToListAsync();
    }

    public async Task<Responsavel?> GetByIdAsync(int id)
    {
        return await _context.Responsaveis
            .Include(r => r.PessoaVulneravel)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<Responsavel> AddAsync(Responsavel responsavel)
    {
        _context.Responsaveis.Add(responsavel);
        await _context.SaveChangesAsync();
        return responsavel;
    }

    public async Task UpdateAsync(Responsavel responsavel)
    {
        _context.Responsaveis.Update(responsavel);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var responsavel = await _context.Responsaveis.FindAsync(id);
        if (responsavel != null)
        {
            _context.Responsaveis.Remove(responsavel);
            await _context.SaveChangesAsync();
        }
    }
}