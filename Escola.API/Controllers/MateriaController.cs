using Escola.Domain.Dtos.InputModels;
using Escola.Domain.Interface.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Escola.API.Controllers
{
    [ApiController]
    [Route("api/materia")]
    public class MateriaController : ControllerBase
    {
        private readonly IMateriaService _materiaService;

        public MateriaController(IMateriaService materiaService)
        {
            _materiaService = materiaService;
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> GetAll()
        {
            var materias = await _materiaService.GetAll();
            return Ok(materias);
        }

        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> Get(int id)
        {
            var materia = await _materiaService.Get(id);
            return Ok(materia);
        }

        [Authorize]
        [HttpPost()]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> Post(MateriaInputModel materiaInputModel)
        {
            var result = await _materiaService.Create(materiaInputModel);
            if (result.Success)
                return Ok(result);

            return StatusCode(500, result);
        }

        [Authorize]
        [HttpPut()]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> Update(MateriaInputModel materiaInputModel)
        {
            var result = await _materiaService.Update(materiaInputModel);

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
            var result = await _materiaService.Delete(id);

            if (result.Success)
                return Ok(result);

            return StatusCode(500, result);
        }
    }
}
