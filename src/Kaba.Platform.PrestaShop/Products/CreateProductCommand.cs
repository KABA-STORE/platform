using System.Text.Json.Serialization;

namespace Kaba.Platform.PrestaShop.Products;

/// <summary>
/// Represents the initial payload required by PrestaShop to create a product.
/// </summary>
/// <remarks>
/// PrestaShop initially accepts only the product type and localized names.
/// Additional catalog information is applied afterward through the product
/// update endpoint.
/// </remarks>
internal sealed class CreateProductCommand
{
    /// <summary>
    /// Gets the PrestaShop product type.
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; init; } = "standard";

    /// <summary>
    /// Gets the localized product names keyed by locale.
    /// </summary>
    [JsonPropertyName("names")]
    public IReadOnlyDictionary<string, string> Names { get; init; } =
        new Dictionary<string, string>();
}
