namespace CarRental.Views;

public partial class ConfirmationPage : ContentPage
{
	public ConfirmationPage(ConfirmationViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}


    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        if (this.BindingContext is Booking booking)
        {
            var start = DateTimeOffset.FromUnixTimeMilliseconds(booking.StartDate).DateTime;
            var end = DateTimeOffset.FromUnixTimeMilliseconds(booking.EndDate).DateTime;

            BookingSummary.Text = $"Vehicle: {booking.VehicleId}\n" +
                                  $"From: {start:dd/MM/yyyy HH:mm}\nTo: {end:dd/MM/yyyy HH:mm}";
        }
    }


}
