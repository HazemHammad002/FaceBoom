using MiniFacebook.Data;
using MiniFacebook.Models.Entities;
using MiniFacebook.Models.RepoInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniFacebook.Models.RepoClass
{
    public class UserRepo : IUserRepo
    {
        private readonly ApplicationDbContext context;
        public UserRepo(ApplicationDbContext _context)
        {
            context = _context;
        }

        public User getUser(string Uid)
        {
            return context.Users.Where(U => U.Id == Uid).ToList()[0];
        }

        public void updateProfile(User user)
        {
            var u = context.Users.Find(user.Id);
            if (user.ProfilePic != null)
            {
                u.ProfilePic = user.ProfilePic;
            }
            u.PhoneNumber = user.PhoneNumber;
            u.UserName = user.UserName;
            u.Gender = user.Gender;
            context.SaveChanges();
        }
    }
}
