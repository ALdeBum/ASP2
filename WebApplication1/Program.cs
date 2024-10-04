using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// ��������� ����������� ������� (���� ��� ����)
builder.Services.AddRouting(); // ���� � ��� ���� ��������

var app = builder.Build();

// ��������� ������������� ����������� ������
app.UseStaticFiles(); // ��� ������������ ������� � ������ � ����� wwwroot

// ��������� middleware ��� ��������� ������������� �����
app.UseMiddleware<NumberInterpretationMiddleware>();

// �����������: ���� �� ������ ������ index.html ��� ������� � ��������� URL
app.MapGet("/", async context =>
{
    context.Response.ContentType = "text/html; charset=utf-8";
    await context.Response.SendFileAsync("wwwroot/index.html");
});

// ������ ����������
app.Run();
