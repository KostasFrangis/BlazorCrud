using IdentityModel.Client;

namespace BlazorApp.Server.Services
{
    public interface ITokenService
    {
        Task<TokenResponse> GetToken(string scope);
    }
}
