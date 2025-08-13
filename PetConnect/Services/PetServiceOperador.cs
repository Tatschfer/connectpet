using Microsoft.EntityFrameworkCore;
using PetConnect.Models;
using DbCtx = PetConnect.Data.PetConnect;

namespace PetConnect.Services;

public class OperadorService
{
    private readonly DbCtx _db;
    public OperadorService(DbCtx db) => _db = db;

    public async Task<List<Operador>> GetAllAsync(CancellationToken ct = default)
        => await _db.Operadores.AsNoTracking().OrderBy(t => t.Nome).ToListAsync(ct);

    public async Task<Operador?> GetByIdAsync(int id, CancellationToken ct = default)
        => await _db.Operadores.AsNoTracking().FirstOrDefaultAsync(t => t.IdOperador == id, ct);

    public async Task<Operador> CreateAsync(Operador operador, CancellationToken ct = default)
    {
        _db.Operadores.Add(operador);
        await _db.SaveChangesAsync(ct);
        return operador;
    }

    public async Task<bool> UpdateAsync(int id, Operador operadores, CancellationToken ct = default)
    {
        var exists = await _db.Operadores.AnyAsync(t => t.IdOperador == id, ct);
        if (!exists) return false;

        operadores.IdOperador = id;
        _db.Entry(operadores).State = EntityState.Modified; 
        await _db.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
    {
        var entity = await _db.Operadores.FindAsync([id], ct);
        if (entity is null) return false;

        _db.Operadores.Remove(entity);
        await _db.SaveChangesAsync(ct);
        return true;
    }
}