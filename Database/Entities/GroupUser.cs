using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Database.Entities
{
    public class GroupUser
    {
        public int GroupId { get; set; }
        public int UserId { get; set; }

        
        // navigation prop
        public User User { get; set; }      // 1 user
        public Group Group { get; set; }    // 1 group
    }
}