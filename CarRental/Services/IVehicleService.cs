using CarRental.DTOs;
using CarRental.Models;
namespace CarRental.Services;
public interface IVehicleService

{
    Task<List<Vehicle>> GetAvailableCarsAsync();
    Task AddCarAsync(Vehicle car);
    Task UpdateCarAsync(Vehicle car);
    Task DeleteCarAsync(int id);
    Task<List<Vehicle>> GetCarsByBrandAsync(string brand);
    Task<List<Vehicle>> GetCarsByModelAsync(string model);
    Task<List<Vehicle>> GetCarsBySeatsAsync(int seats);
    Task<List<Vehicle>> GetCarsByGearboxAsync(string gearbox);

    Task<List<Vehicle>> SearchVehiclesAsync(SearchVehicleDto searchDto, string token);

}
