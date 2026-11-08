using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BeaconSync.API.Controllers;

[ApiController]
[Route("api/v1/auth")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public AuthController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    /// <summary>
    /// Realiza o login para gerar o Token JWT de acesso aos endpoints protegidos.
    /// </summary>
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginModel model)
    {
        // Credenciais de teste simples para validação do projeto
        if (model.Email == "admin@beaconsync.com" && model.Senha == "123456")
        {
            var jwtKey = _configuration["JwtSettings:Key"] ?? "ChaveSuperSecretaParaOProjetoBeaconSync2026";
            var key = Encoding.UTF8.GetBytes(jwtKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, model.Email),
                    new Claim(ClaimTypes.Role, "Admin")
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                Issuer = "BeaconSync",
                Audience = "BeaconSyncApp",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new
            {
                success = true,
                token = tokenString,
                message = "Token gerado com sucesso! Copie este token e cole no botão 'Authorize' do Swagger."
            });
        }

        return Unauthorized(new { success = false, message = "E-mail ou senha inválidos. Use admin@beaconsync.com / 123456" });
    }
}

public class LoginModel
{
    public string Email { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
}