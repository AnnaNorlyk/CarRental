namespace CarRental.Views;

public partial class LogInMainPage : ContentPage
{
	public LogInMainPage(LogInMainViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
