using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using taskManagement.Domain.Entities;
using TaskManagement.Application.Abstractions.Persistence;
using BCrypt.Net;

namespace TaskManagement.Application.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<User> RegisterUserAsync(string name, string email, string password, UserRole role)
        {
            var existingUser = await _userRepository.GetByEmailAsync(email);
            if(existingUser !=null)
            { 
                throw new Exception("Email already in use."); 
            }
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                throw new Exception("Name, email, and password are required.");
            }

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

            var user = new User(name, email, passwordHash,role);
            
            await _userRepository.AddAsync(user);
            return user;

        }

        
    }
    
}
