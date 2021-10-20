using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using eComSolution.Data.Entities;

namespace eComSolution.Service.System.Token
{
    public interface ITokenService
    {
        string CreateToken(User user);
        ClaimsPrincipal ValidateToken(string jwtToken);
    }
}