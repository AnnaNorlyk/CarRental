using CarRental.Models;
using System.Collections.ObjectModel;

namespace CarRental.ViewModels;

[QueryProperty(nameof(Seats), "seats")]
[QueryProperty(nameof(Gearbox), "gearbox")]
public partial class ChooseCarViewModel : BaseViewModel
{
    private readonly CarService _carService;

    public ChooseCarViewModel(CarService carService)
    {
        _carService = carService;
    }

    [ObservableProperty]
    private string seats = string.Empty;

    [ObservableProperty]
    private string gearbox = string.Empty;

    [ObservableProperty]
    private ObservableCollection<Car> filteredCars = new();

    partial void OnSeatsChanged(string value) => _ = LoadCarsAsync();
    partial void OnGearboxChanged(string value) => _ = LoadCarsAsync();

    private async Task LoadCarsAsync()
    {
        if (!int.TryParse(Seats, out int seatCount) || string.IsNullOrWhiteSpace(Gearbox))
            return;

        try
        {
            var allCars = await _carService.GetAvailableCarsAsync();

            var filtered = allCars
                .Where(c => c.Seats == seatCount &&
                            c.Gearbox.Equals(Gearbox, StringComparison.OrdinalIgnoreCase))
                .ToList();

            FilteredCars = new ObservableCollection<Car>(filtered);
        }
        catch (Exception ex)
        {
            // handle errors, optionally show message
            Console.WriteLine($"Error loading cars: {ex.Message}");
        }
    }

    [RelayCommand]
    private async Task SelectCar(Car car)
    {
        var query = $"name={Uri.EscapeDataString(car.Name)}&price={car.PricePerDay}";
        await Shell.Current.GoToAsync($"{nameof(PaymentPage)}?{query}");
    }
}
