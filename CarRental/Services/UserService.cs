using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using CarRental.DTOs;

namespace CarRental.Services;

internal class UserService : IUserService
{
    private readonly HttpClient _httpClient;

    public UserService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<bool> CreateUserAsync(CreateUserDto user)
    {
        try
        {
            var json = JsonSerializer.Serialize(user, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("http://localhost:3000/api/users", content);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("CreateUser error: " + ex.Message);
            return false;
        }
    }

    public async Task<string?> LoginAsync(LoginDto loginDto)
    {
        try
        {
            var json = JsonSerializer.Serialize(loginDto, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            Debug.WriteLine($"Sending JSON: {json}");
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Add this line explicitly to avoid any ambiguity
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await _httpClient.PostAsync("login", content);
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                Debug.WriteLine($"API Response: {error}");
                Debug.WriteLine($"StatusCode: {response.StatusCode}");
                return null;
            }

            var result = await response.Content.ReadAsStringAsync();
            var jsonDoc = JsonDocument.Parse(result);
            var token = jsonDoc.RootElement.GetProperty("token").GetString();

            if (!string.IsNullOrEmpty(token))
            {
                Preferences.Set("auth_token", token);

                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }

            return token;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Login error: " + ex.Message);
            return null;
        }
    }



    // implement later as needed
    public Task AddUserAsync(User user) => throw new NotImplementedException();
    public Task DeleteUserAsync(int id) => throw new NotImplementedException();
    public Task<List<User>> GetAllUsersAsync() => throw new NotImplementedException();
    public Task<User> GetUserByIdAsync(int id) => throw new NotImplementedException();
    public Task UpdateUserAsync(User user) => throw new NotImplementedException();
    public Task<User> GetUserByEmailAsync(string email) => throw new NotImplementedException();
    public Task<User> GetUserByLicenseAsync(string license) => throw new NotImplementedException();
    public Task<User> GetUserByNameAsync(string name) => throw new NotImplementedException();
    public Task<User> GetUserByPhoneAsync(string phone) => throw new NotImplementedException();
}
