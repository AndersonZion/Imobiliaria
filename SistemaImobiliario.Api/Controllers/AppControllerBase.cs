using Microsoft.AspNetCore.Mvc;
using SistemaImobiliario.Application.Common;

namespace SistemaImobiliario.API.Controllers
{
    [ApiController]
    public abstract class AppControllerBase : ControllerBase
    {
        // Método genérico para lidar com ServiceResult<T>
        protected IActionResult HandleResult<T>(ServiceResult<T> result, int successStatusCode = 200)
        {
            if (!result.Success)
                return BadRequest(new { errors = result.Errors });

            // Se for bool e sucesso, retorna NoContent (204)
            if (typeof(T) == typeof(bool) && successStatusCode == 200)
                return StatusCode(204);

            return StatusCode(successStatusCode, result.Data);
        }
    }
}
