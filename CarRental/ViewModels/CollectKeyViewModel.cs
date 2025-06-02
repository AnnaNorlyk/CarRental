using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Net.Http;
using System.Net.Http.Json;

namespace CarRental.ViewModels;

public partial class CollectKeyViewModel : BaseViewModel
{
    private readonly ILockerService _lockerService;

    public CollectKeyViewModel(ILockerService lockerService)
    {
        _lockerService = lockerService;
    }

    [RelayCommand]
    private async Task Unlock()
    {
        var success = await _lockerService.OpenLockerAsync();

        if (success)
            await Shell.Current.DisplayAlert("Success", "Locker is opening", "OK");
        else
            await Shell.Current.DisplayAlert("Error", "Failed to open locker", "OK");
    }
}
