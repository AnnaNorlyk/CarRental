using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.DTOs;

namespace CarRental.Services
{
    public interface IBookingService
    {
        Task<Booking?> CreateBookingAsync(CreateBookingDto dto);
    }

}
