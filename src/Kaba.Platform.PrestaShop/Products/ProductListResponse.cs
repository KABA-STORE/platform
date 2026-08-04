using System.Text.Json.Serialization;

namespace Kaba.Platform.PrestaShop.Products;

/// <summary>
/// Represents a paginated product collection returned by PrestaShop.
/// </summary>
public sealed class ProductListResponse
{
    /// <summary>
    /// Gets the total number of products available.
    /// </summary>
    [JsonPropertyName("totalItems")]
    public int TotalItems { get; init; }

    /// <summary>
    /// Gets the products included in the current response.
    /// </summary>
    [JsonPropertyName("items")]
    public IReadOnlyList<ProductSummary> Items { get; init; } =
        Array.Empty<ProductSummary>();
}

/// <summary>
/// Represents the product fields currently needed by the KABA Platform.
/// </summary>
public sealed class ProductSummary
{
    /// <summary>
    /// Gets the PrestaShop product identifier.
    /// </summary>
    [JsonPropertyName("productId")]
    public int ProductId { get; init; }

    /// <summary>
    /// Gets a value indicating whether the product is enabled.
    /// </summary>
    [JsonPropertyName("enabled")]
    public bool Enabled { get; init; }

    /// <summary>
    /// Gets the product name.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// Gets the available product quantity.
    /// </summary>
    [JsonPropertyName("quantity")]
    public int Quantity { get; init; }

    /// <summary>
    /// Gets the tax-exclusive price.
    /// </summary>
    [JsonPropertyName("priceTaxExcluded")]
    public decimal PriceTaxExcluded { get; init; }

    /// <summary>
    /// Gets the tax-inclusive price.
    /// </summary>
    [JsonPropertyName("priceTaxIncluded")]
    public decimal PriceTaxIncluded { get; init; }

    /// <summary>
    /// Gets the product's default category name.
    /// </summary>
    [JsonPropertyName("category")]
    public string Category { get; init; } = string.Empty;
}
