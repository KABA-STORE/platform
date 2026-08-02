using System.Text.Json.Serialization;

namespace Kaba.Platform.PrestaShop.Categories;

/// <summary>
/// Represents the paginated category collection returned by PrestaShop.
/// </summary>
public sealed class CategoryListResponse
{
    /// <summary>
    /// Gets the total number of categories available.
    /// </summary>
    [JsonPropertyName("totalItems")]
    public int TotalItems { get; init; }

    /// <summary>
    /// Gets the categories included in the current response.
    /// </summary>
    [JsonPropertyName("items")]
    public IReadOnlyList<CategorySummary> Items { get; init; } =
        Array.Empty<CategorySummary>();
}

/// <summary>
/// Represents the category fields needed by the KABA Platform.
/// </summary>
public sealed class CategorySummary
{
    /// <summary>
    /// Gets the PrestaShop category identifier.
    /// </summary>
    [JsonPropertyName("categoryId")]
    public int CategoryId { get; init; }

    /// <summary>
    /// Gets a value indicating whether the category is enabled.
    /// </summary>
    [JsonPropertyName("enabled")]
    public bool Enabled { get; init; }

    /// <summary>
    /// Gets the category name.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;
}
