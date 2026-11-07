using System.Text;
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http;
using BeaconSync.Infrastructure.Extensions;
using BeaconSync.Application.Extensions; // <-- Namespace dos Services da Application
using BeaconSync.Application.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Registra as dependências da Infraestrutura (Banco Oracle) e da Aplicação (Services)
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication(); // <-- Adicionado para carregar os Services

// 1. Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 2. HealthChecks
builder.Services.AddHealthChecks()
    .AddCheck("self", () => Microsoft.Extensions.Diagnostics.HealthChecks.HealthCheckResult.Healthy());

// 3. JWT Authentication
var jwtKey = builder.Configuration["JwtSettings:Key"] ?? "ChaveSuperSecretaParaOProjetoBeaconSync2026";
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "BeaconSync",
            ValidAudience = "BeaconSyncApp",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });
builder.Services.AddAuthorization();

// 4. Rate Limiting (Proteção contra uso excessivo)
builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
    options.AddFixedWindowLimiter("GlobalPolicy", limiterOptions =>
    {
        limiterOptions.PermitLimit = 100; // 100 requisições
        limiterOptions.Window = TimeSpan.FromMinutes(1); // por minuto
        limiterOptions.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        limiterOptions.QueueLimit = 2;
    });
});

// Cache para a Idempotência
builder.Services.AddDistributedMemoryCache();

var app = builder.Build();

// Configuração do Pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseRateLimiter();

app.MapHealthChecks("/health");

// Endpoint Minimal API Integrado com o Service de Pessoas Vulneráveis
app.MapGet("/api/vulneraveis", async (IPessoaVulneravelService service) =>
{
    var pessoas = await service.GetAllAsync();
    return Results.Ok(pessoas);
})
.RequireAuthorization()
.RequireRateLimiting("GlobalPolicy")
.WithTags("Pessoas Vulneráveis")
.WithOpenApi();

app.Run();

// Necessário para os Testes de Integração reconhecerem a API
public partial class Program { }