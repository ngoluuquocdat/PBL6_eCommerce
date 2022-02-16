using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserAPI.ViewModels
{
    public class ResetPassVm
    {
        public string Email { get; set; }
        public string NewPass { get; set; }
        public string Token { get; set; }
    }
}