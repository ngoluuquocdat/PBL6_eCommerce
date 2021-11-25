using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Database.Entities
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }    // vd: admin, mod, customer,...

        
        // navigation prop
        public List<GroupUser> GroupUsers { get; set; }     // many group users
        public List<Permission> Permissions { get; set; }   // many permissions
    }
}