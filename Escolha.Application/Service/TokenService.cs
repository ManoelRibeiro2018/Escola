using Escola.Domain.Interface.Repository;
using Escola.Domain.Interface.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Escola.Application.Service
{
    [ExcludeFromCodeCoverage]
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly IAlunoRepository _alunoRepository;

        public TokenService(IConfiguration configuration, IAlunoRepository alunoRepository)
        {
            _configuration = configuration;
            _alunoRepository = alunoRepository;
        }

        public async Task<string> GenereteToken(int codAluno)
        {
            var aluno = await _alunoRepository.Get(codAluno)
                ?? throw new Exception("Aluno não existe");

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? string.Empty));
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];

            var signCrentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);


            var tokenOption = new JwtSecurityToken(
                    issuer: issuer, audience: audience, claims: new[]
                    {
                        new Claim(type: ClaimTypes.Name, aluno.CodAluno.ToString()),
                    },
                    expires: DateTime.UtcNow.AddHours(1),
                    signingCredentials: signCrentials);

            var token = new JwtSecurityTokenHandler().WriteToken(tokenOption);
            return token;
        }

        public string GenereteTokenDefault()
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? string.Empty));
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];

            var signCrentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);


            var tokenOption = new JwtSecurityToken(
                    issuer: issuer, audience: audience, claims: new[]
                    {
                        new Claim(type: ClaimTypes.Name, "default"),
                    },
                    expires: DateTime.UtcNow.AddHours(1),
                    signingCredentials: signCrentials);

            var token = new JwtSecurityTokenHandler().WriteToken(tokenOption);
            return token;
        }
    }
}
