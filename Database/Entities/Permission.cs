using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Database.Entities
{
    public class Permission
    {
        public int GroupId { get; set; }
        public int FunctionId { get; set; }


        // navigation prop
        public Group Group { get; set; }    // 1 group
        public Function Function { get; set; }      // 1 role
    }
}