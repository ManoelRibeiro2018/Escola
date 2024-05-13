using Escola.Domain.Dtos.InputModels;
using Escola.Domain.Interface.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Escola.API.Controllers
{
    [ApiController]
    [Route("api/aluno")]
    public class AlunoController : ControllerBase
    {
        private readonly IAlunoService _alunoService;


        public AlunoController(IAlunoService alunoService)
        {
            _alunoService = alunoService;
        }

        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> Get(int id)
        {
            var aluno = await _alunoService.Get(id);

            return Ok(aluno);
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> GetAll()
        {
            var alunos = await _alunoService.GetAll();

            return Ok(alunos);
        }

        [Authorize]

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> Post(AlunoInputModel alunoInputModel)
        {
            var result = await _alunoService.Create(alunoInputModel);
            if (result.Success)
                return Ok(result);

            return StatusCode(500, result);
        }

        [Authorize]
        [HttpPut()]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> Update(AlunoInputModel alunoInputModel)
        {
            var result = await _alunoService.Update(alunoInputModel);
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
            var result = await _alunoService.Delete(id);

            if (result.Success)
                return Ok(result);

            return StatusCode(500, result);
        }
    }
}
