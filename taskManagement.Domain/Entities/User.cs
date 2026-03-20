using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace taskManagement.Domain.Entities;

public enum UserRole
{
    User = 1,
    Admin = 2
}
public  class User
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }

    public string Email { get; private set; }

    public string PasswordHash { get; private set; }

    public UserRole Role { get; private set; }

    public DateTime createdAt { get; private set; }

    public User(string name, string email, string passwordHash, UserRole role)
    {
        Id = Guid.NewGuid();
        Name = name;
        Email = email;
        PasswordHash = passwordHash;
        Role = role;
        createdAt = DateTime.UtcNow;
    }

  

}
