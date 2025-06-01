using System.Text;
using System.Text.Json;
using System.Windows.Input;
using CarRental.DTOs;
using CarRental.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CarRental.ViewModels;

public partial class RegisterViewModel : BaseViewModel
{
    private readonly IUserService _userService;

    [ObservableProperty]
    private string firstName;

    [ObservableProperty]
    private string lastName;

    [ObservableProperty]
    private string email;

    [ObservableProperty]
    private string license;

    public RegisterViewModel(IUserService userService)
    {
        _userService = userService;
    }

    [RelayCommand]
    private async Task Register()
    {
        if (string.IsNullOrWhiteSpace(FirstName) ||
            string.IsNullOrWhiteSpace(LastName) ||
            string.IsNullOrWhiteSpace(Email) ||
            string.IsNullOrWhiteSpace(License) ||
            License.Length != 8)
        {
            await Shell.Current.DisplayAlert("Validation Error", "Please fill in all fields. License must be exactly 8 digits.", "OK");
            return;
        }

        var user = new CreateUserDto
        {
            FirstName = FirstName,
            LastName = LastName,
            Email = Email,
            License = License
        };

        var result = await _userService.CreateUserAsync(user);

        if (result)
        {
            await Shell.Current.DisplayAlert("Success", "User registered successfully!", "OK");
            await Shell.Current.GoToAsync(".."); // Go back to login
        }
        else
        {
            await Shell.Current.DisplayAlert("Error", "User registration failed. Please try again.", "OK");
        }
    }
}
