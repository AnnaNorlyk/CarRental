using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.DTOs;

namespace CarRental.Services
{
    public interface IUserService
    {

        Task<List<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int id);
        Task<bool> CreateUserAsync(CreateUserDto user);
        Task<string?> LoginAsync(LoginDto loginDto);


    }


}
