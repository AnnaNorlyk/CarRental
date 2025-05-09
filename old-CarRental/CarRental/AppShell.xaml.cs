namespace CarRental;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();


        
        Routing.RegisterRoute(nameof(LogInMainPage), typeof(LogInMainPage));
        Routing.RegisterRoute(nameof(MenuPage), typeof(MenuPage));
        Routing.RegisterRoute(nameof(BookPage), typeof(BookPage));
        Routing.RegisterRoute(nameof(ChooseCarPage), typeof(ChooseCarPage));
        Routing.RegisterRoute(nameof(PaymentPage), typeof(PaymentPage));
        Routing.RegisterRoute(nameof(ConfirmationPage), typeof(ConfirmationPage));
        Routing.RegisterRoute(nameof(CollectKeyPage), typeof(CollectKeyPage));
        Routing.RegisterRoute(nameof(DeliverKeyPage), typeof(DeliverKeyPage));

  

    }
}
