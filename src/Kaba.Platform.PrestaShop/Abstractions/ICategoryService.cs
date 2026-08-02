using Kaba.Platform.PrestaShop.Categories;

namespace Kaba.Platform.PrestaShop.Abstractions;

/// <summary>
/// Provides category operations for the PrestaShop catalog.
/// </summary>
public interface ICategoryService
{
    /// <summary>
    /// Returns the categories currently available in PrestaShop.
    /// </summary>
    /// <param name="cancellationToken">
    /// A token that can cancel the API request.
    /// </param>
    /// <returns>
    /// The category collection returned by the PrestaShop Admin API.
    /// </returns>
    Task<CategoryListResponse> GetAllAsync(
        CancellationToken cancellationToken = default);
}
