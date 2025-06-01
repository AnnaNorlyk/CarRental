using CarRental.DTOs;
using CarRental.Models;
namespace CarRental.Services;
public interface IVehicleService

{
    Task<List<Vehicle>> SearchVehiclesAsync(SearchVehicleDto dto, string token);

}
