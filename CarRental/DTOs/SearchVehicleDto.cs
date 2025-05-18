using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.DTOs
{
    public class SearchVehicleDto
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int Seats { get; set; }
        public string TransmissionType { get; set; }
    }
}
