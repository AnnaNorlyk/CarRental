using CarRental.DTOs;
using CarRental.Models;
using CarRental.Services;
using System.Collections.ObjectModel;
using System.Net.Http.Headers;

namespace CarRental.ViewModels;

[QueryProperty(nameof(Seats), "seats")]
[QueryProperty(nameof(Gearbox), "gearbox")]
public partial class ChooseCarViewModel : BaseViewModel
{
    private readonly IVehicleService _vehicleService;

    public ChooseCarViewModel(IVehicleService vehicleService)
    {
        _vehicleService = vehicleService;
    }

    [ObservableProperty]
    private string seats = string.Empty;

    [ObservableProperty]
    private string gearbox = string.Empty;

    [ObservableProperty]
    private ObservableCollection<Vehicle> filteredCars = new();

    partial void OnSeatsChanged(string value) => _ = LoadCarsAsync();
    partial void OnGearboxChanged(string value) => _ = LoadCarsAsync();

    private async Task LoadCarsAsync()
    {
        if (!int.TryParse(Seats, out int seatCount) || string.IsNullOrWhiteSpace(Gearbox))
            return;

        try
        {
            var token = await SecureStorage.GetAsync("auth_token");
            if (string.IsNullOrEmpty(token))
            {
                Console.WriteLine("Token is missing. User might not be logged in.");
                return;
            }

            var searchDto = new SearchVehicleDto
            {
                StartDate = DateTime.Today.ToString("yyyy-MM-dd"), // Placeholder
                EndDate = DateTime.Today.AddDays(3).ToString("yyyy-MM-dd"), // Placeholder
                Seats = seatCount,
                TransmissionType = Gearbox.ToLower()
            };

            var matchingCars = await _vehicleService.SearchVehiclesAsync(searchDto, token);
            FilteredCars = new ObservableCollection<Vehicle>(matchingCars);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching filtered cars: {ex.Message}");
        }
    }

    [RelayCommand]
    private async Task SelectCar(Vehicle vehicle)
    {
        var query = $"name={Uri.EscapeDataString(vehicle.Name)}&price={vehicle.PricePerDay}";
        await Shell.Current.GoToAsync($"{nameof(PaymentPage)}?{query}");
    }
}
