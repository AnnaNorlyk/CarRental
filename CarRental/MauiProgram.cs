namespace CarRental;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});
#if DEBUG
		builder.Logging.AddDebug();
#endif
		builder.Services.AddSingleton<LogInMainViewModel>();

		builder.Services.AddSingleton<MenuViewModel>();

		builder.Services.AddSingleton<BookViewModel>();

		builder.Services.AddSingleton<ChooseCarViewModel>();

		builder.Services.AddSingleton<PaymentViewModel>();

		builder.Services.AddSingleton<ConfirmationViewModel>();

		builder.Services.AddSingleton<CollectKeyViewModel>();

		builder.Services.AddSingleton<DeliverKeyViewModel>();

        builder.Services.AddSingleton<IVehicleService, VehicleService>();

        builder.Services.AddSingleton<HttpClient>();

        return builder.Build();
	}
}
