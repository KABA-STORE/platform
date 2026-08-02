using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Kaba.Platform.PrestaShop.Abstractions;
using Kaba.Platform.PrestaShop.Configuration;
using Kaba.Platform.PrestaShop.Exceptions;

namespace Kaba.Platform.PrestaShop.Http;

/// <summary>
/// Sends authenticated HTTP requests to the PrestaShop Admin API.
/// </summary>
/// <remarks>
/// The client obtains OAuth access tokens through <see cref="ITokenService"/>,
/// attaches the bearer token to each request, serializes request payloads,
/// deserializes responses, and converts unsuccessful API responses into
/// <see cref="PrestaShopApiException"/> instances.
/// </remarks>
public sealed class PrestaShopClient : IPrestaShopClient
{
    private static readonly JsonSerializerOptions JsonOptions =
        new(JsonSerializerDefaults.Web);

    private readonly HttpClient _httpClient;
    private readonly ITokenService _tokenService;

    /// <summary>
    /// Initializes a new instance of the <see cref="PrestaShopClient"/> class.
    /// </summary>
    /// <param name="httpClient">
    /// The HTTP client used to communicate with PrestaShop.
    /// </param>
    /// <param name="tokenService">
    /// The service responsible for providing valid OAuth access tokens.
    /// </param>
    /// <param name="options">
    /// The configured PrestaShop connection settings.
    /// </param>
    public PrestaShopClient(
        HttpClient httpClient,
        ITokenService tokenService,
        PrestaShopOptions options)
    {
        _httpClient = httpClient;
        _tokenService = tokenService;

        _httpClient.BaseAddress = new Uri(
            $"{options.BaseUrl.TrimEnd('/')}/");
    }

    /// <inheritdoc />
    public Task<TResponse> GetAsync<TResponse>(
        string relativePath,
        CancellationToken cancellationToken = default)
    {
        return SendAsync<object, TResponse>(
            HttpMethod.Get,
            relativePath,
            request: null,
            cancellationToken);
    }

    /// <inheritdoc />
    public Task<TResponse> PostAsync<TRequest, TResponse>(
        string relativePath,
        TRequest request,
        CancellationToken cancellationToken = default)
    {
        return SendAsync<TRequest, TResponse>(
            HttpMethod.Post,
            relativePath,
            request,
            cancellationToken);
    }

    /// <inheritdoc />
    public Task<TResponse> PatchAsync<TRequest, TResponse>(
        string relativePath,
        TRequest request,
        CancellationToken cancellationToken = default)
    {
        return SendAsync<TRequest, TResponse>(
            HttpMethod.Patch,
            relativePath,
            request,
            cancellationToken);
    }

    /// <inheritdoc />
    public async Task DeleteAsync(
        string relativePath,
        CancellationToken cancellationToken = default)
    {
        using var request = await CreateRequestAsync(
            HttpMethod.Delete,
            relativePath,
            cancellationToken);

        using var response = await _httpClient.SendAsync(
            request,
            cancellationToken);

        await EnsureSuccessAsync(
            response,
            relativePath,
            cancellationToken);
    }

    private async Task<TResponse> SendAsync<TRequest, TResponse>(
        HttpMethod method,
        string relativePath,
        TRequest? request,
        CancellationToken cancellationToken)
    {
        using var httpRequest = await CreateRequestAsync(
            method,
            relativePath,
            cancellationToken);

        if (request is not null)
        {
            httpRequest.Content = JsonContent.Create(
                request,
                options: JsonOptions);
        }

        using var response = await _httpClient.SendAsync(
            httpRequest,
            HttpCompletionOption.ResponseHeadersRead,
            cancellationToken);

        await EnsureSuccessAsync(
            response,
            relativePath,
            cancellationToken);

        var result = await response.Content.ReadFromJsonAsync<TResponse>(
            JsonOptions,
            cancellationToken);

        return result ?? throw new InvalidOperationException(
            $"PrestaShop returned an empty or invalid JSON response " +
            $"for '{relativePath}'.");
    }

    private async Task<HttpRequestMessage> CreateRequestAsync(
        HttpMethod method,
        string relativePath,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(relativePath))
        {
            throw new ArgumentException(
                "A PrestaShop API path is required.",
                nameof(relativePath));
        }

        var accessToken = await _tokenService.GetAccessTokenAsync(
            cancellationToken);

        var request = new HttpRequestMessage(
            method,
            relativePath.TrimStart('/'));

        // Authentication is applied here so product, category, stock, and
        // image services never need to handle access tokens directly.
        request.Headers.Authorization =
            new AuthenticationHeaderValue("Bearer", accessToken);

        request.Headers.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

        return request;
    }

    private static async Task EnsureSuccessAsync(
        HttpResponseMessage response,
        string relativePath,
        CancellationToken cancellationToken)
    {
        if (response.IsSuccessStatusCode)
        {
            return;
        }

        // Preserve the response body because PrestaShop often includes useful
        // validation details. Credentials and authorization headers are not
        // included in the exception.
        var responseBody = await response.Content.ReadAsStringAsync(
            cancellationToken);

        throw new PrestaShopApiException(
            response.StatusCode,
            relativePath,
            responseBody);
    }
}
