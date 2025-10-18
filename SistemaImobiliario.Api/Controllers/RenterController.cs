using Microsoft.AspNetCore.Mvc;
using SistemaImobiliario.Application.DTOs;
using SistemaImobiliario.Application.Interfaces;
using SistemaImobiliario.Application.Services;
using SistemaImobiliario.Shared.Filters;
using System.Text.Json;

namespace SistemaImobiliario.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RenterController : AppControllerBase
    {
        private readonly IRenterService _service;

        public RenterController(IRenterService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return HandleResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            return HandleResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RenterDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return HandleResult(result, 201);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] RenterDto dto)
        {
            var result = await _service.UpdateAsync(id, dto);
            return HandleResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            return HandleResult(result);
        }

        [HttpGet("GetPaged")]
        public async Task<IActionResult> GetPaged([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? search = null)
        {
            var result = await _service.GetPagedAsync(pageNumber, pageSize, search);

            if (!result.Success)
                return HandleResult(result);

            return HandleResult(result);
        }

        [HttpGet("GetPaged2")]
        public async Task<IActionResult> GetPaged([FromQuery] PaginationFilter filter)
        {
            var result = await _service.GetPagedAsync(filter);

            if (!result.Success)
                return HandleResult(result);

            return HandleResult(result);
        }
    }
}
