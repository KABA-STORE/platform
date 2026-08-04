using Kaba.Platform.PrestaShop.Products;

namespace Kaba.Platform.PrestaShop.Abstractions;

/// <summary>
/// Provides product operations for the PrestaShop catalog.
/// </summary>
public interface IProductService
{
    /// <summary>
    /// Returns products from the PrestaShop catalog.
    /// </summary>
    Task<ProductListResponse> GetAllAsync(
        int limit = 50,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates the initial PrestaShop product record.
    /// </summary>
    /// <remarks>
    /// PrestaShop initially accepts the product type and localized name.
    /// Additional product details are applied in later update operations.
    /// </remarks>
    /// <param name="name">The English product name.</param>
    /// <param name="cancellationToken">
    /// A token that can cancel the API request.
    /// </param>
    /// <returns>The product returned by PrestaShop after creation.</returns>
    Task<ProductResponse> CreateBasicAsync(
        string name,
        CancellationToken cancellationToken = default);
}
