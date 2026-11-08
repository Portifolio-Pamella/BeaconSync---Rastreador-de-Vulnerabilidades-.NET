using BeaconSync.Application.DTOs;
using BeaconSync.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeaconSync.API.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/enderecos")]
public class EnderecosController : ControllerBase
{
    private readonly IEnderecoService _service;

    public EnderecosController(IEnderecoService service) => _service = service;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EnderecoDto>>> GetAll() => Ok(await _service.GetAllAsync());

    [HttpGet("{id:int}")]
    public async Task<ActionResult<EnderecoDto>> GetById(int id)
    {
        var res = await _service.GetByIdAsync(id);
        return res == null ? NotFound() : Ok(res);
    }

    [HttpPost]
    public async Task<ActionResult<EnderecoDto>> Create([FromBody] EnderecoDto dto)
    {
        var created = await _service.AddAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] EnderecoDto dto)
    {
        try { await _service.UpdateAsync(id, dto); return NoContent(); }
        catch { return NotFound(); }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id) { await _service.DeleteAsync(id); return NoContent(); }
}