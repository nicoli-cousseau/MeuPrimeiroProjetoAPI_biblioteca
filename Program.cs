using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "API da Biblioteca está no ar!");

app.MapGet("/api/livros", () =>
{
    return Results.Ok(new[]
    {
        new { id = 1, titulo = "Dom Casmurro", disponivel = true },
        new { id = 2, titulo = "Capitães da Areia", disponivel = false }
    });
});

app.MapGet("/api/livros/{id:int}", (int id) =>
{
    if (id == 1) return Results.Ok(new { id = 1, titulo = "Dom Casmurro", disponivel = true });
    if (id == 2) return Results.Ok(new { id = 2, titulo = "Capitães da Areia", disponivel = false });
    return Results.NotFound(new { mensagem = "Livro não encontrado." });
});

app.MapPost("/api/livros", async (HttpRequest requisicao) =>
{
    using JsonDocument documento = await JsonDocument.ParseAsync(requisicao.Body);
    string titulo = documento.RootElement.GetProperty("titulo").GetString() ?? "";
    return Results.Created("/api/livros/3", new { id = 3, titulo, disponivel = true });
});

app.MapPut("/api/livros/{id:int}", async (int id, HttpRequest requisicao) =>
{
    if (id != 1 && id != 2) return Results.NotFound(new { mensagem = "Livro não encontrado." });
    using JsonDocument documento = await JsonDocument.ParseAsync(requisicao.Body);
    string titulo = documento.RootElement.GetProperty("titulo").GetString() ?? "";
    return Results.Ok(new { id, titulo, disponivel = true, mensagem = "Livro atualizado." });
});

app.MapDelete("/api/livros/{id:int}", (int id) =>
{
    if (id != 1 && id != 2) return Results.NotFound(new { mensagem = "Livro não encontrado." });
    return Results.NoContent();
});

app.Run();