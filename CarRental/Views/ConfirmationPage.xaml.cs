namespace CarRental.Views;

public partial class ConfirmationPage : ContentPage
{
	public ConfirmationPage(ConfirmationViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
