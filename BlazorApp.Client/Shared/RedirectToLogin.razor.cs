using Microsoft.AspNetCore.Components;


namespace BlazorApp.Client.Shared
{
	public partial class RedirectToLogin
	{
		[Inject] public NavigationManager Navigation { get; set; }

		protected override async Task OnInitializedAsync()
		{
			Navigation.NavigateTo($"/login?redirectUri={Uri.EscapeDataString(Navigation.Uri)}", true);
		}
	}
}
