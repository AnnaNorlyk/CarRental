using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;


namespace CarRental.Services
{
   

    public class LockerService : ILockerService
    {
        private readonly HttpClient _httpClient;

        public LockerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> OpenLockerAsync()
        {
            try
            { 
                var response = await _httpClient.PostAsync("open", null);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Locker open error: " + ex.Message);
                return false;
            }
        }
    }

}
