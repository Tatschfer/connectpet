using Microsoft.EntityFrameworkCore;
using PetConnect.Models;
using DbCtx = PetConnect.Data.PetConnect;


namespace PetConnect.Services;

public class TutorService
{
    private readonly DbCtx _db;
    public TutorService(DbCtx db) => _db = db;

    public async Task<List<Tutor>> GetAllAsync(CancellationToken ct = default)
        => await _db.Tutores.AsNoTracking().OrderBy(t => t.NomeTutor).ToListAsync(ct);

    public async Task<Tutor?> GetByIdAsync(int id, CancellationToken ct = default)
        => await _db.Tutores.AsNoTracking().FirstOrDefaultAsync(t => t.IdTutor == id, ct);

    public async Task<Tutor> CreateAsync(Tutor tutor, CancellationToken ct = default)
    {
        _db.Tutores.Add(tutor);
        await _db.SaveChangesAsync(ct);
        return tutor;
    }

    public async Task<bool> UpdateAsync(int id, Tutor tutor, CancellationToken ct = default)
    {
        var exists = await _db.Tutores.AnyAsync(t => t.IdTutor == id, ct);
        if (!exists) return false;

        tutor.IdTutor = id; 
        _db.Entry(tutor).State = EntityState.Modified; 
        await _db.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
    {
        var entity = await _db.Tutores.FindAsync([id], ct);
        if (entity is null) return false;

        _db.Tutores.Remove(entity);
        await _db.SaveChangesAsync(ct);
        return true;
    }
}