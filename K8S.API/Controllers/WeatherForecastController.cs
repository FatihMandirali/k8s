using Microsoft.AspNetCore.Mvc;

namespace K8S.API.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController(IHttpClientFactory _httpClientFactory) : ControllerBase
{
    

    [HttpGet("GetWeatherForecast")]
    public async Task<string> GetWeatherForecast()
    {
        var client = _httpClientFactory.CreateClient("ApiService1");

        // Örnek bir istek
        var response = await client.GetAsync("test");

        // Başarılı yanıt kontrolü
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        return "hata";
    }

    [HttpGet("test")]
    public async Task<string> Test()
    {
        var client = _httpClientFactory.CreateClient("ApiService1");

        // Örnek bir istek
        var response = await client.GetAsync("test/List");

        // Başarılı yanıt kontrolü
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        return "hata";
    }
    
    [HttpGet( "test1")]
    public async Task<string> Test1([FromQuery] int id, [FromQuery] string name)
    {
        var client = _httpClientFactory.CreateClient("ApiService1");

        // Örnek bir istek
        var response = await client.PostAsJsonAsync("test",new SampleData
        {
            Id=id,
            Name = name
        });

        // Başarılı yanıt kontrolü
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        return "hata";
    }
}