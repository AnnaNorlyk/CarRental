using CarRental.DTOs;
using CarRental.Models;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Net.Http.Headers;

internal class VehicleService : IVehicleService
{
    private readonly HttpClient _httpClient;

    public VehicleService(HttpClient httpClient)
    {
        
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("http://localhost:3000/api/");
    }

    public async Task<List<Vehicle>> GetAllVehiclesAsync(string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await _httpClient.GetAsync("vehicles");

        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<Vehicle>>();
    }

    public async Task<Vehicle> GetVehicleByIdAsync(string id, string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await _httpClient.GetAsync($"vehicles/{id}");

        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Vehicle>();
    }

    public async Task<List<Vehicle>> SearchVehiclesAsync(SearchVehicleDto dto, string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var json = JsonSerializer.Serialize(dto);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("vehicles/search", content);

        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<Vehicle>>();
    }

    // placeholder method for getting available cars
    public async Task<List<Vehicle>> GetAvailableCarsAsync()
    {
        var dummyToken = await SecureStorage.GetAsync("auth_token");
        var dto = new SearchVehicleDto
        {
            StartDate = DateTime.Today.ToString("yyyy-MM-dd"),
            EndDate = DateTime.Today.AddDays(3).ToString("yyyy-MM-dd"),
            Seats = 4,
            TransmissionType = "automatic"
        };

        return await SearchVehiclesAsync(dto, dummyToken);
    }

    //  not implemented yet
    public Task AddCarAsync(Vehicle car) => throw new NotImplementedException();
    public Task DeleteCarAsync(int id) => throw new NotImplementedException();
    public Task UpdateCarAsync(Vehicle car) => throw new NotImplementedException();
    public Task<List<Vehicle>> GetCarsByBrandAsync(string brand) => throw new NotImplementedException();
    public Task<List<Vehicle>> GetCarsByGearboxAsync(string gearbox) => throw new NotImplementedException();
    public Task<List<Vehicle>> GetCarsByModelAsync(string model) => throw new NotImplementedException();
    public Task<List<Vehicle>> GetCarsBySeatsAsync(int seats) => throw new NotImplementedException();
 

}
