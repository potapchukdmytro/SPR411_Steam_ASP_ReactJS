namespace SPR411_SteamClone.API.Middlewares
{
    public class ExampleMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExampleMiddleware> _logger;

        public ExampleMiddleware(RequestDelegate next, ILogger<ExampleMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Request
            var httpMethod = context.Request.Method;
            var url = $"{context.Request.Host}{context.Request.Path}";
            var date = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
            string? query = context.Request.QueryString.Value;

            var headers = context.Request.Headers.Select(h => (h.Key, string.Join("; ", h.Value.Select(v => v.ToString()))));
            var headersString = string.Join("\n\t", headers.Select(h => $"{h.Key} - {h.Item2}"));

            _logger.LogInformation($"[{date}] - Request\n\tmethod: {httpMethod}\n\turl: {url}\n\tquery: {query}\n\theaders:\n\t{headersString}");

            await _next(context);

            // Response

            var statusCode = context.Response.StatusCode;
            var responseHeaders = context.Response.Headers.Select(h => (h.Key, string.Join("; ", h.Value.Select(v => v.ToString()))));
            var responseHeadersString = string.Join("\n\t", responseHeaders.Select(h => $"{h.Key} - {h.Item2}"));

            _logger.LogInformation($"[{date}] - Response\n\tstatus code: {statusCode}\n\theaders:\n\t{responseHeadersString}");

        }
    }
}
