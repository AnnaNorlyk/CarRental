using System.Net.Http.Headers;

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
        builder.Services.AddSingleton<RegisterViewModel>();

        builder.Services.AddSingleton<IBookingService, BookingService>();
        builder.Services.AddSingleton<ILockerService, LockerService>();
        builder.Services.AddSingleton<IVehicleService, VehicleService>();
        builder.Services.AddSingleton<IUserService, UserService>();
        builder.Services.AddSingleton<HttpClient>(provider =>
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:3000/api/")
            };

            var token = Preferences.Get("auth_token", null);
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }
            return client;
        });


        // Load auth token and apply it globally if it exists
        var token = Preferences.Get("auth_token", null);
        if (!string.IsNullOrEmpty(token))
        {
            var httpClient = builder.Services.BuildServiceProvider()
                                             .GetRequiredService<HttpClient>();
            httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }

        return builder.Build();
    }
}
