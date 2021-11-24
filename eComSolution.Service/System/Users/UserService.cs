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
using System.IO;
using System.Text.RegularExpressions;

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
        public async Task<ApiResult<string>> Login(LoginRequest request)   // Login
        {

            bool IsNull = (String.IsNullOrEmpty(request.Username) || String.IsNullOrEmpty(request.Password));

            if(IsNull) return new ApiResult<string>(false, "Dữ liệu đầu vào không được để trống!");

            var user =  await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
            if(user == null) return new ApiResult<string>(false, "Tên đăng nhập không tồn tại!");   // return Unauthorized("Invalid Username.");

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password));

            // so sánh 2 mảng byte: password hash từ request VS p   assword hash của user trong Db
            for(int i = 0; i<computedHash.Length; i++)
            {
                if(computedHash[i] != user.PasswordHash[i]) return new ApiResult<string>(false, "Mật khẩu không đúng. Hãy thử lại!");; //Unauthorized("Invalid Password.");
            }

            return new ApiResult<string>(true, ResultObj : _tokenService.CreateToken(user));
        }
        public async Task<ApiResult<string>> CheckUsername(string username){
            var user =  await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if(user == null) return new ApiResult<string>(true, "Tên đăng nhập hợp lệ!");
            else return new ApiResult<string>(false, "Tên đăng nhập đã được sử dụng. Vui lòng thử với tên đăng nhập khác!");
        }
        public async Task<ApiResult<string>> CheckEmail(string email){
            var user =  await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if(user == null) return new ApiResult<string>(true, "Email hợp lệ!");
            else return new ApiResult<string>(false, "Email đã được sử dụng. Vui lòng thử với email khác!");
        }
        public async Task<ApiResult<string>> CheckPhone(string phonenumber){
            var user =  await _context.Users.FirstOrDefaultAsync(u => u.PhoneNumber == phonenumber);
            if(user == null) return new ApiResult<string>(true, "Số điện thoại hợp lệ!");
            else return new ApiResult<string>(false, "Số điện thoại này đã được sử dụng. Vui lòng thử với số điện thoại khác!");
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
            if(email != null) return new ApiResult<string>(false, "Email đã được sử dụng. Vui lòng thử với email khác!");
            
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
            if(phone != null) return new ApiResult<string>(false, "Số điện thoại này đã được sử dụng. Vui lòng thử với số điện thoại khác!");

            if(!IsValid(request.Username, "", "", "")) // valid username
            {
                return new ApiResult<string>(false, "Tên đăng nhập phải có ít nhất 8 kí tự, không bao gồm chữ cái viết hoa và kí tự đặc biệt.");
            }
            // check username is used
            var username =  await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
            if(username != null) return new ApiResult<string>(false, "Tên đăng nhập đã được sử dụng. Vui lòng thử với tên đăng nhập khác!");

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
                Address = request.Address,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber
            };
            
            _context.Users.Add(new_user);

            await _context.SaveChangesAsync();

            var  gu = new GroupUser{
                GroupId = 1, 
                UserId = new_user.Id,
            };

            _context.GroupUsers.Add(gu);
            await _context.SaveChangesAsync();

            return new ApiResult<string>(true, Message: "Đăng kí tài khoản thành công!", ResultObj : _tokenService.CreateToken(new_user));
        }
        public async Task<ApiResult<UserViewModel>> GetUserById(int UserId)
        {
            var user =  await _context.Users.FirstOrDefaultAsync(u => u.Id == UserId);
            if(user == null) return new ApiResult<UserViewModel>(false, "Người dùng này không tồn tại trong hệ thống!");

            var userViewModel = new UserViewModel 
            {
                Id = user.Id,
                Fullname = user.Fullname,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address
            };

            return new ApiResult<UserViewModel>(true, userViewModel);
        }
        public async Task<ApiResult<List<UserViewModel>>> GetAllUsers(string name){
            var query = await _context.Users.ToListAsync();
            if (name != null) query = query.Where(u => u.Fullname.Contains(name)).ToList();
            List<UserViewModel> listUser = new List<UserViewModel>();
            foreach (var user in query){
                if(user.Id == 1) continue;
                listUser.Add(new UserViewModel {Id = user.Id, Fullname = user.Fullname, Email = user.Email, PhoneNumber = user.PhoneNumber, Address = user.Address, Disable = user.Disable});
            }
            return new ApiResult<List<UserViewModel>>(true, listUser);
        }
        public async Task<ApiResult<List<UserViewModel>>> GetUserDisable(){
            var query = await _context.Users.Where(user => user.Disable == true).ToListAsync();
            List<UserViewModel> listUser = new List<UserViewModel>();
            foreach (var user in query){
                listUser.Add(new UserViewModel {Id = user.Id, Fullname = user.Fullname, Email = user.Email, PhoneNumber = user.PhoneNumber, Address = user.Address, Disable = user.Disable});
            }
            if(listUser.Count > 0){
                return new ApiResult<List<UserViewModel>>(true, listUser);
            }else{
                return new ApiResult<List<UserViewModel>>(false, Message: "Không có người dùng nào bị vô hiệu hóa!");
            }
        }
        public async Task<ApiResult<string>> DisableUser(int userId){
            var user =  await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if(user == null) return new ApiResult<string>(false, "Người dùng này không tồn tại trong hệ thống!");

            if(user.ShopId != null){
                var shop = await _context.Shops.FirstOrDefaultAsync(s => s.Id == user.ShopId);
                shop.Disable = true;
                _context.Shops.Update(shop);
            }

            user.Disable = true;
            _context.Users.Update(user);

            if(await _context.SaveChangesAsync() > 0)
            return new ApiResult<string>(true, Message: "Đã vô hiệu hóa người dùng này!");
            else
            return new ApiResult<string>(false, Message: "Đã xảy ra lỗi. Vui lòng thử lại!");

        }
        public async Task<ApiResult<string>> EnableUser(int userId){
            var user =  await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if(user == null) return new ApiResult<string>(false, "Người dùng này không tồn tại trong hệ thống!");

            if(user.ShopId != null){
                var shop = await _context.Shops.FirstOrDefaultAsync(s => s.Id == user.ShopId);
                shop.Disable = false;
                _context.Shops.Update(shop);
            }

            user.Disable = false;
            _context.Users.Update(user);

            if(await _context.SaveChangesAsync() > 0)
            return new ApiResult<string>(true, Message: "Enable user successfully!");
            else
            return new ApiResult<string>(false, Message: "Đã xảy ra lỗi. Vui lòng thử lại!");

        }
        public async Task<List<Function>> GetPermissions(int userId){
            var user =  await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if(user == null) return null;

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
        public async Task<ApiResult<string>> ChangePassword(int userId, ChangePasswordVm request)
        {
            bool IsNull = (String.IsNullOrEmpty(request.CurrentPassword) || String.IsNullOrEmpty(request.NewPassword));

            if(IsNull) return new ApiResult<string>(false, "Dữ liệu đầu vào không được để trống!");

            var user =  await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.CurrentPassword));

            // so sánh 2 mảng byte: password hash từ request VS p   assword hash của user trong Db
            for(int i = 0; i<computedHash.Length; i++)
            {
                if(computedHash[i] != user.PasswordHash[i]) return new ApiResult<string>(false, "Mật khẩu hiện tại của bạn không đúng. Vui lòng thử lại!");; //Unauthorized("Invalid Password.");
            }

            if(!IsValid("", request.NewPassword, "", "")) // valid password
            {
                return new ApiResult<string>(false, "Mật khẩu tối thiểu 8 ký tự, ít nhất một chữ cái viết hoa, một chữ cái viết thường, một số và một ký tự đặc biệt.");
            }

            user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.NewPassword));
            _context.Update(user);
            if(await _context.SaveChangesAsync() > 0)
            return new ApiResult<string>(true, Message: "Thay đổi mật khẩu thành công!");
            else
            return new ApiResult<string>(false, Message: "Đã xảy ra lỗi. Vui lòng thử lại!");
        }
        public string GetUniqueKey(int size)   // tạo key cho chức năng quên mật khẩu
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
        public string GetHash(string plainText)  // hash md5
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
            bool IsNull = (String.IsNullOrEmpty(email));

            if(IsNull) return new ApiResult<string>(false, "Dữ liệu đầu vào không được để trống!");
            // check email is exist?
            var user =  await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if(user == null) return new ApiResult<string>(false, "Email không đúng hoặc không tồn tại!");
            
            // random key with length = 16
            var key = GetUniqueKey(16);
            key = GetHash(key);
            string emailhash = GetHash(email);

            string content = "";
            string s = "<a href=" + $"https://localhost:5001/api/Users/ConfirmResetPass?email={emailhash}&key={key}" + @">
            <button type='button'>Reset Password</button>
            </a> ";
            string[] readfile = File.ReadAllLines("./wwwroot/index.html");
                foreach (string line in readfile)
                {
                    string test = line;
                    string newtest = test.Trim();
                    if (newtest.Equals("<a href=''><button></button></a>"))
                    {
                        newtest =  newtest.Replace(newtest, s);    
                    }
                    content += newtest;
                }
            
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

            return new ApiResult<string>(true, Message: "Chúng tôi đã gửi thông tin thông tin đặt lại mật khẩu đến email của bạn. Vui lòng kiểm tra email và làm theo hướng dẫn!");
        }
        public async Task<ApiResult<string>> ResetPassword(string email, string password){
            bool IsNull = (String.IsNullOrEmpty(email) || String.IsNullOrEmpty(password));

            if(IsNull) return new ApiResult<string>(false, "Dữ liệu đầu vào không được để trống!");
            var user =  await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            using var hmac = new HMACSHA512(user.PasswordSalt);

            if(!IsValid("", password, "", "")) // valid password
            {
                return new ApiResult<string>(false, "Mật khẩu tối thiểu 8 ký tự, ít nhất một chữ cái viết hoa, một chữ cái viết thường, một số và một ký tự đặc biệt.");
            }

            user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            _context.Update(user);
            if(await _context.SaveChangesAsync() > 0)
            return new ApiResult<string>(true, Message: "Đặt lại mật khẩu mới thành công!");
            else
            return new ApiResult<string>(false, Message: "Đã xảy ra lỗi. Vui lòng thử lại!");
        }
        public async Task<ApiResult<string>> ComfirmResetPassword(string email, string key){
            var resetPass =  await _context.ResetPasses.FirstOrDefaultAsync(u => u.Email == email);
            if(resetPass == null) return new ApiResult<string>(false, "Không khả dụng. Vui lòng nhấn chọn 'quên mật khẩu'.");

            // kiểm tra thời gian
            var list = _context.ResetPasses.Where(x => DateTime.Now <= ((DateTime)x.Time).AddMinutes(5)).ToList();
            foreach(ResetPass i in list){
                if(i == resetPass){

                    // nhập sai quá 3 lần thì xóa record
                    if(resetPass.Numcheck > 2)
                    {
                        _context.ResetPasses.Remove(resetPass);
                        _context.SaveChanges();
                        return new ApiResult<string>(false, "Bạn đã nhập sai quá số lần quy định!");
                    }

                    // sai key thì tăng numcheck lên 1
                    if(resetPass.Token != key){
                        resetPass.Numcheck += 1;
                        _context.ResetPasses.Update(resetPass);
                        _context.SaveChanges();
                        return new ApiResult<string>(false, "Mã xác thực không đúng!");
                    }
                    
                    // thời gian ok, key ok thì chuyển qua cho nhập pass mới
                    return new ApiResult<string>(true, Message: "Kiểm tra ok rồi, cho chuyển qua đổi pass đi");
                }
            }

            return new ApiResult<string>(false, Message: "Mã quá hạn! Vui lòng nhấn chọn lại 'Quên mật khẩu'.");
        }
        public async Task<ApiResult<string>> UpdateUser(int userId, UpdateUserVm updateUser)
        {
            bool IsNull = (String.IsNullOrEmpty(updateUser.Fullname) || String.IsNullOrEmpty(updateUser.Email) 
                        || String.IsNullOrEmpty(updateUser.PhoneNumber) || String.IsNullOrEmpty(updateUser.Address));

            if(IsNull) return new ApiResult<string>(false, "Dữ liệu đầu vào không được để trống!");

            var user =  await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if(user == null) return new ApiResult<string> (false, Message: "Người dùng này không tồn tại trong hệ thống!");

            if(!IsValid("", "", updateUser.Email, "")) // valid email
            {
                return new ApiResult<string>(false, "Email không hợp lệ. Vui lòng nhập lại!");
            }
            // check email is used
            var email =  await _context.Users.FirstOrDefaultAsync(u => u.Email == updateUser.Email &&  u.Id != userId);
            if(email != null) return new ApiResult<string>(false, "Email đã được sử dụng. Vui lòng thử với email khác!");

            if(VerifyEmail(updateUser.Email.ToLower()) == false) // verify email
            {
                return new ApiResult<string>(false, "Email này không tồn tại. Vui lòng nhập Email khác và thử lại!");
            }
            
            if(!IsValid("", "", "",updateUser.PhoneNumber)) // valid phone number
            {
                return new ApiResult<string>(false, "Số điện thoại không hợp lệ. Vui lòng nhập lại!");
            }
            // check phone number is used
            var phone =  await _context.Users.FirstOrDefaultAsync(u => u.PhoneNumber == updateUser.PhoneNumber &&  u.Id != userId);
            if(phone != null) return new ApiResult<string>(false, "Số điện thoại này đã được sử dụng. Vui lòng thử với số điện thoại khác!");

            user.Fullname = updateUser.Fullname;
            user.Email = updateUser.Email;
            user.PhoneNumber = updateUser.PhoneNumber;
            user.Address = updateUser.Address;

            _context.Users.Update(user);

            if(await _context.SaveChangesAsync() > 0)
            return new ApiResult<string>(true, Message: "Cập nhật thông tin cá nhân thành công!");
            else
            return new ApiResult<string>(false, Message: "Đã xảy ra lỗi. Vui lòng thử lại!");
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
    }
}
