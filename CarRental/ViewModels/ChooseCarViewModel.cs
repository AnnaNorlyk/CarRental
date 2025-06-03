using CarRental.DTOs;
using CarRental.Models;
using CarRental.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;

namespace CarRental.ViewModels;

[QueryProperty(nameof(StartDate), "start")]
[QueryProperty(nameof(EndDate), "end")]
public partial class ChooseCarViewModel : BaseViewModel
{
    [ObservableProperty]
    private Vehicle selectedVehicle;

    [ObservableProperty]
    private DateTime startDate;

    [ObservableProperty]
    private DateTime endDate;

    private readonly IVehicleService _vehicleService;

    public ObservableCollection<Vehicle> Vehicles { get; } = new();

    public ChooseCarViewModel(IVehicleService vehicleService)
    {
        _vehicleService = vehicleService;
        LoadAllVehicles(); // kald ved instansiering – eller kald fra view
    }

    private async void LoadAllVehicles()
    {
        try
        {
            var result = await _vehicleService.GetAllVehiclesAsync();
            Vehicles.Clear();
            foreach (var v in result)
            {
                Console.WriteLine($"🔍 Vehicle: {v.Fabricant} {v.Model} ({v.Seats} seats, {v.TransmissionType})");
                Vehicles.Add(v);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("🚨 Failed to load vehicles: " + ex.Message);
        }
    }
}