using System.Text.Json;
using Kaba.Platform.PrestaShop.Abstractions;
using Kaba.Platform.PrestaShop.Configuration;
using Kaba.Platform.PrestaShop.Models;

namespace Kaba.Platform.PrestaShop.Authentication;

public sealed class OAuthTokenService : ITokenService, IDisposable
{
    private const string RequestedScopes =
        "product_read product_write " +
        "category_read category_write " +
        "manufacturer_read manufacturer_write " +
        "feature_read feature_write " +
        "feature_value_read feature_value_write " +
        "attribute_read attribute_write " +
        "attribute_group_read attribute_group_write " +
        "supplier_read supplier_write " +
        "tax_read tax_rules_group_read";

    private readonly HttpClient _httpClient;
    private readonly PrestaShopOptions _options;
    private readonly SemaphoreSlim _refreshLock = new(1, 1);

    private string? _cachedAccessToken;
    private DateTimeOffset _tokenExpiresAtUtc;

    public OAuthTokenService(
        HttpClient httpClient,
        PrestaShopOptions options)
    {
        _httpClient = httpClient;
        _options = options;

        if (string.IsNullOrWhiteSpace(_options.BaseUrl))
        {
            throw new ArgumentException(
                "PrestaShop BaseUrl is required.",
                nameof(options));
        }

        if (string.IsNullOrWhiteSpace(_options.ClientId))
        {
            throw new ArgumentException(
                "PrestaShop ClientId is required.",
                nameof(options));
        }

        if (string.IsNullOrWhiteSpace(_options.ClientSecret))
        {
            throw new ArgumentException(
                "PrestaShop ClientSecret is required.",
                nameof(options));
        }
    }

    public async Task<string> GetAccessTokenAsync(
        CancellationToken cancellationToken = default)
    {
        if (HasValidCachedToken())
        {
            return _cachedAccessToken!;
        }

        await _refreshLock.WaitAsync(cancellationToken);

        try
        {
            // Another request may have refreshed the token while we waited.
            if (HasValidCachedToken())
            {
                return _cachedAccessToken!;
            }

            return await RequestNewTokenAsync(cancellationToken);
        }
        finally
        {
            _refreshLock.Release();
        }
    }

    private bool HasValidCachedToken()
    {
        return !string.IsNullOrWhiteSpace(_cachedAccessToken)
            && DateTimeOffset.UtcNow < _tokenExpiresAtUtc;
    }

    private async Task<string> RequestNewTokenAsync(
        CancellationToken cancellationToken)
    {
        var tokenEndpoint =
            $"{_options.BaseUrl.TrimEnd('/')}/admin-api/access_token";

        using var requestContent = new FormUrlEncodedContent(
            new Dictionary<string, string>
            {
                ["grant_type"] = "client_credentials",
                ["client_id"] = _options.ClientId,
                ["client_secret"] = _options.ClientSecret,
                ["scope"] = RequestedScopes
            });

        using var response = await _httpClient.PostAsync(
            tokenEndpoint,
            requestContent,
            cancellationToken);

        response.EnsureSuccessStatusCode();

        var tokenResponse =
            await JsonSerializer.DeserializeAsync<OAuthTokenResponse>(
                await response.Content.ReadAsStreamAsync(cancellationToken),
                cancellationToken: cancellationToken);

        if (tokenResponse is null ||
            string.IsNullOrWhiteSpace(tokenResponse.AccessToken))
        {
            throw new InvalidOperationException(
                "PrestaShop returned an invalid OAuth token response.");
        }

        _cachedAccessToken = tokenResponse.AccessToken;

        // Refresh one minute before the token's actual expiry.
        var safeLifetimeSeconds = Math.Max(
            tokenResponse.ExpiresIn - 60,
            30);

        _tokenExpiresAtUtc = DateTimeOffset.UtcNow.AddSeconds(
            safeLifetimeSeconds);

        return _cachedAccessToken;
    }

    public void Dispose()
    {
        _refreshLock.Dispose();
    }
}
