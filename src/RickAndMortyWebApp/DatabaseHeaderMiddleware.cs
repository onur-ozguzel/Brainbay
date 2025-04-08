namespace RickAndMortyWebApp
{
    public class DatabaseHeaderMiddleware(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            var response = context.Response;

            // Check if it's going to be served from the cache by using OnStarting
            response.OnStarting(() =>
            {
                if (!response.Headers.ContainsKey("Age"))
                {
                    response.Headers["from-database"] = string.Empty;
                }

                return Task.CompletedTask;
            });

            await next(context);
        }
    }
}
