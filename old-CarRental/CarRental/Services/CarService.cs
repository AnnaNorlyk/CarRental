using System.Net.Http.Json;
using CarRental.Models;

namespace CarRental.Services;

public class CarService : ICarService
{
    private readonly HttpClient _httpClient;

    public CarService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Car>> GetAvailableCarsAsync()
    {
        var cars = await _httpClient.GetFromJsonAsync<List<Car>>("https://your-api-url.com/api/cars");
        return cars ?? new List<Car>();
    }
    public async Task<Car> GetCarByIdAsync(int id)
    {
        var car = await _httpClient.GetFromJsonAsync<Car>($"https://your-api-url.com/api/cars/{id}");
        return car;
    }
    public async Task AddCarAsync(Car car)
    {
        var response = await _httpClient.PostAsJsonAsync("https://your-api-url.com/api/cars", car);
        response.EnsureSuccessStatusCode();
    }
    public async Task UpdateCarAsync(Car car)
    {
        var response = await _httpClient.PutAsJsonAsync($"https://your-api-url.com/api/cars/{car.Id}", car);
        response.EnsureSuccessStatusCode();
    }
    public async Task DeleteCarAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"https://your-api-url.com/api/cars/{id}");
        response.EnsureSuccessStatusCode();
    }

    public async Task<List<Car>> GetCarsByBrandAsync(string brand)
    {
        var cars = await _httpClient.GetFromJsonAsync<List<Car>>($"https://your-api-url.com/api/cars/brand/{brand}");
        return cars ?? new List<Car>();
    }
    public async Task<List<Car>> GetCarsByModelAsync(string model)
    {
        var cars = await _httpClient.GetFromJsonAsync<List<Car>>($"https://your-api-url.com/api/cars/model/{model}");
        return cars ?? new List<Car>();
    }
    public async Task<List<Car>> GetCarsBySeatsAsync(int seats)
    {
        var cars = await _httpClient.GetFromJsonAsync<List<Car>>($"https://your-api-url.com/api/cars/seats/{seats}");
        return cars ?? new List<Car>();
    }
    public async Task<List<Car>> GetCarsByGearboxAsync(string gearbox)
    {
        var cars = await _httpClient.GetFromJsonAsync<List<Car>>($"https://your-api-url.com/api/cars/gearbox/{gearbox}");
        return cars ?? new List<Car>();
    }

}
