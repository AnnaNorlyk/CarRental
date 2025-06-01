using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CarRental.Models;
using CarRental.Services;
using CarRental.DTOs;

namespace CarRental.ViewModels
{
    [QueryProperty(nameof(Seats), "seats")]
    [QueryProperty(nameof(Gearbox), "gearbox")]
    [QueryProperty(nameof(Start), "start")]
    [QueryProperty(nameof(End), "end")]
    public partial class ChooseCarViewModel : BaseViewModel
    {
        private readonly IVehicleService _vehicleService;

        public ChooseCarViewModel(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [ObservableProperty] private string seats;
        [ObservableProperty] private string gearbox;
        [ObservableProperty] private string start;
        [ObservableProperty] private string end;

        [ObservableProperty] private ObservableCollection<Vehicle> filteredCars = new();

        partial void OnSeatsChanged(string value) => TryLoadCars();
        partial void OnGearboxChanged(string value) => TryLoadCars();
        partial void OnStartChanged(string value) => TryLoadCars();
        partial void OnEndChanged(string value) => TryLoadCars();

        private async void TryLoadCars()
        {
            if (!IsValidQuery()) return;

            Console.WriteLine($"Loading cars with: {Seats}, {Gearbox}, {Start}, {End}");
            if (!int.TryParse(Seats, out int seatCount)) return;
            var dto = new SearchVehicleDto
            {
                Seats = seatCount,
                TransmissionType = Gearbox.ToLower(),
                StartDate = Start,
                EndDate = End
            };

            try
            {
                var token = await SecureStorage.GetAsync("auth_token");
                var cars = await _vehicleService.SearchVehiclesAsync(dto, token);
                FilteredCars = new ObservableCollection<Vehicle>(cars);
                Console.WriteLine($"Found {cars.Count} cars");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching cars: " + ex.Message);
            }
        }

        private bool IsValidQuery() =>
            !string.IsNullOrEmpty(Seats) &&
            !string.IsNullOrEmpty(Gearbox) &&
            !string.IsNullOrEmpty(Start) &&
            !string.IsNullOrEmpty(End);

        [RelayCommand]
        private async Task LoadVehicles()
        {
            // Example: fetch and assign vehicles
            var token = await SecureStorage.GetAsync("auth_token");
            if (string.IsNullOrEmpty(token)) return;

            var searchDto = new SearchVehicleDto
            {
                StartDate = Start,
                EndDate = End,
                Seats = int.Parse(Seats),
                TransmissionType = Gearbox.ToLower()
            };

            var vehicles = await _vehicleService.SearchVehiclesAsync(searchDto, token);
            FilteredCars = new ObservableCollection<Vehicle>(vehicles);
        }
        [RelayCommand]
        private async Task SelectCar(Vehicle selectedVehicle)
        {
            if (selectedVehicle == null) return;

            Console.WriteLine($"[DEBUG] Selected vehicle: {selectedVehicle.Id}");

            var query = $"name={Uri.EscapeDataString(selectedVehicle.Model)}";
            await Shell.Current.GoToAsync($"{nameof(PaymentPage)}?{query}");
        }

    }
}