namespace CarRental.Models
{
    public class Car
    {
        public string Name { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string Id { get; set; } = string.Empty;
        public int Seats { get; set; }
        public string Gearbox { get; set; } = string.Empty;
        public decimal PricePerDay { get; set; }
        public string ImageUrl { get; set; } = string.Empty;

    }
}
