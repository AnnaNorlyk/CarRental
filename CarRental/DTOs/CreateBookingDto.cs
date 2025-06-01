namespace CarRental.DTOs;

public class CreateBookingDto
{
    public string VehicleId { get; set; }
    public string CustomerFirstName { get; set; }
    public string CustomerLastName { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerLicenseId { get; set; }
    public string CustomerMobile { get; set; }
    public string StartDate { get; set; } 
    public string EndDate { get; set; }
}
