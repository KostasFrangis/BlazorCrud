using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using Pocos.BingMap;

namespace BlazorApp.Client.Pages
{
    public partial class BingMap : ComponentBase
    {
        private string SearchQuery { get; set; }
        private List<string> SearchResults { get; set; }
        private readonly HttpClient httpClient = new HttpClient();

        private async Task Search()
        {
            if (!string.IsNullOrWhiteSpace(SearchQuery))
            {
                var apiKey = "AsngcyeAQ4hvF1aBay2_DcxAfiCPu1ttKi6r0keqZjKuPwpaUeWJ8Cs7GuI6yro9";
                var baseUrl = "https://dev.virtualearth.net/REST/v1/Autosuggest";
                var query = Uri.EscapeDataString(SearchQuery);
                var url = $"{baseUrl}?query={query}&key={apiKey}";

                var response = await httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    Root root = JsonConvert.DeserializeObject<Root>(content);
                    List<Resource> resources = root.resourceSets[0].resources;

                    SearchResults = new List<string>();
                    foreach (var resource in resources)
                    {
                        foreach (var val in resource.value)
                        {
                            if (!String.IsNullOrEmpty(val.name))
                                SearchResults.Add(val.name);
                        }
                    }
                }
                else
                {
                    SearchResults = new List<string> { "Error occurred while fetching results." };
                }
            }
            else
            {
                SearchResults = new List<string> { "Please enter a search query." };
            }
        }
    }

    
}
