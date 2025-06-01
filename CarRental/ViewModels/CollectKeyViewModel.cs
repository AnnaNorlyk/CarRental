using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Net.Http;
using System.Net.Http.Json;

namespace CarRental.ViewModels;

public partial class CollectKeyViewModel : BaseViewModel
{
    private readonly HttpClient _httpClient;

    public CollectKeyViewModel(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    [RelayCommand]
    private async Task OpenLocker()
    {
        try
        {
            var response = await _httpClient.PostAsync("https://your-api-url/api/locker/open", null);

            if (response.IsSuccessStatusCode)
            {
                await Shell.Current.DisplayAlert("Success", "Locker is opening!", "OK");
            }
            else
            {
                await Shell.Current.DisplayAlert("Error", "Failed to open locker.", "OK");
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", $"Exception: {ex.Message}", "OK");
        }
    }
}
