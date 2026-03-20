using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using taskManagement.Domain.Entities;

namespace TaskManagement.Application.Abstractions.Persistence;

public interface IJwtService
{
    string GenerateToken(User user);
}
