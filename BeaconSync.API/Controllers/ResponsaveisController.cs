using BeaconSync.Application.DTOs;
using BeaconSync.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeaconSync.API.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/responsaveis")]
public class ResponsaveisController : ControllerBase
{
    private readonly IResponsavelService _service;

    public ResponsaveisController(IResponsavelService service) => _service = service;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ResponsavelDto>>> GetAll() => Ok(await _service.GetAllAsync());

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ResponsavelDto>> GetById(int id)
    {
        var res = await _service.GetByIdAsync(id);
        return res == null ? NotFound() : Ok(res);
    }

    [HttpPost]
    public async Task<ActionResult<ResponsavelDto>> Create([FromBody] ResponsavelDto dto)
    {
        var created = await _service.AddAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] ResponsavelDto dto)
    {
        try { await _service.UpdateAsync(id, dto); return NoContent(); }
        catch { return NotFound(); }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id) { await _service.DeleteAsync(id); return NoContent(); }
}