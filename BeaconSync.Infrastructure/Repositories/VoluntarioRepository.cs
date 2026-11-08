using BeaconSync.Domain.Models;
using BeaconSync.Infrastructure.Data;
using BeaconSync.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BeaconSync.Infrastructure.Repositories;

public class VoluntarioRepository : IVoluntarioRepository
{
    private readonly AppDbContext _context;

    public VoluntarioRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Voluntario>> GetAllAsync()
    {
        return await _context.Voluntarios.ToListAsync();
    }

    public async Task<Voluntario?> GetByIdAsync(int id)
    {
        return await _context.Voluntarios.FindAsync(id);
    }

    public async Task<Voluntario> AddAsync(Voluntario voluntario)
    {
        _context.Voluntarios.Add(voluntario);
        await _context.SaveChangesAsync();
        return voluntario;
    }

    public async Task UpdateAsync(Voluntario voluntario)
    {
        _context.Voluntarios.Update(voluntario);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var voluntario = await _context.Voluntarios.FindAsync(id);
        if (voluntario != null)
        {
            _context.Voluntarios.Remove(voluntario);
            await _context.SaveChangesAsync();
        }
    }
}