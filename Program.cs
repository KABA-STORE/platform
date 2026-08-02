var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/", () => Results.Ok(new
{
    service = "KABA Connector API",
    status = "running",
    version = "1.0.0"
}));

app.MapGet("/health", () => Results.Ok(new
{
    status = "healthy",
    timestamp = DateTimeOffset.UtcNow
}));

app.Run();
