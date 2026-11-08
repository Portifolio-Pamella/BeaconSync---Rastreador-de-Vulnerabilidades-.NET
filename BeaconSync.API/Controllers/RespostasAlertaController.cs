using BeaconSync.Application.DTOs;
using BeaconSync.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeaconSync.API.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/respostas-alerta")]
public class RespostasAlertaController : ControllerBase
{
    private readonly IRespostaAlertaService _service;

    public RespostasAlertaController(IRespostaAlertaService service) => _service = service;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RespostaAlertaDto>>> GetAll() => Ok(await _service.GetAllAsync());

    [HttpGet("{id:int}")]
    public async Task<ActionResult<RespostaAlertaDto>> GetById(int id)
    {
        var res = await _service.GetByIdAsync(id);
        return res == null ? NotFound() : Ok(res);
    }

    [HttpPost]
    public async Task<ActionResult<RespostaAlertaDto>> Create([FromBody] RespostaAlertaDto dto)
    {
        var created = await _service.AddAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] RespostaAlertaDto dto)
    {
        try { await _service.UpdateAsync(id, dto); return NoContent(); }
        catch { return NotFound(); }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id) { await _service.DeleteAsync(id); return NoContent(); }
}