using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetConnect.Models;
using DbCtx = PetConnect.Data.PetConnect;

namespace App.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PetsController : ControllerBase
{
    private readonly DbCtx _db;
    public PetsController(DbCtx db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Pet>>> GetAll(CancellationToken ct)
        => Ok(await _db.Operadores.AsNoTracking().ToListAsync(ct));

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Pet>> GetById(int id, CancellationToken ct)
    {
        var pet = await _db.Pets.FindAsync([id], ct);
        return pet is null ? NotFound() : Ok(pet);
    }

    [HttpPost]
    public async Task<ActionResult<Pet>> Create([FromBody] PetInputDto dto, CancellationToken ct)
    {
        var pet = new Pet
        {
            Nome = dto.NomePet,
            Raca = dto.RacaPet,
            DataDeNascimento = dto.DataDeNascimentoPet,
            Cpf = dto.CPFTutor,
            Cor = dto.CorPet,
            Especie = dto.EspeciePet
        };
        _db.Pets.Add(pet);
        await _db.SaveChangesAsync(ct);
        return CreatedAtAction(nameof(GetById), new { id = pet.IdPet }, pet);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] PetUpdateDto dto, CancellationToken ct)
    {
        var pet = await _db.Pets.FindAsync([id], ct);
        if (pet is null) return NotFound();

        if (!string.IsNullOrWhiteSpace(dto.NomePet)) pet.Nome = dto.NomePet;
        if (!string.IsNullOrWhiteSpace(dto.RacaPet)) pet.Raca = dto.RacaPet;
        if (!string.IsNullOrWhiteSpace(dto.CPFTutor)) pet.Cpf = dto.CPFTutor;
        if (!string.IsNullOrWhiteSpace(dto.CorPet)) pet.Cor = dto.CorPet;
        if (!string.IsNullOrWhiteSpace(dto.EspeciePet)) pet.Especie = dto.EspeciePet;

        await _db.SaveChangesAsync(ct);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var pet = await _db.Pets.FindAsync([id], ct);
        if (pet is null) return NotFound();

        _db.Pets.Remove(pet);
        await _db.SaveChangesAsync(ct);
        return NoContent();
    }
}
