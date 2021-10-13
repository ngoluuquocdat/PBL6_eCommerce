using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eComSolution.ViewModel.System.Users;

namespace eComSolution.Service.System.Users
{
    public interface IUserService
    {
        Task<string> Register(RegisterRequest request);

        Task<string> Login(LoginRequest request);
        
    }
}