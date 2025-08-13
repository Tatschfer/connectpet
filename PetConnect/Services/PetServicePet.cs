using Microsoft.EntityFrameworkCore;
using PetConnect.Models;
using DbCtx = PetConnect.Data.PetConnect;

namespace PetConnect.Services;

public class PetService
{
    private readonly DbCtx _db;
    public PetService(DbCtx db) => _db = db;

    public async Task<List<Pet>> GetAllAsync(CancellationToken ct = default)
        => await _db.Pets.AsNoTracking().OrderBy(t => t.Nome).ToListAsync(ct);

    public async Task<Pet?> GetByIdAsync(int id, CancellationToken ct = default)
        => await _db.Pets.AsNoTracking().FirstOrDefaultAsync(t => t.IdPet == id, ct);

    public async Task<Pet> CreateAsync(Pet pet, CancellationToken ct = default)
    {
        _db.Pets.Add(pet);
        await _db.SaveChangesAsync(ct);
        return pet;
    }

    public async Task<bool> UpdateAsync(int id, Pet pet, CancellationToken ct = default)
    {
        var exists = await _db.Pets.AnyAsync(t => t.IdPet == id, ct);
        if (!exists) return false;

        pet.IdPet = id; 
        _db.Entry(pet).State = EntityState.Modified;
        await _db.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
    {
        var entity = await _db.Pets.FindAsync([id], ct);
        if (entity is null) return false;

        _db.Pets.Remove(entity);
        await _db.SaveChangesAsync(ct);
        return true;
    }
}