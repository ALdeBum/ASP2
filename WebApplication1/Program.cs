using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Добавляем необходимые сервисы (если они есть)
builder.Services.AddRouting(); // Если у вас есть маршруты

var app = builder.Build();

// Разрешаем использование статических файлов
app.UseStaticFiles(); // Это обрабатывает запросы к файлам в папке wwwroot

// Добавляем middleware для обработки интерпретации чисел
app.UseMiddleware<NumberInterpretationMiddleware>();

// Опционально: если вы хотите отдать index.html при запросе к корневому URL
app.MapGet("/", async context =>
{
    context.Response.ContentType = "text/html; charset=utf-8";
    await context.Response.SendFileAsync("wwwroot/index.html");
});

// Запуск приложения
app.Run();
