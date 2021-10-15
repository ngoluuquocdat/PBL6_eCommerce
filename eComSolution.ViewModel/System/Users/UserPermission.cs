using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using eComSolution.Data.Entities;

namespace eComSolution.ViewModel.System.Users
{
    public class UserPermission
    {
        public int Id { get; set; }

        public List<string> Permissions { get; set; }
    }
}