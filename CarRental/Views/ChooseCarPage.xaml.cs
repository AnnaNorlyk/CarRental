
namespace CarRental.Views;

public partial class ChooseCarPage : ContentPage
{
    
    public ChooseCarPage(ChooseCarViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
    }


    private async void OnVehicleSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Vehicle selected)
        {
            // Hent ViewModel (hvis du vil hente StartDate/EndDate derfra)
            if (BindingContext is ChooseCarViewModel vm)
            {
                var navParams = new Dictionary<string, object>
            {
                { "vehicleId", selected.Id },
                { "start", vm.StartDate },
                { "end", vm.EndDate }
            };

                await Shell.Current.GoToAsync(nameof(PaymentPage), navParams);
            }

            // (Valgfrit) Ryd valg, så man kan trykke samme bil igen
            ((CollectionView)sender).SelectedItem = null;
        }
    }

}
