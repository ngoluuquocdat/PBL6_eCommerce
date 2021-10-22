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
using EmailValidation;
using eShopSolution.ViewModels.Common;
using Microsoft.EntityFrameworkCore;
using eComSolution.Service.Common;

namespace eComSolution.Service.System.Users
{
    public class UserService : IUserService
    {
        private readonly EComDbContext _context;
        private readonly ITokenService _tokenService;
        private readonly IEmailService _emailService;

        public UserService(EComDbContext context, ITokenService tokenService, IEmailService emailService)
        {
            _context = context;
            _tokenService = tokenService;
            _emailService = emailService;
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

        // public bool IsValidEmail(string email){
        //     EmailValidator emailValidator = new EmailValidator();
        //     EmailValidationResult result;

        //     if (!emailValidator.Validate(email, out result))
        //     {
        //         Console.WriteLine("Unable to check email"); // no internet connection or mailserver is down / busy
        //     }

        //     if(result == EmailValidationResult.OK){
        //         return true;
        //     }else{
        //         return false;
        //     }
        // }

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
            
            // if(IsValidEmail(request.Email.ToLower()) == false){
            //     return new ApiResult<string>(false, "Email is not exist!");
            // }

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

        public async Task<ApiResult<UserPermission>> GetPermissions(int userId){
            var user =  await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if(user == null) return new ApiResult<UserPermission>(false, "User is not exist!");

            var query = from _user in _context.Users
            join _groupuser in _context.GroupUsers on _user.Id equals _groupuser.UserId
            join _permission in _context.Permissions on _groupuser.GroupId equals _permission.GroupId
            join _function in _context.Functions on _permission.FunctionId equals _function.Id
            where _user.Id == userId
            select new { 
                function = _function.ActionName
            }.ToString(); 

            var userPermission = new UserPermission{
                Id = userId,
                Permissions = query.Distinct().ToList()
            };
            return new ApiResult<UserPermission>(true, userPermission);
        }
        public async Task<ApiResult<string>> ChangePassword(int userId, ChangePasswordVm request){
            var user =  await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.CurrentPassword));

            // so sánh 2 mảng byte: password hash từ request VS p   assword hash của user trong Db
            for(int i = 0; i<computedHash.Length; i++)
            {
                if(computedHash[i] != user.PasswordHash[i]) return new ApiResult<string>(false, "Your current password is incorrect!");; //Unauthorized("Invalid Password.");
            }
            user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.NewPassword));
            _context.Update(user);
            if(await _context.SaveChangesAsync() > 0)
            return new ApiResult<string>(true, Message: "Success change password!");
            else
            return new ApiResult<string>(false, Message: "Fail! Please try again.");
        }

        // tạo key cho chức năng quên mật khẩu
        public string GetUniqueKey(int size)
        {
            char[] chars =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            byte[] data = new byte[size];
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetBytes(data);
            }
            StringBuilder result = new StringBuilder(size);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }
        
        // hash md5
        public string GetHash(string plainText)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(plainText));
            byte[] result = md5.Hash;
            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }
        public async Task<ApiResult<string>> ForgetPassword(string email){
            // check email is exist?
            var user =  await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if(user == null) return new ApiResult<string>(false, "Your email is not correct!");

            // random key with length = 16
            var key = GetUniqueKey(16);
            key = GetHash(key);
            string emailhash = GetHash(email);
            
            string content = $"You have requested to reset your password \nLink reset pass: https://localhost:5001/api/Users/ConfirmResetPass?email={emailhash}&key={key}";

            _emailService.SendEmail(email, content);

            // nếu có record cùng gmail thì xóa 
            var instance = await _context.ResetPasses.FirstOrDefaultAsync(u => u.Email == emailhash);
            if(instance != null){
                _context.ResetPasses.Remove(instance);
            }
            // insert record ResetPass
            var x = new ResetPass{
                Email = emailhash,
                Token = key,
                Time = DateTime.Now
            };
            _context.ResetPasses.Add(x);
            await _context.SaveChangesAsync();

            return new ApiResult<string>(true, Message: "We have sent password reset information to your email, please check your email and follow the instructions");
        }
        public async Task<ApiResult<string>> ResetPassword(string email, string password){
            var user =  await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            using var hmac = new HMACSHA512(user.PasswordSalt);

            user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            _context.Update(user);
            if(await _context.SaveChangesAsync() > 0)
            return new ApiResult<string>(true, Message: "Success reset password!");
            else
            return new ApiResult<string>(false, Message: "Fail! Please try again.");
        }
        public async Task<ApiResult<string>> ComfirmResetPassword(string email, string key){
            var resetPass =  await _context.ResetPasses.FirstOrDefaultAsync(u => u.Email == email);
            if(resetPass == null) return new ApiResult<string>(false, "No availble. Please press forget password again!");

            // kiểm tra thời gian
            var list = _context.ResetPasses.Where(x => DateTime.Now <= ((DateTime)x.Time).AddMinutes(1)).ToList();
            foreach(ResetPass i in list){
                if(i == resetPass){

                    // nhập sai quá 3 lần thì xóa record
                    if(resetPass.Numcheck > 2)
                    {
                        _context.ResetPasses.Remove(resetPass);
                        _context.SaveChanges();
                        return new ApiResult<string>(false, "You have entered the wrong number of times");
                    }

                    // sai key thì tăng numcheck lên 1
                    if(resetPass.Token != key){
                        resetPass.Numcheck += 1;
                        _context.ResetPasses.Update(resetPass);
                        _context.SaveChanges();
                        return new ApiResult<string>(false, "Your key is not correct!");
                    }
                    
                    // thời gian ok, key ok thì chuyển qua cho nhập pass mới
                    return new ApiResult<string>(true, Message: "Kiểm tra ok rồi, cho chuyển qua đổi pass đi");
                }
            }

            return new ApiResult<string>(false, Message: "mã quá hạn mẹ rồi!");
        }
        public async Task<ApiResult<string>> UpdateUser(int userId, UpdateUserVm updateUser){
            var user =  await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if(user == null) return new ApiResult<string> (false, Message: "User is not exist!");

            user.Fullname = updateUser.Fullname;
            user.Email = updateUser.Email;
            user.PhoneNumber = updateUser.PhoneNumber;

            _context.Users.Update(user);

            if(await _context.SaveChangesAsync() > 0)
            return new ApiResult<string>(true, Message: "Success update user!");
            else
            return new ApiResult<string>(false, Message: "Fail! Please try again.");
        }
    }
}
