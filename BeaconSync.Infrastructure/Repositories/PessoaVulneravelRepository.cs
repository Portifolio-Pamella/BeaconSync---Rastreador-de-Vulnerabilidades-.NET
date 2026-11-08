using BeaconSync.Domain.Models;
using BeaconSync.Infrastructure.Data;
using BeaconSync.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BeaconSync.Infrastructure.Repositories;

public class PessoaVulneravelRepository : IPessoaVulneravelRepository
{
    private readonly AppDbContext _context;

    public PessoaVulneravelRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<PessoaVulneravel>> GetAllAsync()
    {
        return await _context.PessoasVulneraveis
            .Include(p => p.Enderecos)
            .Include(p => p.Responsaveis)
            .ToListAsync();
    }

    public async Task<PessoaVulneravel?> GetByIdAsync(int id)
    {
        return await _context.PessoasVulneraveis
            .Include(p => p.Enderecos)
            .Include(p => p.Responsaveis)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task AddAsync(PessoaVulneravel pessoa)
    {
        await _context.PessoasVulneraveis.AddAsync(pessoa);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(PessoaVulneravel pessoa)
    {
        _context.PessoasVulneraveis.Update(pessoa);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var pessoa = await GetByIdAsync(id);
        if (pessoa != null)
        {
            _context.PessoasVulneraveis.Remove(pessoa);
            await _context.SaveChangesAsync();
        }
    }
}