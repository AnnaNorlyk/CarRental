namespace CarRental.ViewModels;

public partial class LogInMainViewModel : BaseViewModel
{

    [ObservableProperty]
    private string email;

    [ObservableProperty]
    private string driverLicense;

    [RelayCommand]
    private async Task Login()
    {
        await Shell.Current.GoToAsync(nameof(MenuPage));
    }
}
