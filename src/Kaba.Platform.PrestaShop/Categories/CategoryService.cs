using Kaba.Platform.PrestaShop.Abstractions;

namespace Kaba.Platform.PrestaShop.Categories;

/// <summary>
/// Provides category operations backed by the PrestaShop Admin API.
/// </summary>
/// <remarks>
/// This service contains category-specific API paths and models while
/// delegating authentication, HTTP transport, JSON handling, and error
/// processing to <see cref="IPrestaShopClient"/>.
/// </remarks>
public sealed class CategoryService : ICategoryService
{
    private readonly IPrestaShopClient _prestaShopClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="CategoryService"/> class.
    /// </summary>
    /// <param name="prestaShopClient">
    /// The authenticated client used to communicate with PrestaShop.
    /// </param>
    public CategoryService(IPrestaShopClient prestaShopClient)
    {
        _prestaShopClient = prestaShopClient;
    }

    /// <inheritdoc />
    public Task<CategoryListResponse> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        // The collection endpoint currently returns up to 50 records by
        // default, which is sufficient for the existing KABA category count.
        // Pagination will be added before this service is considered complete.
        return _prestaShopClient.GetAsync<CategoryListResponse>(
            "admin-api/categories?limit=50",
            cancellationToken);
    }
}
