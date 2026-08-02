using System.Text.Json.Serialization;

namespace Kaba.Platform.PrestaShop.Models;

internal sealed class OAuthTokenResponse
{
    [JsonPropertyName("token_type")]
    public string TokenType { get; init; } = string.Empty;

    [JsonPropertyName("expires_in")]
    public int ExpiresIn { get; init; }

    [JsonPropertyName("access_token")]
    public string AccessToken { get; init; } = string.Empty;
}
