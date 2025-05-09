namespace CarRental.Views;

public partial class PaymentPage : ContentPage
{
	public PaymentPage(PaymentViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
