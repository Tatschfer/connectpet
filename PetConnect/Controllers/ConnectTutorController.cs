using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetConnect.Models;
using DbCtx = PetConnect.Data.PetConnect;

namespace App.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TutorController : ControllerBase
{
    private readonly DbCtx _db;
    public TutorController(DbCtx db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Tutor>>> GetAll(CancellationToken ct)
        => Ok(await _db.Operadores.AsNoTracking().ToListAsync(ct));

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Tutor>> GetById(int id, CancellationToken ct)
    {
        var t = await _db.Tutores.FindAsync([id], ct);
        return t is null ? NotFound() : Ok(t);
    }

    [HttpPost]
    public async Task<ActionResult<Tutor>> Create([FromBody] TutorInputDto dto, CancellationToken ct)
    {
        var t = new Tutor
        {
            NomeTutor = dto.NomeTutor,
            CpfTutor = dto.CPFTutor,
            DataDeNascimentoTutor = dto.DataDeNascimentoTutor,
            EnderecoTutor = dto.EnderecoTutor,
            TelefoneTutor = dto.TelefoneTutor,
            EmailTutor = dto.EmailTutor
        };
        _db.Tutores.Add(t);
        await _db.SaveChangesAsync(ct);
        return CreatedAtAction(nameof(GetById), new { id = t.IdTutor }, t);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] TutorUpdateDto dto, CancellationToken ct)
    {
        var t = await _db.Tutores.FindAsync([id], ct);
        if (t is null) return NotFound();

        if (!string.IsNullOrWhiteSpace(dto.NomeTutor)) t.NomeTutor = dto.NomeTutor;
        if (!string.IsNullOrWhiteSpace(dto.CPFTutor)) t.CpfTutor = dto.CPFTutor;
        if (!string.IsNullOrWhiteSpace(dto.EnderecoTutor)) t.EnderecoTutor = dto.EnderecoTutor;
        if (!string.IsNullOrWhiteSpace(dto.TelefoneTutor)) t.TelefoneTutor = dto.TelefoneTutor;
        if (!string.IsNullOrWhiteSpace(dto.EmailTutor)) t.EmailTutor = dto.EmailTutor;

        await _db.SaveChangesAsync(ct);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var t = await _db.Tutores.FindAsync([id], ct);
        if (t is null) return NotFound();
        _db.Tutores.Remove(t);
        await _db.SaveChangesAsync(ct);
        return NoContent();
    }
}
