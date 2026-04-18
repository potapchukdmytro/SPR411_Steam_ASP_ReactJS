using Microsoft.Extensions.Primitives;
using System.Reflection.PortableExecutable;

namespace SPR411_SteamClone.API.Middlewares
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ApiKeyMiddleware> _logger;

        public ApiKeyMiddleware(RequestDelegate next, ILogger<ApiKeyMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if(context.Request.Path.Value.Contains("api/auth/"))
            {
                await _next(context);
                return;
            }

            string apiKey = "123456";

            // Request
            bool res = context.Request.Query.TryGetValue("apiKey", out StringValues apiQuery);
            
            if(res)
            {
                var apiQueryValue = apiQuery.ToString();

                if(apiQueryValue != null && apiQueryValue == apiKey)
                {
                    await _next(context);
                    return;
                }
            }

            // Response
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/text";
            await context.Response.WriteAsync("Your API key is missing. Append this to the URL with the apiKey param, or use the x-api-key HTTP header.");

            return;
        }
    }
}
