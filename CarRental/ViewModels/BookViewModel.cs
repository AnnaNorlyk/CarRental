using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CarRental.ViewModels
{
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

        public ObservableCollection<string> SeatOptions { get; } = new() { "2", "4", "5", "7" };
        public ObservableCollection<string> GearboxOptions { get; } = new() { "Manual", "Automatic" };

        [RelayCommand]
        private async Task Search()
        {
            Console.WriteLine($"Search criteria: seats={SelectedSeat}, gearbox={SelectedGearbox}, start={DepartureDate:yyyy-MM-dd}, end={ReturnDate:yyyy-MM-dd}");
            var query = $"seats={SelectedSeat}&gearbox={SelectedGearbox}&start={DepartureDate:yyyy-MM-dd}&end={ReturnDate:yyyy-MM-dd}";
            await Shell.Current.GoToAsync($"{nameof(ChooseCarPage)}?{query}");
        }
    }
}
