using Escola.Domain.Interface.Service;
using Microsoft.AspNetCore.Mvc;

namespace Escola.API.Controllers
{
    [ApiController]
    [Route("api/token")]
    public class TokenController : ControllerBase
    {
        private readonly ITokenService _jwtService;

        public TokenController(ITokenService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpGet("sessionToken/{codAluno}")]
        public IActionResult GenerateSessionToken(int codAluno)
        {
            var token = _jwtService.GenereteToken(codAluno);
            return Ok(new { token });
        }

        [HttpGet("defaultToken")]
        public IActionResult GenerateDefaultToken()
        {
            var defaultToken = _jwtService.GenereteTokenDefault();
            return Ok(new { defaultToken });
        }
    }
}
