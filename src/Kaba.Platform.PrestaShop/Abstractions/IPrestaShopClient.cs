using System.Net.Http.Json;

namespace Kaba.Platform.PrestaShop.Abstractions;

/// <summary>
/// Provides authenticated access to the PrestaShop Admin API.
/// </summary>
/// <remarks>
/// Implementations are responsible for obtaining OAuth access tokens,
/// attaching authorization headers, sending HTTP requests, validating
/// responses, and deserializing JSON payloads.
///
/// Product, category, stock, and image services should use this abstraction
/// instead of creating or configuring their own HTTP requests.
/// </remarks>
public interface IPrestaShopClient
{
    /// <summary>
    /// Sends an authenticated GET request and deserializes the JSON response.
    /// </summary>
    /// <typeparam name="TResponse">
    /// The type into which the response body will be deserialized.
    /// </typeparam>
    /// <param name="relativePath">
    /// The Admin API path relative to the configured PrestaShop base URL.
    /// Example: <c>admin-api/categories</c>.
    /// </param>
    /// <param name="cancellationToken">
    /// A token that can cancel the HTTP operation.
    /// </param>
    /// <returns>The deserialized API response.</returns>
    Task<TResponse> GetAsync<TResponse>(
        string relativePath,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Sends an authenticated POST request containing a JSON payload and
    /// deserializes the JSON response.
    /// </summary>
    /// <typeparam name="TRequest">
    /// The type of the request payload.
    /// </typeparam>
    /// <typeparam name="TResponse">
    /// The type into which the response body will be deserialized.
    /// </typeparam>
    /// <param name="relativePath">
    /// The Admin API path relative to the configured PrestaShop base URL.
    /// </param>
    /// <param name="request">
    /// The payload to serialize and send to PrestaShop.
    /// </param>
    /// <param name="cancellationToken">
    /// A token that can cancel the HTTP operation.
    /// </param>
    /// <returns>The deserialized API response.</returns>
    Task<TResponse> PostAsync<TRequest, TResponse>(
        string relativePath,
        TRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Sends an authenticated PATCH request containing a JSON payload and
    /// deserializes the JSON response.
    /// </summary>
    Task<TResponse> PatchAsync<TRequest, TResponse>(
        string relativePath,
        TRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Sends an authenticated DELETE request.
    /// </summary>
    Task DeleteAsync(
        string relativePath,
        CancellationToken cancellationToken = default);
}
