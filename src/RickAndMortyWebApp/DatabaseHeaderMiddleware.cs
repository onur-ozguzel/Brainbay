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
                if (response.Headers.ContainsKey("Age"))
                {
                    // Age header exists, meaning it's served from the cache
                    response.Headers["from-database"] = "false";
                }
                else
                {
                    // Otherwise, the request was processed normally
                    response.Headers["from-database"] = "true";
                }

                return Task.CompletedTask;
            });

            await next(context);
        }
    }
}
