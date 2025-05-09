namespace CarRental.ViewModels;

public partial class BookViewModel : BaseViewModel
{


    [ObservableProperty]
    private DateTime departureDate = DateTime.Today;

    [ObservableProperty]
    private DateTime returnDate = DateTime.Today.AddDays(1);

    [ObservableProperty]
    private string selectedSeat;

    [ObservableProperty]
    private string selectedGearbox;

    public ObservableCollection<string> SeatOptions { get; } = new()
    {
        "2", "4", "5", "7"
    };

    public ObservableCollection<string> GearboxOptions { get; } = new()
    {
        "Manual", "Automatic"
    };

 
    [RelayCommand]
    private async Task Search()
    {
        var seat = Uri.EscapeDataString(SelectedSeat);
        var gearbox = Uri.EscapeDataString(SelectedGearbox);

        await Shell.Current.GoToAsync($"{nameof(ChooseCarPage)}?seats={seat}&gearbox={gearbox}");
    }


}
