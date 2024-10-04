public class NumberInterpretationMiddleware
{
    private readonly RequestDelegate _next;

    public NumberInterpretationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Path.StartsWithSegments("/api/number") && context.Request.Method == HttpMethods.Get)
        {
            var numberQuery = context.Request.Query["number"];

            if (int.TryParse(numberQuery, out int number) && number >= 1 && number <= 100000)
            {
                var interpretation = number % 2 == 0 ? "четное" : "нечетное";
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync($"Число {number} — {interpretation}.");
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("Ошибка: число должно быть от 1 до 100000.");
            }
        }
        else
        {
            await _next(context); 
        }
    }
}
