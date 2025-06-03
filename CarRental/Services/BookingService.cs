using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using CarRental.DTOs;

namespace CarRental.Services
{
    public class BookingService : IBookingService
    {
        private readonly HttpClient _http;

        public BookingService(HttpClient http)
        {
            _http = http;
        }

        public async Task<Booking?> CreateBookingAsync(CreateBookingDto dto)
        {
            var response = await _http.PostAsJsonAsync("bookings", dto);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Booking failed: " + error);
                return null;
            }

            return await response.Content.ReadFromJsonAsync<Booking>();
        }
    }
}
