using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Net.Http;
using System.Threading.Tasks;

namespace CarRental.ViewModels;

public partial class DeliverKeyViewModel : BaseViewModel
{
    private readonly ILockerService _lockerService;

    public DeliverKeyViewModel(ILockerService lockerService)
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

