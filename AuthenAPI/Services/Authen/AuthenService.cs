using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.IO;
using AuthenAPI.Services.Tokens;
using AuthenAPI.ViewModels.Common;
using AuthenAPI.ViewModels;
using EmailValidation;
using Database;
using Database.Entities;
using System.Text.RegularExpressions;

namespace AuthenAPI.Services.Authen
{
    public class AuthenService : IAuthenService
    {
        private readonly EComDbContext _context;
        private readonly ITokenService _tokenService;

        public AuthenService(EComDbContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        public async Task<ApiResult<string>> Login(LoginRequest request)   // Login
        {

            bool IsNull = (String.IsNullOrEmpty(request.Username) || String.IsNullOrEmpty(request.Password));
            string role_name = "";

            if(IsNull) return new ApiResult<string>(false, "Dữ liệu đầu vào không được để trống!");

            var user =  await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
            if(user == null) return new ApiResult<string>(false, "Tên đăng nhập không tồn tại!");   // return Unauthorized("Invalid Username.");

            var roles = await (from _user in _context.Users
            join _groupuser in _context.GroupUsers on _user.Id equals _groupuser.UserId
            join _group in _context.Groups on _groupuser.GroupId equals _group.Id
            where _user.Id == user.Id 
            select new {_group}).ToListAsync();
            
            if(roles.Count == 1) role_name = "Member";
            if(roles.Count == 2) role_name = "Mod";
            if(roles.Count == 3) role_name = "Admin";

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password));

            // so sánh 2 mảng byte: password hash từ request VS p   assword hash của user trong Db
            for(int i = 0; i<computedHash.Length; i++)
            {
                if(computedHash[i] != user.PasswordHash[i]) return new ApiResult<string>(false, "Mật khẩu không đúng. Hãy thử lại!");; //Unauthorized("Invalid Password.");
            }

            return new ApiResult<string>(true, ResultObj : _tokenService.CreateToken(user, role_name));
        }

        public bool IsValidEmail(string email){
            EmailValidator emailValidator = new EmailValidator();
            EmailValidationResult result;

            if (!emailValidator.Validate(email, out result))
            {
                Console.WriteLine("Unable to check email"); // no internet connection or mailserver is down / busy
            }

            if(result == EmailValidationResult.OK){
                return true;
            }else{
                return false;
            }
        }
        public async Task<ApiResult<string>> CheckUsername(string username){
            var user =  await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if(user == null) return new ApiResult<string>(true, "Tên đăng nhập hợp lệ!");
            else return new ApiResult<string>(true, true, "Tên đăng nhập đã được sử dụng. Vui lòng thử với tên đăng nhập khác!");
        }
        public async Task<ApiResult<string>> CheckEmail(string email){
            var user =  await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if(user == null) return new ApiResult<string>(true, "Email hợp lệ!");
            else return new ApiResult<string>(true, true, "Email đã được sử dụng. Vui lòng thử với email khác!");
        }
        public async Task<ApiResult<string>> CheckPhone(string phonenumber){
            var user =  await _context.Users.FirstOrDefaultAsync(u => u.PhoneNumber == phonenumber);
            if(user == null) return new ApiResult<string>(true, "Số điện thoại hợp lệ!");
            else return new ApiResult<string>(true, true, "Số điện thoại này đã được sử dụng. Vui lòng thử với số điện thoại khác!");
        }

        public async Task<ApiResult<string>> Register(RegisterRequest request)
        {
            bool IsNull = (String.IsNullOrEmpty(request.Fullname) || String.IsNullOrEmpty(request.Email) || String.IsNullOrEmpty(request.PhoneNumber)
                          || String.IsNullOrEmpty(request.Username) || String.IsNullOrEmpty(request.Password));

            if(IsNull) return new ApiResult<string>(false, "Dữ liệu đầu vào không được để trống!");

            if(!IsValid("", "", request.Email, "")) // valid email
            {
                return new ApiResult<string>(false, "Email không hợp lệ. Vui lòng nhập lại!");
            }
            // check email is used
            var email =  await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if(email != null) return new ApiResult<string>(true, true, "Email đã được sử dụng. Vui lòng thử với email khác!");
            
            if(VerifyEmail(request.Email.ToLower()) == false) // verify email
            {
                return new ApiResult<string>(false, "Email này không tồn tại. Vui lòng nhập Email khác và thử lại!");
            }
            
            if(!IsValid("", "", "",request.PhoneNumber)) // valid phone number
            {
                return new ApiResult<string>(false, "Số điện thoại không hợp lệ. Vui lòng nhập lại!");
            }
            // check phone number is used
            var phone =  await _context.Users.FirstOrDefaultAsync(u => u.PhoneNumber == request.PhoneNumber);
            if(phone != null) return new ApiResult<string>(true, true , "Số điện thoại này đã được sử dụng. Vui lòng thử với số điện thoại khác!");

            if(!IsValid(request.Username, "", "", "")) // valid username
            {
                return new ApiResult<string>(false, "Tên đăng nhập phải có ít nhất 8 kí tự, không bao gồm chữ cái viết hoa và kí tự đặc biệt.");
            }
            // check username is used
            var username =  await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
            if(username != null) return new ApiResult<string>(true, true, "Tên đăng nhập đã được sử dụng. Vui lòng thử với tên đăng nhập khác!");

            if(!IsValid("", request.Password, "", "")) // valid password
            {
                return new ApiResult<string>(false, "Mật khẩu tối thiểu 8 ký tự, ít nhất một chữ cái viết hoa, một chữ cái viết thường, một số và một ký tự đặc biệt.");
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

            var  gu = new GroupUser{
                GroupId = 3, 
                UserId = new_user.Id,
            };

            _context.GroupUsers.Add(gu);
            await _context.SaveChangesAsync();

            return new ApiResult<string>(true, Message: "Đăng kí tài khoản thành công!", ResultObj : _tokenService.CreateToken(new_user, "Member"));
        }

        public bool VerifyEmail(string email)  // Verify Email
        {
            EmailValidator emailValidator = new EmailValidator();
            EmailValidationResult result;

            emailValidator.Validate(email, out result);

            if(result == EmailValidationResult.OK) return true;
            else return false;
        }
        public  bool IsValid(string username, string password, string email, string phonenumber) 
        {
            if(!String.IsNullOrEmpty(username)) return Regex.Match(username, @"^(?=.{8,}$)(?![_.])(?!.*[_.]{2})[a-zA-Z0-9._]+(?<![_.])$").Success;
            if(!String.IsNullOrEmpty(password)) return Regex.Match(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$").Success;
            if(!String.IsNullOrEmpty(email)) return Regex.Match(email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$").Success; 
            if(!String.IsNullOrEmpty(phonenumber)) return Regex.Match(phonenumber, @"^([\+]?61[-]?|[0])?[1-9][0-9]{8}$").Success;

            return false;
        }

        public async Task<List<Function>> GetPermissions(int userId){
            var user =  await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if(user == null) return null;

            if (user.Disable == true) return new List<Function>{new Function() {ActionName = "Users.Get"}};

            var query = from _user in _context.Users
            join _groupuser in _context.GroupUsers on _user.Id equals _groupuser.UserId
            join _permission in _context.Permissions on _groupuser.GroupId equals _permission.GroupId
            join _function in _context.Functions on _permission.FunctionId equals _function.Id
            where _user.Id == userId
            select new Function { 
                ActionName = _function.ActionName
            }; 
            return query.Distinct().ToList();
        }
    }
}
