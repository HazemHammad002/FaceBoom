using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniFacebook.Models.Entities
{
    public class Role:IdentityRole
    {
        public string Description { get; set; }
        public Role():base()
        {
                
        }
        public Role(string rolename):base()
        {

        }
    }
}
