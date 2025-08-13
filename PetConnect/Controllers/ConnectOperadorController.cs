using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetConnect.Models; 
using DbCtx = PetConnect.Data.PetConnect;

namespace App.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OperadoresController : ControllerBase
{
    private readonly DbCtx _db;
    public OperadoresController(DbCtx db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Operador>>> GetAll(CancellationToken ct)
        => Ok(await _db.Operadores.AsNoTracking().ToListAsync(ct));

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Operador>> GetById(int id, CancellationToken ct)
    {
        var op = await _db.Operadores
                          .AsNoTracking()
                          .FirstOrDefaultAsync(o => o.IdOperador == id, ct);
        return op is null ? NotFound() : Ok(op);
    }

    [HttpPost]
        public async Task<ActionResult<Operador>> Create([FromBody] OperadorCreateDto dto, CancellationToken ct)
        {
            var op = new Operador
            {
                Nome = dto.NomeOperador,      
                CpfOperador = dto.CPFOperador,       
                CnpjOperador = dto.CNPJOperador,      
                TelefoneOperador = dto.TelefoneOperador
            };

            _db.Operadores.Add(op);
            await _db.SaveChangesAsync(ct);

        return CreatedAtAction(nameof(GetById), new { id = op.IdOperador }, op);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] OperadorUpdateDto dto, CancellationToken ct)
    {
        var op = await _db.Operadores.FirstOrDefaultAsync(o => o.IdOperador == id, ct);
        if (op is null) return NotFound();

        if (!string.IsNullOrWhiteSpace(dto.NomeOperador)) op.Nome = dto.NomeOperador;            
        if (!string.IsNullOrWhiteSpace(dto.CPFOperador)) op.CpfOperador = dto.CPFOperador;      
        if (!string.IsNullOrWhiteSpace(dto.CNPJOperador)) op.CnpjOperador = dto.CNPJOperador;    
        if (!string.IsNullOrWhiteSpace(dto.EmailOperador)) op.EmailOperador = dto.EmailOperador;  
        if (!string.IsNullOrWhiteSpace(dto.TelefoneOperador)) op.TelefoneOperador = dto.TelefoneOperador;

        await _db.SaveChangesAsync(ct);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var op = await _db.Operadores.FirstOrDefaultAsync(o => o.IdOperador == id, ct);
        if (op is null) return NotFound();

        _db.Operadores.Remove(op);
        await _db.SaveChangesAsync(ct);
        return NoContent();
    }
}
