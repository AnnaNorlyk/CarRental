using CarRental.DTOs;
using CarRental.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;


namespace CarRental.ViewModels;

public partial class LogInMainViewModel : BaseViewModel
{
    private readonly IUserService _userService;

    public LogInMainViewModel(IUserService userService)
    {
        _userService = userService;
    }

    [ObservableProperty]
    private string email;

    [ObservableProperty]
    private string driverLicense;

    [RelayCommand]
    private async Task Login()
    {
        Debug.WriteLine($"Email: '{Email}'");
        Debug.WriteLine($"License: '{DriverLicense}'");

        var loginDto = new LoginDto
        {
            Email = Email,
            License = DriverLicense
        };

        var token = await _userService.LoginAsync(loginDto);

        if (!string.IsNullOrEmpty(token))
        {
            await Shell.Current.GoToAsync(nameof(MenuPage));
        }
        else
        {
            await Shell.Current.DisplayAlert("Login failed", "Invalid email or license number.", "OK");
        }
    }



    [RelayCommand]
    private async Task GoToRegister()
    {
        await Shell.Current.GoToAsync(nameof(RegisterPage));
    }
}
