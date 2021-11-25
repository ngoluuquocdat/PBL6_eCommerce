using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace UserAPI.ViewModels
{
    public class UserPermission
    {
        public int Id { get; set; }

        public List<string> Permissions { get; set; }
    }
}