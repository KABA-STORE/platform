namespace Kaba.Platform.PrestaShop.Abstractions;

public interface ITokenService
{
    Task<string> GetAccessTokenAsync(
        CancellationToken cancellationToken = default);
}
