using IdentityModel.Client;

namespace BlazorApp.Client.Services
{
	public interface ITokenService
	{
		Task<TokenResponse> GetToken(string scope);
	}
}
