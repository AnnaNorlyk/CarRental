using System.Net.Http;

namespace CarRental.ViewModels;

public partial class MenuViewModel : BaseViewModel
{
    private readonly HttpClient _httpClient;


    public MenuViewModel(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    [RelayCommand]
    private async Task Logout()
    {
        Preferences.Remove("auth_token");
        _httpClient.DefaultRequestHeaders.Authorization = null;
        await Shell.Current.GoToAsync($"//{nameof(LogInMainPage)}");
    }



    [RelayCommand]
    private async Task GoToBooking() =>
        await Shell.Current.GoToAsync(nameof(BookPage));

    [RelayCommand]
    private async Task GoToCollectKey() =>
        await Shell.Current.GoToAsync(nameof(CollectKeyPage));

    [RelayCommand]
    private async Task GoToDeliverKey() =>
        await Shell.Current.GoToAsync(nameof(DeliverKeyPage));

}
