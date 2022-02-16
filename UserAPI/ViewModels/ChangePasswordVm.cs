using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserAPI.ViewModels
{
    public class ChangePasswordVm
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}