namespace CarRental.Views;

public partial class CollectKeyPage : ContentPage
{
	public CollectKeyPage(CollectKeyViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
