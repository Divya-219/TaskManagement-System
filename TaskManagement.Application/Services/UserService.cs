using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using taskManagement.Domain.Entities;
using TaskManagement.Application.Abstractions.Persistence;
using BCrypt.Net;
using System.Runtime.CompilerServices;

namespace TaskManagement.Application.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private  readonly IJwtService _jwtService;

        public UserService(IUserRepository userRepository, IJwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
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
        public async Task<string>loginasync(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null)
                throw new Exception("email not found.");
            var validPassword = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
            if (!validPassword)
                throw new Exception("password not found");
            return _jwtService.GenerateToken(user);

            



        }


    }
    
}
