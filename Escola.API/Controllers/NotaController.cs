using Escola.Domain.Dtos.InputModels;
using Escola.Domain.Interface.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Escola.API.Controllers
{
    [ApiController]
    [Route("api/nota")]
    public class NotaController : ControllerBase
    {
        private readonly INotaService _notaService;

        public NotaController(INotaService notaService)
        {
            _notaService = notaService;
        }

        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<ActionResult> Get(int id)
        {
            var nota = await _notaService.Get(id);
            return Ok(nota);
        }

        [Authorize]
        [HttpGet()]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<ActionResult> GetAll()
        {
            var notas = await _notaService.GetAll();
            return Ok(notas);
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> Post(NotaInputModel notaInputModel)
        {
            var result = await _notaService.Create(notaInputModel);
            if (result.Success)
                return Ok(result);

            return StatusCode(500, result);
        }

        [Authorize]
        [HttpPut()]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> Put(NotaInputModel notaInputModel)
        {
            var result = await _notaService.Update(notaInputModel);
            if (result.Success)
                return Ok(result);

            return StatusCode(500, result);
        }

        [Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _notaService.Delete(id);
            if (result.Success)
                return Ok(result);

            return StatusCode(500, result);
        }

        [Authorize] // Requer autenticação JWT
        [HttpGet("consultaNota/{codAluno}")]
        [ProducesResponseType(401)]

        public async Task<IActionResult> ConsultarNotas(string codAluno)
        {
            var tokenClaims = HttpContext.User.Identity as ClaimsIdentity;
            var tokenICodAluno = tokenClaims.FindFirst(ClaimTypes.Name)?.Value;

            if (tokenICodAluno != codAluno)
            {
                return Unauthorized("Unauthorized access");
            }

            var notas = await _notaService.GetAllWithMateria(int.Parse(codAluno));
            return Ok(notas);
        }
    }
}
