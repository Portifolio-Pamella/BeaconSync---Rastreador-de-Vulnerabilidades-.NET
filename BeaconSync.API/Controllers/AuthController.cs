using Microsoft.AspNetCore.Mvc;

namespace BeaconSync.API.Controllers;

[ApiController]
[Route("api/v1/auth")]
public class AuthController : ControllerBase
{
    [HttpPost("login")]
    public IActionResult Login()
    {
        // Conforme o contrato de integração, o login local do .NET foi descontinuado.
        // O ecossistema agora utiliza exclusivamente o sistema de autenticação centralizado em Java.
        return StatusCode(StatusCodes.Status410Gone, new
        {
            success = false,
            statusCode = 410,
            message = "O login nativo do .NET foi descontinuado. Por favor, utilize o endpoint de autenticação da API Java para gerar o token JWT."
        });
    }
}