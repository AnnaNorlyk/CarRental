
namespace CarRental.Models
{
    public class User
    {
        public int Id { get; set; }  // Primary key
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string License { get; set; }  // 8-digit license number
    }
}
