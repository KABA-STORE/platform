namespace Kaba.Platform.PrestaShop.Configuration;

public sealed class PrestaShopOptions
{
    public const string SectionName = "PrestaShop";

    public required string BaseUrl { get; init; }

    public required string ClientId { get; init; }

    public required string ClientSecret { get; init; }
}
