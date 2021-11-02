using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using eComSolution.Data.Entities;

namespace eComSolution.ViewModel.System.Users
{
    public class UserViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Họ tên")]
        public string Fullname { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Số điện thoại")]
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public bool Disable { get; set; }
        
        // [Display(Name = "Mã cửa hàng")]
        // public int ShopId { get; set; }

    }
}