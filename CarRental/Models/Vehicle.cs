namespace CarRental.Models
{
    public class Vehicle
    {
        public string Id { get; set; }
        public string Model { get; set; }
        public string Fabricant { get; set; }
        public int Seats { get; set; }
        public string TransmissionType { get; set; }
        public bool IsAvailable { get; set; }
    }
}
