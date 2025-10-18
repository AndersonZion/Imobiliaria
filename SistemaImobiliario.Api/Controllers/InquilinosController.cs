using Microsoft.AspNetCore.Mvc;
using SistemaImobiliario.Application.DTOs;
using SistemaImobiliario.Application.Interfaces;

namespace SistemaImobiliario.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InquilinoController : ControllerBase
    {
        private readonly IInquilinoService _service;

        public InquilinoController(IInquilinoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            if (!result.Success) return BadRequest(result.Errors);
            return Ok(result.Data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (!result.Success) return NotFound(result.Errors);
            return Ok(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] InquilineDto dto)
        {
            var result = await _service.CreateAsync(dto);
            if (!result.Success) return BadRequest(result.Errors);
            return CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] InquilineDto dto)
        {
            var result = await _service.UpdateAsync(id, dto);
            if (!result.Success) return BadRequest(result.Errors);
            return Ok(result.Data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            if (!result.Success) return NotFound(result.Errors);
            return NoContent();
        }
    }
}
