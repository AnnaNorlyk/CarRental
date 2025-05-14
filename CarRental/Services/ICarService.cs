using CarRental.Models;
namespace CarRental.Services;
public interface ICarService
{
    Task<List<Car>> GetAvailableCarsAsync();
    Task<Car> GetCarByIdAsync(int id);
    Task AddCarAsync(Car car);
    Task UpdateCarAsync(Car car);
    Task DeleteCarAsync(int id);
    Task<List<Car>> GetCarsByBrandAsync(string brand);
    Task<List<Car>> GetCarsByModelAsync(string model);
    Task<List<Car>> GetCarsBySeatsAsync(int seats);
    Task<List<Car>> GetCarsByGearboxAsync(string gearbox);


}
