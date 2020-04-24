using MiniFacebook.Data;
using MiniFacebook.Models.RepoInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniFacebook.Models.RepoClass
{
    public class RoleRepo:IRoleRepo
    {
        private readonly ApplicationDbContext context;
        public RoleRepo(ApplicationDbContext _context)
        {
            context = _context;
        }
    }
}
