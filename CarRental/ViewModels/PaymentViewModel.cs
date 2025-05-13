namespace CarRental.ViewModels;

[QueryProperty(nameof(CarName), "name")]
[QueryProperty(nameof(PricePerDay), "price")]
public partial class PaymentViewModel : BaseViewModel
{
    [ObservableProperty] private string carName;
    [ObservableProperty] private decimal pricePerDay;

    [ObservableProperty] private string cardNumber;
    [ObservableProperty] private string expiry;
    [ObservableProperty] private string cvc;

    [RelayCommand]
    private async Task SubmitPayment()
    {
        await Shell.Current.GoToAsync($"{nameof(ConfirmationPage)}?car={Uri.EscapeDataString(CarName)}");
    }
}
