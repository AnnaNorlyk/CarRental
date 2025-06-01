using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Net.Http;
using System.Threading.Tasks;

namespace CarRental.ViewModels;

public partial class DeliverKeyViewModel : BaseViewModel
{
    private readonly HttpClient _httpClient;

    public DeliverKeyViewModel(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    [RelayCommand]
    private async Task CloseLocker()
    {
        try
        {
            var response = await _httpClient.PostAsync("https://your-api-url/api/locker/open", null);

            if (response.IsSuccessStatusCode)
            {
                await Shell.Current.DisplayAlert("Success", "Locker is closing!", "OK");
            }
            else
            {
                await Shell.Current.DisplayAlert("Error", "Failed to close locker.", "OK");
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", $"Exception: {ex.Message}", "OK");
        }
    }
}
