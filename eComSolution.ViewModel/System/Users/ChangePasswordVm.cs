using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eComSolution.ViewModel.System.Users
{
    public class ChangePasswordVm
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ComfirmPassword { get; set; }
    }
}