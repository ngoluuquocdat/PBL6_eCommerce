using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eComSolution.ViewModel.System.Users
{
    public class RegisterRequest
    {
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Username { get; set; }
        public string Password {get; set;}

    }
}