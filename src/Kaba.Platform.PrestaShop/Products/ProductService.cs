using Kaba.Platform.PrestaShop.Abstractions;

namespace Kaba.Platform.PrestaShop.Products;

/// <summary>
/// Provides product operations backed by the PrestaShop Admin API.
/// </summary>
/// <remarks>
/// This service contains product-specific business operations while
/// delegating authentication, HTTP communication, JSON serialization,
/// and API error handling to <see cref="IPrestaShopClient"/>.
/// </remarks>
public sealed class ProductService : IProductService
{
    private readonly IPrestaShopClient _prestaShopClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProductService"/> class.
    /// </summary>
    /// <param name="prestaShopClient">
    /// The authenticated client used to communicate with PrestaShop.
    /// </param>
    public ProductService(IPrestaShopClient prestaShopClient)
    {
        _prestaShopClient = prestaShopClient;
    }

    /// <inheritdoc />
    public Task<ProductListResponse> GetAllAsync(
        int limit = 50,
        CancellationToken cancellationToken = default)
    {
        if (limit is < 1 or > 100)
        {
            throw new ArgumentOutOfRangeException(
                nameof(limit),
                limit,
                "The product limit must be between 1 and 100.");
        }

        return _prestaShopClient.GetAsync<ProductListResponse>(
            $"admin-api/products?limit={limit}",
            cancellationToken);
    }

    /// <inheritdoc />
    public Task<ProductResponse> CreateBasicAsync(
        string name,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException(
                "A product name is required.",
                nameof(name));
        }

        var command = new CreateProductCommand
        {
            Type = "standard",
            Names = new Dictionary<string, string>
            {
                // Our store currently operates in English.
                // When multilingual support is introduced, this dictionary
                // will contain one entry per installed language.
                ["en-US"] = name.Trim()
            }
        };

        return _prestaShopClient.PostAsync<
            CreateProductCommand,
            ProductResponse>(
            "admin-api/products",
            command,
            cancellationToken);
    }
}
