using System.Net;

namespace Kaba.Platform.PrestaShop.Exceptions;

/// <summary>
/// Represents an unsuccessful response returned by the PrestaShop Admin API.
/// </summary>
/// <remarks>
/// The exception preserves the HTTP status code, request path, and response
/// body to support troubleshooting without exposing OAuth credentials.
/// </remarks>
public sealed class PrestaShopApiException : Exception
{
    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="PrestaShopApiException"/> class.
    /// </summary>
    /// <param name="statusCode">
    /// The HTTP status code returned by PrestaShop.
    /// </param>
    /// <param name="requestPath">
    /// The relative Admin API path that failed.
    /// </param>
    /// <param name="responseBody">
    /// The response body returned by PrestaShop, when available.
    /// </param>
    public PrestaShopApiException(
        HttpStatusCode statusCode,
        string requestPath,
        string? responseBody)
        : base(
            $"PrestaShop API request to '{requestPath}' failed " +
            $"with status code {(int)statusCode} ({statusCode}).")
    {
        StatusCode = statusCode;
        RequestPath = requestPath;
        ResponseBody = responseBody;
    }

    /// <summary>
    /// Gets the HTTP status code returned by PrestaShop.
    /// </summary>
    public HttpStatusCode StatusCode { get; }

    /// <summary>
    /// Gets the relative path of the failed request.
    /// </summary>
    public string RequestPath { get; }

    /// <summary>
    /// Gets the response body returned by PrestaShop, when available.
    /// </summary>
    public string? ResponseBody { get; }
}
