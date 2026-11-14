using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using BeaconSync.Application.Extensions;
using BeaconSync.Infrastructure.Extensions;
using BeaconSync.API.Middlewares;
using System.Text;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Leitura das chaves do JWT a partir do appsettings.json
var jwtKey = builder.Configuration["JwtSettings:Key"] ?? "ChaveSuperSecretaParaOProjetoBeaconSync2026";

// Adiciona as camadas de Application e Infrastructure
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

// Adiciona o Memory Cache necessário para a Idempotência
builder.Services.AddMemoryCache();

// Configuração do Rate Limiting (Requisito 04)
builder.Services.AddRateLimiter(options =>
{
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
        RateLimitPartition.GetFixedWindowLimiter(
            // Identifica por utilizador logado ou pelo IP remoto
            partitionKey: context.User.Identity?.Name ?? context.Connection.RemoteIpAddress?.ToString() ?? "anonymous",
            factory: _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 10, // Máximo de 10 requisições para teste de 429
                Window = TimeSpan.FromMinutes(1), // Por cada 1 minuto
                QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                QueueLimit = 2
            }));

    // Retorna HTTP 429 Too Many Requests personalizado
    options.OnRejected = async (context, cancellationToken) =>
    {
        context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
        context.HttpContext.Response.ContentType = "application/json";
        await context.HttpContext.Response.WriteAsync(
            "{\"success\": false, \"statusCode\": 429, \"message\": \"Muitas requisições efetuadas. Por favor, tente novamente mais tarde.\"}",
            cancellationToken);
    };
});

// Health Checks
builder.Services.AddHealthChecks()
    .AddCheck("self", () => Microsoft.Extensions.Diagnostics.HealthChecks.HealthCheckResult.Healthy(), tags: new[] { "live" });

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Configuração do Swagger com Suporte a JWT Bearer
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "BeaconSync API", Version = "v1" });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insira o token JWT desta forma: Bearer {seu token}"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

// Configuração da Autenticação JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),

            ValidateIssuer = true,
            ValidIssuer = "BeaconSync",

            ValidateAudience = true,
            ValidAudience = "BeaconSyncApp",

            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "BeaconSync API v1");
    c.RoutePrefix = string.Empty; // Abre o Swagger diretamente na raiz (http://localhost:5006/)
});

// Mapeamento dos Health Checks
app.MapHealthChecks("/health/live", new HealthCheckOptions
{
    Predicate = check => check.Tags.Contains("live")
});

app.UseHttpsRedirection();

// Ordem correta dos Middlewares
app.UseAuthentication();
app.UseAuthorization();

// Rate Limiter e Idempotência após autenticação
app.UseRateLimiter();
app.UseMiddleware<IdempotencyMiddleware>();

app.MapControllers();

app.Run();

public partial class Program { }