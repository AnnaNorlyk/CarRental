using CarRental.Models;
using CarRental.DTOs;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

internal class VehicleService : IVehicleService
{
    private readonly HttpClient _httpClient;

    public VehicleService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Vehicle>> SearchVehiclesAsync(SearchVehicleDto dto, string token)
    {
        var json = JsonSerializer.Serialize(dto, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        var request = new HttpRequestMessage(HttpMethod.Post, "vehicles/search")
        {
            Content = new StringContent(json, Encoding.UTF8, "application/json")
        };
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            Console.WriteLine("API Error: " + error);
            return new List<Vehicle>();
        }

        return await response.Content.ReadFromJsonAsync<List<Vehicle>>() ?? new List<Vehicle>();
    }

    public async Task<List<Vehicle>> GetAllVehiclesAsync()
    {
        var response = await _httpClient.GetAsync("vehicles");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<Vehicle>>() ?? new List<Vehicle>();
    }



}

