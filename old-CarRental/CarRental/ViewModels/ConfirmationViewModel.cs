namespace CarRental.ViewModels;

[QueryProperty(nameof(Car), "car")]
public partial class ConfirmationViewModel : BaseViewModel
{
    [ObservableProperty] private string car;
}
