using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Models
{
    public class Booking
    {
        public string Id { get; set; }
        public string VehicleId { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerLicenseId { get; set; }
        public string CustomerMobile { get; set; }
        public long StartDate { get; set; }
        public long EndDate { get; set; }
        public string KeyStatus { get; set; }
        public string DropoffCode { get; set; }
        public string CollectionCode { get; set; }
    }

}
