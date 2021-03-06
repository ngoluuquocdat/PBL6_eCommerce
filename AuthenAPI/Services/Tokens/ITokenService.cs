using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Database.Entities;

namespace AuthenAPI.Services.Tokens
{
    public interface ITokenService
    {
        string CreateToken(User user, string role_name);
        ClaimsPrincipal ValidateToken(string jwtToken);
    }
}