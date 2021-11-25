using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Database.Entities
{
    public class Function
    {
        public int Id { get; set; }
        public string ActionName { get; set; }


        // navigation prop
        public List<Permission> Permissions { get; set; }   // many permissions
    }
}