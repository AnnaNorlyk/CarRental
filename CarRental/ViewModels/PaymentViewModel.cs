using CarRental.DTOs;
using CarRental.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

[QueryProperty(nameof(VehicleId), "vehicleId")]
[QueryProperty(nameof(StartDate), "start")]
[QueryProperty(nameof(EndDate), "end")]
public partial class PaymentViewModel : BaseViewModel
{
    private readonly HttpClient _http;

    public PaymentViewModel(HttpClient httpClient)
    {
        _http = httpClient;
    }

    [ObservableProperty] private string vehicleId;
    [ObservableProperty] private DateTime startDate;
    [ObservableProperty] private DateTime endDate;

    [RelayCommand]
    public async Task PayAsync()
    {
        var dto = new CreateBookingDto
        {
            VehicleId = VehicleId,
            CustomerFirstName = "Anna",
            CustomerLastName = "Kristensen",
            CustomerEmail = "anna@test.com",
            CustomerLicenseId = "LIC789",
            CustomerMobile = "+4512345678",
            StartDate = StartDate.ToString("o"),
            EndDate = EndDate.ToString("o")
        };

        try
        {
            var response = await _http.PostAsJsonAsync("bookings", dto);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                await Shell.Current.DisplayAlert("Fejl", $"Booking mislykkedes: {error}", "OK");
                return;
            }

            var json = await response.Content.ReadAsStringAsync();
            var booking = JsonSerializer.Deserialize<Booking>(json);

            await Shell.Current.GoToAsync(nameof(ConfirmationPage), new Dictionary<string, object>
            {
                { "booking", booking }
            });
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Fejl", $"Der skete en fejl: {ex.Message}", "OK");
        }
    }
}
