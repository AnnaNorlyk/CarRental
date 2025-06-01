namespace CarRental.ViewModels;

[QueryProperty(nameof(Car), "car")]
public partial class ConfirmationViewModel : BaseViewModel
{
    [ObservableProperty] private string car;

    [RelayCommand]
    private async Task GoBackAsync()
    {
        await Shell.Current.GoToAsync("//MenuPage");
    }



}
