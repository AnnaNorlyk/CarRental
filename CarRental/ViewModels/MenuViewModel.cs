namespace CarRental.ViewModels;

public partial class MenuViewModel : BaseViewModel
{

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
