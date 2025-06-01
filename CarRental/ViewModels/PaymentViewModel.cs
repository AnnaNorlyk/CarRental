using CarRental.DTOs;




public partial class PaymentViewModel : BaseViewModel
{
    private readonly IUserService _userService;
    private readonly IVehicleService _vehicleService;

    public PaymentViewModel(IUserService userService, IVehicleService vehicleService)
    {
        _userService = userService;
        _vehicleService = vehicleService;
    }

}
