namespace CarRental.Views;

public partial class ChooseCarPage : ContentPage
{
    private readonly ChooseCarViewModel _viewModel;
    public ChooseCarPage(ChooseCarViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = _viewModel = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadVehiclesCommand.ExecuteAsync(null);
    }

}
