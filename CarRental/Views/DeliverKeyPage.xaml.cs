namespace CarRental.Views;

public partial class DeliverKeyPage : ContentPage
{
	public DeliverKeyPage(DeliverKeyViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
