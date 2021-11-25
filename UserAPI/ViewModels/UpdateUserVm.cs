using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UserAPI.ViewModels
{
    public class UpdateUserVm
    {

        [Display(Name = "Họ tên")]
        public string Fullname { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Số điện thoại")]
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

    }
}