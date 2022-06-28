using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Net.Http.Headers;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.RazorPages;
using V2Layground.WebClient.Models;

namespace V2Layground.WebClient.Pages
{
    public class WeatherforecastModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public WeatherForecast[]? Forecast { get; set; }

        public WeatherforecastModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task OnGetAsync()
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var client = _httpClientFactory.CreateClient("resource-server-1-http-client");
            client.DefaultRequestHeaders.Add(HeaderNames.Authorization, $"Bearer {token}");

            var responseMessage = await client.GetAsync("weatherforecast");
            if(!responseMessage.IsSuccessStatusCode) return;
            var result = await responseMessage.Content.ReadFromJsonAsync<WeatherForecast[]>();

            this.Forecast = result ?? new WeatherForecast[0];
        }
    }
}
