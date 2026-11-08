using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BeaconSync.API.Middlewares;

public class IdempotencyMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IMemoryCache _cache;

    public IdempotencyMiddleware(RequestDelegate next, IMemoryCache cache)
    {
        _next = next;
        _cache = cache;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Apenas aplicamos idempotência em métodos POST (operações de escrita críticas)
        if (HttpMethods.IsPost(context.Request.Method) && context.Request.Headers.ContainsKey("X-Idempotency-Key"))
        {
            var idempotencyKey = context.Request.Headers["X-Idempotency-Key"].ToString();
            var cacheKey = $"Idempotency-{idempotencyKey}";

            // Se já processámos esta chave, retornamos a resposta guardada em cache
            if (_cache.TryGetValue(cacheKey, out CachedResponse? cachedResponse))
            {
                context.Response.StatusCode = cachedResponse!.StatusCode;
                context.Response.ContentType = cachedResponse.ContentType;
                await context.Response.WriteAsync(cachedResponse.Body);
                return;
            }

            // Intercepta a resposta original para salvá-la no cache
            var originalBodyStream = context.Response.Body;
            using var memoryStream = new MemoryStream();
            context.Response.Body = memoryStream;

            await _next(context);

            memoryStream.Position = 0;
            var responseBody = await new StreamReader(memoryStream).ReadToEndAsync();
            memoryStream.Position = 0;

            // Se a operação foi bem-sucedida (2xx), guardamos no cache por 10 minutos
            if (context.Response.StatusCode >= 200 && context.Response.StatusCode < 300)
            {
                var responseToCache = new CachedResponse
                {
                    StatusCode = context.Response.StatusCode,
                    ContentType = context.Response.ContentType ?? "application/json",
                    Body = responseBody
                };

                _cache.Set(cacheKey, responseToCache, TimeSpan.FromMinutes(10));
            }

            await memoryStream.CopyToAsync(originalBodyStream);
            return;
        }

        await _next(context);
    }
}

public class CachedResponse
{
    public int StatusCode { get; set; }
    public string ContentType { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
}