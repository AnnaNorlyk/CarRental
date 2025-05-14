using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Services
{
    internal class UserService : IUserService
    {
        public UserService() { }
        public Task AddUserAsync(User user)
        {
            throw new NotImplementedException();
        }
        public Task DeleteUserAsync(int id)
        {
            throw new NotImplementedException();
        }
        public Task<List<User>> GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }
        public Task<User> GetUserByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
        public Task UpdateUserAsync(User user)
        {
            throw new NotImplementedException();
        }
        public Task<User> GetUserByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }
        public Task<User> GetUserByLicenseAsync(string license)
        {
            throw new NotImplementedException();
        }
        public Task<User> GetUserByNameAsync(string name)
        {
            throw new NotImplementedException();
        }
        public Task<User> GetUserByPhoneAsync(string phone)
        {
            throw new NotImplementedException();
        }

    }
}
