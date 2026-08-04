using Kaba.Platform.PrestaShop.Abstractions;
using Kaba.Platform.PrestaShop.Authentication;
using Kaba.Platform.PrestaShop.Categories;
using Kaba.Platform.PrestaShop.Configuration;
using Kaba.Platform.PrestaShop.Http;
using Kaba.Platform.PrestaShop.Products;

var builder = WebApplication.CreateBuilder(args);

var prestaShopOptions = new PrestaShopOptions
{
    BaseUrl = Environment.GetEnvironmentVariable("PRESTASHOP_URL")
        ?? throw new InvalidOperationException(
            "PRESTASHOP_URL is not configured."),

    ClientId = Environment.GetEnvironmentVariable("PRESTASHOP_CLIENT_ID")
        ?? throw new InvalidOperationException(
            "PRESTASHOP_CLIENT_ID is not configured."),

    ClientSecret = Environment.GetEnvironmentVariable(
        "PRESTASHOP_CLIENT_SECRET")
        ?? throw new InvalidOperationException(
            "PRESTASHOP_CLIENT_SECRET is not configured.")
};

builder.Services.AddSingleton(prestaShopOptions);

// The token service handles OAuth authentication and caches access tokens.
builder.Services.AddHttpClient<ITokenService, OAuthTokenService>();

// All PrestaShop services use this authenticated client instead of
// handling HTTP headers, tokens, serialization, or errors themselves.
builder.Services.AddHttpClient<IPrestaShopClient, PrestaShopClient>();

// Catalog services use the shared authenticated PrestaShop client.
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();

var app = builder.Build();

app.MapGet("/", () => Results.Ok(new
{
    service = "KABA Platform API",
    status = "running",
    version = "0.2.0"
}));

app.MapGet("/health", () => Results.Ok(new
{
    status = "healthy",
    timestamp = DateTimeOffset.UtcNow
}));

app.MapGet(
    "/internal/prestashop/token-status",
    async (
        ITokenService tokenService,
        CancellationToken cancellationToken) =>
    {
        var token = await tokenService.GetAccessTokenAsync(
            cancellationToken);

        return Results.Ok(new
        {
            status = "authenticated",
            tokenReceived = !string.IsNullOrWhiteSpace(token)
        });
    });

app.MapGet(
    "/internal/prestashop/categories",
    async (
        ICategoryService categoryService,
        CancellationToken cancellationToken) =>
    {
        var categories = await categoryService.GetAllAsync(
            cancellationToken);

        return Results.Ok(categories);
    });

app.MapGet(
    "/internal/prestashop/products",
    async (
        IProductService productService,
        int? limit,
        CancellationToken cancellationToken) =>
    {
        var products = await productService.GetAllAsync(
            limit ?? 50,
            cancellationToken);

        return Results.Ok(products);
    });

app.MapPost(
    "/internal/prestashop/products/basic",
    async (
        CreateBasicProductRequest request,
        IProductService productService,
        CancellationToken cancellationToken) =>
    {
        var product = await productService.CreateBasicAsync(
            request.Name,
            cancellationToken);

        return Results.Created(
            $"/internal/prestashop/products/{product.ProductId}",
            product);
    });

app.Run();

/// <summary>
/// Represents the minimal API request used to create the initial
/// PrestaShop product record.
/// </summary>
public sealed record CreateBasicProductRequest(string Name);
