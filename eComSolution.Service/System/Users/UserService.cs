using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using eComSolution.Data.EF;
using eComSolution.Data.Entities;
using eComSolution.Service.System.Token;
using eComSolution.ViewModel.System.Users;
using eShopSolution.ViewModels.Common;
using Microsoft.EntityFrameworkCore;

namespace eComSolution.Service.System.Users
{
    public class UserService : IUserService
    {
        private readonly EComDbContext _context;
        private readonly ITokenService _tokenService;

        public UserService(EComDbContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        public async Task<ApiResult<string>> Login(LoginRequest request)
        {
            var user =  await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
            if(user == null) return new ApiResult<string>(false, "Invalid username!");   // return Unauthorized("Invalid Username.");

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password));

            // so sánh 2 mảng byte: password hash từ request VS p   assword hash của user trong Db
            for(int i = 0; i<computedHash.Length; i++)
            {
                if(computedHash[i] != user.PasswordHash[i]) return new ApiResult<string>(false, "Invalid password!");; //Unauthorized("Invalid Password.");
            }

            return new ApiResult<string>(true, ResultObj : _tokenService.CreateToken(user));
        }

        public async Task<ApiResult<string>> Register(RegisterRequest request)
        {
            string message = string.Empty;
            bool check = false;
            if(_context.Users.Any(u => u.Username == request.Username.ToLower()))
            {
                check = true;
                message += "Username is exist! \n";
            }
            if(_context.Users.Any(u => u.Email == request.Email.ToLower()))
            {
                check = true;
                message += "Email is exist! \n";
            }
            if(_context.Users.Any(u => u.PhoneNumber == request.PhoneNumber))
            {
                check = true;
                message += "Phonenumber is exist! \n";
            }
            if(check){
                return new ApiResult<string>(false, message);
            }

            using var hmac = new HMACSHA512();

            var new_user = new User
            {
                Username = request.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password)),
                PasswordSalt = hmac.Key,
                Fullname = request.Fullname,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber
            };
            _context.Users.Add(new_user);
            await _context.SaveChangesAsync();

            return new ApiResult<string>(true, ResultObj : _tokenService.CreateToken(new_user));
        }
        public async Task<ApiResult<UserViewModel>> GetUserById(int UserId)
        {
            var user =  await _context.Users.FirstOrDefaultAsync(u => u.Id == UserId);
            if(user == null) return new ApiResult<UserViewModel>(false, "User is not exist!");

            var userViewModel = new UserViewModel 
            {
                Id = user.Id,
                Fullname = user.Fullname,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };

            return new ApiResult<UserViewModel>(true, userViewModel);
        }

        public async Task<ApiResult<UserPermission>> GetPermissions(int UserId){
            var user =  await _context.Users.FirstOrDefaultAsync(u => u.Id == UserId);
            if(user == null) return new ApiResult<UserPermission>(false, "User is not exist!");

            var query = from _user in _context.Users
            join _groupuser in _context.GroupUsers on _user.Id equals _groupuser.UserId
            join _permission in _context.Permissions on _groupuser.GroupId equals _permission.GroupId
            join _function in _context.Functions on _permission.FunctionId equals _function.Id
            where _user.Id == UserId
            select new { 
                function = _function.ActionName
            }.ToString(); 

            var userPermission = new UserPermission{
                Id = UserId,
                Permissions = query.Distinct().ToList()
            };
            return new ApiResult<UserPermission>(true, userPermission);
        }
    }
}