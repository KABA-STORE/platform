using System.Text.Json.Serialization;

namespace Kaba.Platform.PrestaShop.Products;

/// <summary>
/// Represents the essential fields returned after creating a product.
/// </summary>
public sealed class ProductResponse
{
    /// <summary>
    /// Gets the PrestaShop product identifier.
    /// </summary>
    [JsonPropertyName("productId")]
    public int ProductId { get; init; }

    /// <summary>
    /// Gets the PrestaShop product type.
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; init; } = string.Empty;

    /// <summary>
    /// Gets a value indicating whether the product is enabled.
    /// </summary>
    [JsonPropertyName("enabled")]
    public bool Enabled { get; init; }

    /// <summary>
    /// Gets the localized product names.
    /// </summary>
    [JsonPropertyName("names")]
    public IReadOnlyDictionary<string, string> Names { get; init; } =
        new Dictionary<string, string>();
}
