using BeaconSync.Domain.Models;
using BeaconSync.Infrastructure.Data;
using BeaconSync.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BeaconSync.Infrastructure.Repositories;

public class EnderecoRepository : IEnderecoRepository
{
    private readonly AppDbContext _context;

    public EnderecoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Endereco>> GetAllAsync()
    {
        return await _context.Enderecos
            .Include(e => e.PessoaVulneravel)
            .ToListAsync();
    }

    public async Task<Endereco?> GetByIdAsync(int id)
    {
        return await _context.Enderecos
            .Include(e => e.PessoaVulneravel)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<Endereco> AddAsync(Endereco endereco)
    {
        _context.Enderecos.Add(endereco);
        await _context.SaveChangesAsync();
        return endereco;
    }

    public async Task UpdateAsync(Endereco endereco)
    {
        _context.Enderecos.Update(endereco);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var endereco = await _context.Enderecos.FindAsync(id);
        if (endereco != null)
        {
            _context.Enderecos.Remove(endereco);
            await _context.SaveChangesAsync();
        }
    }
}